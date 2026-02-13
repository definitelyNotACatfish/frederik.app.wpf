using frederik.app.wpf.Commands;
using frederik.app.wpf.Models;
using System.Windows.Input;

namespace frederik.app.wpf.ViewModels
{
    public class WorkflowViewModel : AViewModelBase
    {
        private Workflow _workflow;

        public WorkflowViewModel()
        {
            _workflow = new Workflow();

            StartProcessing = new AsyncCommand(StartProccessing);
            PauseProcessing = new AsyncCommand(PauseProccessing);

            _workflow.IsProcessingEvent -= StartProcessingEvent;
            _workflow.IsProcessingEvent += StartProcessingEvent;

            _loadPortViewModel1 = new LoadPortViewModel(_workflow.LoadPort1);
            _loadPortViewModel2 = new LoadPortViewModel(_workflow.LoadPort2);
        }

        private void StartProcessingEvent(object? sender, bool e) => IsProcessing = e;


        public ICommand StartProcessing { get; }

        public ICommand PauseProcessing { get; }

        private async Task StartProccessing(object? parameters)
        {
            await _workflow.Start().ConfigureAwait(false); 
        }

        private async Task PauseProccessing(object? parameters)
        {
            await _workflow.Pause().ConfigureAwait(false);
        }


        private bool _isProcessing = false;
        public bool IsProcessing
        {
            get => _isProcessing;
            set
            {
                if (_isProcessing != value)
                {
                    _isProcessing = value;
                    OnPropertyChanged();
                }
            }
        }

        private LoadPortViewModel _loadPortViewModel1;
        public LoadPortViewModel LoadPortViewModel1
        {
            get
            {
                return _loadPortViewModel1;
            }
            set
            {
                if (_loadPortViewModel1 != value)
                {
                    _loadPortViewModel1 = value;
                    OnPropertyChanged();
                }
            }
        }

        private LoadPortViewModel _loadPortViewModel2;
        public LoadPortViewModel LoadPortViewModel2
        {
            get
            {
                return _loadPortViewModel2;
            }
            set
            {
                if (_loadPortViewModel2 != value)
                {
                    _loadPortViewModel2 = value;
                    OnPropertyChanged();
                }
            }
        }

        private RobotArm _robotArm = new RobotArm();
        public RobotArm RobotArm
        {
            get => _robotArm;
            set
            {
                if (_robotArm != value)
                {
                    _robotArm = value;
                    OnPropertyChanged();
                }
            }
        }

        private Station _stationA = new Station("StationA");
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

        private Station _stationB = new Station("StationB");
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
