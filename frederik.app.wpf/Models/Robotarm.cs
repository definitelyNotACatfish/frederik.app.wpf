using frederik.app.wpf.Exceptions;

namespace frederik.app.wpf.Models
{
    public class RobotArm
    {
        public event EventHandler<Wafer>? CurrentWaferChanged;

        public event EventHandler<Station>? CurrentStationChanged;

        public event EventHandler<bool>? ArmIsRotatingEvent;

        public Wafer? CurrentWafer { get; private set; }

        public Station? CurrentStation { get; private set; }

        public bool IsArmRotating { get; private set; }

        /// <summary>
        /// Initializes robot arm on Boot
        /// </summary>
        /// <returns></returns>
        public async Task InitArm(Station station)
        {
            try
            {
                CurrentStation = station;
                await station.Occupy();
                CurrentStationChanged?.Invoke(this, CurrentStation);
            }
            catch (Exception ex)
            {
#warning TODO: handle exception
                throw;
            }
        }

        public async Task RotateArmToStation(Station newStation, CancellationToken cancellationToken = default)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested)
                { throw new TaskCanceledException(); }

                await CurrentStation.DeOccupy();
                CurrentStationChanged?.Invoke(this, CurrentStation);

                IsArmRotating = true;
                ArmIsRotatingEvent?.Invoke(this, IsArmRotating);
                // Simulate the arm movement
                await Task.Delay(TimeSpan.FromSeconds(2));
                IsArmRotating = false;
                ArmIsRotatingEvent?.Invoke(this, IsArmRotating);
                await Task.Delay(TimeSpan.FromSeconds(2));

                await newStation.Occupy();
                CurrentStation = newStation;
                CurrentStationChanged?.Invoke(this, CurrentStation);
            }
            catch (Exception ex)
            {
#warning TODO: handle exception
                throw;
            }
        }

        public async Task LoadWaferOnArm(Cassette cassette, CancellationToken cancellationToken = default)
        {
            if (CurrentWafer is not null)
            { throw new NoWaferLoadedException("Robot arm already has a wafer to loaded on station '{0}'", CurrentStation); }

            try
            {
                if (cancellationToken.IsCancellationRequested)
                { throw new TaskCanceledException(); }

                Wafer wafer = await cassette.GetNextWafer();
                CurrentWafer = wafer;
                CurrentWaferChanged?.Invoke(this, CurrentWafer);
            }
            catch (Exception ex)
            {
#warning TODO: Handle exception properly
                throw;
            }
        }

        public async Task PushWaferOnCassette(Cassette cassette, Wafer wafer, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            { throw new TaskCanceledException(); }

            if (CurrentWafer is null)
            { throw new NoWaferLoadedException("Robot arm has no wafer to load into cassette on station '{0}'", CurrentStation); }

            try
            {
                await cassette.AddWafer(wafer);
                CurrentWafer = null;
                CurrentWaferChanged?.Invoke(this, CurrentWafer);
            }
            catch (Exception ex)
            {
#warning TODO: Handle exception properly
                throw;
            }
        }
    }
}
