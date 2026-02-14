using frederik.app.wpf.Enums;
using frederik.app.wpf.Exceptions;

namespace frederik.app.wpf.Models
{
    public class Workflow
    {
        private bool _pausePressed = false;

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public event EventHandler<bool>? IsProcessingEvent;

        public LoadPort LoadPort1 { get; private set; } = new LoadPort();

        public LoadPort LoadPort2 { get; private set; } = new LoadPort();

        public Station StationA { get; private set; } = new Station("Station A");

        public Station StationB { get; private set; } = new Station("Station B");

        public RobotArm RobotArm { get; private set; } = new RobotArm();

        public WorkflowState CurrentWorkflowState { get; private set; } = WorkflowState.Undefined;

        public bool IsProcessing { get; private set; }

        public async Task Start()
        {
            if (_pausePressed)
            {
                _pausePressed = false;
                await Continue();
                return;
            }

            IsProcessing = true;
            IsProcessingEvent?.Invoke(this, IsProcessing);

            try
            {
                await Init();

                // Give it some time, that the demo looks better
                await Task.Delay(1000);

                await HandlingWafers();
            }
            catch (Exception ex)
            {
#warning TODO: handle exception
            }
            finally
            {
                Finish();
            }
        }

        public async Task Continue()
        {
            try
            {
                IsProcessing = true;
                IsProcessingEvent?.Invoke(this, IsProcessing);
                _cancellationTokenSource = new CancellationTokenSource();
                await HandlingWafers();
            }
            catch (Exception ex)
            {
#warning TODO: handle exception
            }
            finally
            {
                Finish();
            }
        }

        private void Finish()
        {
            IsProcessing = false;
            IsProcessingEvent?.Invoke(this, IsProcessing);
        }

        public async Task Pause()
        {
            _cancellationTokenSource.Cancel();
            _pausePressed = true;
        }

        private async Task Init()
        {
            await LoadPort1.LoadCassette(Random.Shared.Next(1, 25));
            await LoadPort2.UnloadCassette();
            await RobotArm.InitArm(StationA);

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            CurrentWorkflowState = WorkflowState.Init;
        }

        private async Task HandlingWafers()
        {
            if (CurrentWorkflowState == WorkflowState.Undefined)
            { throw new WorkflowNotInitedException("Can't handle wafers, workflow is not inited"); }

            if (LoadPort1.Cassette is null)
            { throw new CassetteEmptyException("Cant handle wafers, cassete in load port 1 is empty"); }

            // Nothing is null, as the init is called first, else it must be handled of course
            foreach (Wafer wafer in LoadPort1.Cassette.Wafers.ToList())
            {
                while (!CurrentWorkflowState.Equals(WorkflowState.Done))
                {
                    if (CurrentWorkflowState.Equals(WorkflowState.Init)
                    || CurrentWorkflowState.Equals(WorkflowState.RobotArmOnStationA))
                    {
                        CurrentWorkflowState = WorkflowState.RobotArmOnStationA;
                        await RobotArm.LoadWaferOnArm(LoadPort1.Cassette, _cancellationTokenSource.Token);
                        CurrentWorkflowState = WorkflowState.LoadedWaferFromCassetteOnArm;
                    }

                    if (CurrentWorkflowState.Equals(WorkflowState.LoadedWaferFromCassetteOnArm))
                    {
                        await RobotArm.RotateArmToStation(StationB, _cancellationTokenSource.Token);
                        CurrentWorkflowState = WorkflowState.RobotArmRotatedFromA2B;
                    }

                    if (CurrentWorkflowState.Equals(WorkflowState.RobotArmRotatedFromA2B)
                        || CurrentWorkflowState.Equals(WorkflowState.RobotArmOnStationB))
                    {
                        CurrentWorkflowState = WorkflowState.RobotArmOnStationB;
                        await RobotArm.PushWaferOnCassette(LoadPort2.Cassette, RobotArm.CurrentWafer, _cancellationTokenSource.Token);
                        CurrentWorkflowState = WorkflowState.UnloadedWaferFromArmIntoCassette;
                    }

                    if (CurrentWorkflowState.Equals(WorkflowState.UnloadedWaferFromArmIntoCassette)
                        || CurrentWorkflowState.Equals(WorkflowState.RobotArmRotatingFromB2A))
                    {
                        CurrentWorkflowState = WorkflowState.RobotArmRotatingFromB2A;
                        await RobotArm.RotateArmToStation(StationA, _cancellationTokenSource.Token);
                        CurrentWorkflowState = WorkflowState.RobotArmOnStationA;
                        CurrentWorkflowState = WorkflowState.Done;
                    }
                }
                CurrentWorkflowState = WorkflowState.Init;
            }
        }
    }
}
