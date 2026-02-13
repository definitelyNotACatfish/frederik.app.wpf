using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace frederik.app.wpf.ViewModels
{
    public abstract class AViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
