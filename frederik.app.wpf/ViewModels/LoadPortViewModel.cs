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

            _loadPort.Cassette.WaferAdded -= CassetteWaferAdded; 
            _loadPort.Cassette.WaferAdded += CassetteWaferAdded;

            _loadPort.Cassette.WaferRemoved -= CassetteWaferRemoved;
            _loadPort.Cassette.WaferRemoved += CassetteWaferRemoved;

            _loadPort.Cassette.WaferCleared -= CassetteWaferCleared;
            _loadPort.Cassette.WaferCleared += CassetteWaferCleared;
        }

        public ObservableCollection<Wafer> Wafers { get; set; } = new ObservableCollection<Wafer>();

        private void CassetteWaferRemoved(object? sender, Wafer e)
        {
            Wafers.Remove(e);
        }

        private void CassetteWaferAdded(object? sender, Wafer e)
        {
            Wafers.Add(e);
        }

        private void CassetteWaferCleared(object? sender, EventArgs e)
        {
            Wafers.Clear();
        }
    }
}
