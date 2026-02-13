using frederik.app.wpf.Exceptions;

namespace frederik.app.wpf.Models
{
    internal class RobotArm
    {
        public Wafer? CurrentWafer { get; private set; }

        public Station? CurrentStation { get; private set; }

        public bool IsArmRotating { get; private set; }

        /// <summary>
        /// Initializes robot arm on Boot
        /// </summary>
        /// <returns></returns>
        public async Task InitArm(Station station)
        {
            CurrentStation = station;
            await station.Occupy();
        }

        public async Task RotateArmFromToStation(Station previous, Station newStation)
        {
            try
            {
                await previous.DeOccupy();

                IsArmRotating = true;
                // Simulate the arm movement
                await Task.Delay(TimeSpan.FromSeconds(2));
                IsArmRotating = false;

                await newStation.Occupy();
                CurrentStation = newStation;
            }
            catch (Exception ex)
            {
#warning TODO: handle exception
            }
        }

        public async Task LoadWaferOnArm(Cassette cassette)
        {
            if (CurrentWafer is not null)
            { throw new NoWaferLoadedException("Robot arm already has a wafer to loaded on station '{0}'", CurrentStation); }

            try
            {
                Wafer wafer = await cassette.GetNextWafer();
                CurrentWafer = wafer;
            }
            catch (Exception ex)
            {
#warning TODO: Handle exception properly
            }
        }

        public async Task PushWaferOnCassette(Cassette cassette, Wafer wafer)
        {
            if(CurrentWafer is null)
            { throw new NoWaferLoadedException("Robot arm has no wafer to load into cassette on station '{0}'", CurrentStation);}

            try
            {
               await cassette.AddWafer(wafer);
               CurrentWafer = null;
            }
            catch (Exception ex) 
            {
#warning TODO: Handle exception properly
            }
        }
    }
}
