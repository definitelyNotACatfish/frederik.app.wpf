using frederik.app.wpf.Exceptions;

namespace frederik.app.wpf.Models
{
    /// <summary>
    /// Each cassete contains n wafers
    /// </summary>
    internal class Cassette
    {
        // Given by task that the maximum is 25
        public const int MAX_WAFERS = 25;

        public Cassette(int waferCount)
        {
            if (Wafers.Count > MAX_WAFERS)
            { throw new CassetteFullException("Cassette can't be inited with more than '{0}' wafers", MAX_WAFERS); }

            Init(waferCount);
        }

        public List<Wafer> Wafers { get; set; } = new List<Wafer>();

        public async Task AddWafer(Wafer wafer)
        {
            if (Wafers.Count > MAX_WAFERS)
            { throw new CassetteFullException("Cassette contains already the max count of '{0}' wafers", MAX_WAFERS); }
        
             Wafers.Add(wafer);
        }

        public async Task<Wafer> GetNextWafer()
        {
            if (Wafers.Count == 0)
            { throw new CassetteFullException("Cassette contains already the max count of '{0}' wafers", MAX_WAFERS); }

            // I assume i get the next wafer
            Wafer wafer = Wafers.First();
            Wafers.Remove(wafer);

            return wafer;
        }

        private void Init(int waferCount)
        {
            Wafers.Clear();

            for (int i = 0; i < waferCount; i++)
            { Wafers.Add(new Wafer()); }
        }
    }
}
