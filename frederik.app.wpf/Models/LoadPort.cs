namespace frederik.app.wpf.Models
{
    internal class LoadPort
    {
        /// <summary>
        /// Contains the loadport a cassette with wafers
        /// </summary>
        public bool CassetteInserted => Cassette is not null;

        public Cassette? Cassette { get; private set; }

        /// <summary>
        /// Load a cassette into the machine
        /// </summary>
        /// <returns></returns>
        public async Task LoadCassette()
        { 
            Cassette = new Cassette(25);
        }


        /// <summary>
        /// Allow unloading a cassette if needed
        /// </summary>
        /// <returns></returns>
        public async Task UnloadCassette()
        {
            Cassette = null;
        }
    }
}
