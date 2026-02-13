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

            _workflow.LoadPort1.Changed -= LoadPort1_Changed;
            _workflow.LoadPort1.Changed += LoadPort1_Changed;

            _workflow.LoadPort2.Changed -= LoadPort2_Changed;
            _workflow.LoadPort2.Changed += LoadPort2_Changed;
        }

        private void LoadPort1_Changed(object? sender, EventArgs e)
        {
            LoadPort1 = ((LoadPort)sender);
        }

        private void LoadPort2_Changed(object? sender, EventArgs e)
        {
            LoadPort2 = (LoadPort)sender;
        }

        private void StartProcessingEvent(object? sender, bool e) => IsProcessing = e;
        

        public ICommand StartProcessing { get; }

        public ICommand PauseProcessing { get; }

        private async Task StartProccessing(object? parameters)
        {
            await _workflow.Start();
        }

        private async Task PauseProccessing(object? parameters)
        {
            await _workflow.Pause();
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

        private LoadPort _loadPort1 = new LoadPort();
        public LoadPort LoadPort1
        {
            get => _loadPort1;
            set
            {
                if (_loadPort1 != value)
                {
                    _loadPort1 = value;
                    OnPropertyChanged();
                }
            }
        }

        private LoadPort _loadPort2 = new LoadPort();
        public LoadPort LoadPort2
        {
            get => _loadPort2;
            set
            {
                if (_loadPort2 != value)
                {
                    _loadPort2 = value;
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
