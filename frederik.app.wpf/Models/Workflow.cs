using frederik.app.wpf.Enums;
using frederik.app.wpf.Exceptions;

namespace frederik.app.wpf.Models
{
    internal class Workflow
    {
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public LoadPort LoadPort1 { get; private set; } = new LoadPort();

        public LoadPort LoadPort2 { get; private set; } = new LoadPort();

        public Station StationA { get; private set; } = new Station("StationA");

        public Station StationB { get; private set; } = new Station("StationB");

        public RobotArm RobotArm { get; private set; } = new RobotArm();

        public WorkflowState CurrentWorkflowState { get; private set; } = WorkflowState.Undefined;

        public bool Proccessing { get; private set; }

        public async Task Start()
        {
            Proccessing = true;

            try
            {
                await Init();
                await HandlingWafers();
            }
            catch (Exception ex)
            {
#warning TODO: handle exception
            }
            finally
            {
                Proccessing = false;
            }
        }

        public async Task Continue()
        {
            try
            {
                _cancellationTokenSource = new CancellationTokenSource();
                await HandlingWafers();
            }
            catch (Exception ex)
            {
#warning TODO: handle exception
            }
        }

        public async Task Pause()
        {
            _cancellationTokenSource.Cancel();
        }

        private async Task Init()
        {
            await LoadPort1.LoadCassette().ConfigureAwait(false);
            await LoadPort2.UnloadCassette().ConfigureAwait(false);
            await RobotArm.InitArm(StationA).ConfigureAwait(false);

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            CurrentWorkflowState = WorkflowState.Init;
        }

        private async Task HandlingWafers()
        {
            if(CurrentWorkflowState == WorkflowState.Undefined)
            { throw new WorkflowNotInitedException("Can't handle wafers, workflow is not inited"); }

            // Nothing is null, as the init is called first, else it must be handled of course
            foreach (Wafer wafer in LoadPort1.Cassette.Wafers.ToList())
            {
                if (CurrentWorkflowState == WorkflowState.Init 
                    || CurrentWorkflowState == WorkflowState.RobotArmOnStationA)
                {
                    CurrentWorkflowState = WorkflowState.RobotArmOnStationA;
                    await RobotArm.LoadWaferOnArm(LoadPort1.Cassette, _cancellationTokenSource.Token).ConfigureAwait(false);
                    CurrentWorkflowState = WorkflowState.LoadedWaferFromCassetteOnArm;
                }

                if(CurrentWorkflowState == WorkflowState.LoadedWaferFromCassetteOnArm)
                {
                    await RobotArm.RotateArmFromToStation(StationA, StationB, _cancellationTokenSource.Token).ConfigureAwait(false);
                    CurrentWorkflowState = WorkflowState.RobotArmRotatedFromA2B;
                }

                if(CurrentWorkflowState == WorkflowState.RobotArmRotatedFromA2B 
                    || CurrentWorkflowState == WorkflowState.RobotArmOnStationB)
                {
                    CurrentWorkflowState = WorkflowState.RobotArmOnStationB;
                    await RobotArm.PushWaferOnCassette(LoadPort2.Cassette, RobotArm.CurrentWafer, _cancellationTokenSource.Token).ConfigureAwait(false);
                    CurrentWorkflowState = WorkflowState.UnloadedWaferFromArmIntoCassette;
                }

                if(CurrentWorkflowState == WorkflowState.UnloadedWaferFromArmIntoCassette
                    || CurrentWorkflowState == WorkflowState.RobotArmRotatingFromB2A)
                {
                    CurrentWorkflowState = WorkflowState.RobotArmRotatingFromB2A;
                    await RobotArm.RotateArmFromToStation(StationB, StationA, _cancellationTokenSource.Token).ConfigureAwait(false);
                    CurrentWorkflowState = WorkflowState.RobotArmOnStationA;
                }
            }
        }
    }
}
