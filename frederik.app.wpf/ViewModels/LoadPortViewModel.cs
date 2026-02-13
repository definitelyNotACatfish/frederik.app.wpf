using frederik.app.wpf.Models;
using System.Collections.ObjectModel;

namespace frederik.app.wpf.ViewModels
{
    public class LoadPortViewModel : AViewModelBase
    {
        private LoadPort _loadPort;

        public LoadPortViewModel(LoadPort loadPort)
        {
            _loadPort = loadPort;

            Wafers.Clear();
            foreach (var wafer in loadPort.Cassette.Wafers)
            { Wafers.Add(wafer); }

            _loadPort.Cassette.WaferAdded -= Cassette_WaferAdded; 
            _loadPort.Cassette.WaferAdded += Cassette_WaferAdded;

            _loadPort.Cassette.WaferRemoved -= Cassette_WaferRemoved;
            _loadPort.Cassette.WaferRemoved += Cassette_WaferRemoved;
        }

        public ObservableCollection<Wafer> Wafers { get; set; } = new ObservableCollection<Wafer>();

        private void Cassette_WaferRemoved(object? sender, Wafer e)
        {
            Wafers.Remove(e);
        }

        private void Cassette_WaferAdded(object? sender, Wafer e)
        {
            Wafers.Add(e);
        }


    }
}
