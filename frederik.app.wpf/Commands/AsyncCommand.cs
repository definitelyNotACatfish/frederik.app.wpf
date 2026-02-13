using System.Windows.Input;

namespace frederik.app.wpf.Commands
{
    public class AsyncCommand : ICommand
    {
        private readonly Func<object?, Task> _execute;
        private readonly Func<object?, bool>? _canExecute;
        private bool _isExecuting;

        public AsyncCommand(Func<object?, Task> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return !_isExecuting && (_canExecute == null || _canExecute(parameter));
        }

        public async void Execute(object? parameter)
        {
            _isExecuting = true;
            OnCanExecuteChanged();
            await _execute(parameter);
            _isExecuting = false;
            OnCanExecuteChanged();
        }

        public event EventHandler? CanExecuteChanged;
        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}