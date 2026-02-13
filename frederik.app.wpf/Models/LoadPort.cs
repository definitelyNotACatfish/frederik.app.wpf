namespace frederik.app.wpf.Models
{
    public class LoadPort
    {
        public Cassette Cassette { get; private set; } = new Cassette();

        public event EventHandler? Changed;

        public async Task LoadCassette(int waferCount)
        {
            await Cassette.Clear();
            for (int i = 0; i < waferCount; i++) 
            {
                await Cassette.AddWafer(new Wafer("Wafer"+i));
            }
            Changed?.Invoke(this, new EventArgs());
        }

        public async Task UnloadCassette()
        {
            await Cassette.Clear();
            Changed?.Invoke(this, new EventArgs());
        }

        private void CassetteChangedEvent(object? sender, EventArgs e)
        {
            Changed?.Invoke(this, new EventArgs());
        }
    }
}
