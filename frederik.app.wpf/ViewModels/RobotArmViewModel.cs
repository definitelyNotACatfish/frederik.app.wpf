using frederik.app.wpf.Models;
using System.Drawing.Printing;

namespace frederik.app.wpf.ViewModels
{
    public class RobotArmViewModel : AViewModelBase
    {
        private RobotArm _robotArm;

        public RobotArmViewModel(RobotArm robotArm, Station stationA, Station stationB)
        {
            _robotArm = robotArm;
            _stationA = stationA;
            _stationB = stationB;

            _robotArm.CurrentWaferChanged -= RobotArmCurrentWaferChanged;
            _robotArm.CurrentWaferChanged += RobotArmCurrentWaferChanged;

            _robotArm.CurrentStationChanged -= RobotArmCurrentStationChanged;
            _robotArm.CurrentStationChanged += RobotArmCurrentStationChanged;

            _robotArm.ArmIsRotatingEvent -= RobotArmIsRotatingEvent;
            _robotArm.ArmIsRotatingEvent += RobotArmIsRotatingEvent;
        }

        private void RobotArmIsRotatingEvent(object? sender, bool e)
        {
            IsRotating = e;
        }

        private void RobotArmCurrentStationChanged(object? sender, Station e)
        {
            CurrentStation = e;
        }

        private void RobotArmCurrentWaferChanged(object? sender, Wafer e)
        {
            CurrentWafer = e;
        }

        private Wafer? _currentWafer;
        public Wafer? CurrentWafer
        {
            get => _currentWafer;
            set
            {
                if (_currentWafer != value)
                {
                    _currentWafer = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isRotating;
        public bool IsRotating
        {
            get => _isRotating;
            set
            {
                if (_isRotating != value)
                {
                    _isRotating = value;
                    OnPropertyChanged();
                }
            }
        }

        private Station _currentStation;
        public Station CurrentStation
        {
            get => _currentStation;
            set
            {
                if (_currentStation != value)
                {
                    _currentStation = value;
                    OnPropertyChanged();
                }
            }
        }

        private Station _stationA;
        public Station StationA
        {
            get => _stationA;
            set
            {
                if (_stationA != value)
                {
                    _stationA = value;
                    OnPropertyChanged();
                }
            }
        }

        private Station _stationB;
        public Station StationB
        {
            get => _stationB;
            set
            {
                if (_stationB != value)
                {
                    _stationB = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
