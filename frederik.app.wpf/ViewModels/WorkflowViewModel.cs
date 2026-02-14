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
            _robotArmViewModel = new RobotArmViewModel(_workflow.RobotArm, _workflow.StationA, _workflow.StationB);
        }

        public ICommand StartProcessing { get; }

        public ICommand PauseProcessing { get; }

        private void StartProcessingEvent(object? sender, bool e) => IsProcessing = e;

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

        private RobotArmViewModel _robotArmViewModel;
        public RobotArmViewModel RobotArmViewModel
        {
            get => _robotArmViewModel;
            set
            {
                if (_robotArmViewModel != value)
                {
                    _robotArmViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
