namespace frederik.app.wpf.Models
{
    internal class Workflow
    {
        public LoadPort LoadPort1 { get; private set; } = new LoadPort();

        public LoadPort LoadPort2 { get; private set; } = new LoadPort();

        public Station StationA { get; private set; } = new Station("StationA");

        public Station StationB { get; private set; } = new Station("StationB");

        public RobotArm RobotArm { get; private set; } = new RobotArm();

        public bool Proccessing { get; private set; }

        public async Task Init()
        {
            await LoadPort1.LoadCassette();
            await LoadPort2.UnloadCassette();
            await RobotArm.InitArm(StationA);
        }

        public async Task Start()
        {
            Proccessing = true;

            try
            {
                await Init();

                // Nothing is null, as the init is called first, else it must be handled of course
                foreach (Wafer wafer in LoadPort1.Cassette.Wafers.ToList())
                {
                    await RobotArm.LoadWaferOnArm(LoadPort1.Cassette);
                    await RobotArm.RotateArmFromToStation(StationA, StationB);
                    await RobotArm.PushWaferOnCassette(LoadPort2.Cassette, RobotArm.CurrentWafer);
                    await RobotArm.RotateArmFromToStation(StationB, StationA);
                }
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

        public async Task Pause()
        {

        }
    }
}
