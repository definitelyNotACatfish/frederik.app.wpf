namespace frederik.app.wpf.Models
{
    public class LoadPort
    {
        /// <summary>
        /// Contains the loadport a cassette with wafers
        /// </summary>
        public bool CassetteInserted { get; private set; }

        public Cassette Cassette { get; private set; } = new Cassette();

        public event EventHandler? Changed;

        public async Task LoadCassette(int waferCount)
        {
            await Cassette.RemoveCassette();
            for (int i = 0; i < waferCount; i++) 
            {
                await Cassette.AddWafer(new Wafer());
            }
            CassetteInserted = true;
            Changed?.Invoke(this, new EventArgs());
        }

        public async Task UnloadCassette()
        {
            await Cassette.RemoveCassette();
            CassetteInserted = false;
            Changed?.Invoke(this, new EventArgs());
        }
    }
}
