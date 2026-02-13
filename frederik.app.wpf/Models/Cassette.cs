using frederik.app.wpf.Exceptions;

namespace frederik.app.wpf.Models
{
    /// <summary>
    /// Each cassete contains n wafers
    /// </summary>
    public class Cassette
    {
        // Given by task that the maximum is 25
        public const int MAX_WAFERS = 25;

        public event EventHandler? WaferAddedEvent;

        public event EventHandler<Wafer>? NextWaferEvent;

        public List<Wafer> Wafers { get; private set; } = new List<Wafer>();

        public async Task AddWafer(Wafer wafer)
        {
            if (Wafers.Count > MAX_WAFERS)
            { throw new CassetteFullException("Cassette contains already the max count of '{0}' wafers", MAX_WAFERS); }
        
            Wafers.Add(wafer);
            WaferAddedEvent?.Invoke(this, new EventArgs());
        }

        public async Task<Wafer> GetWafer()
        {
            if (Wafers.Count == 0)
            { throw new CassetteFullException("Cassette contains already the max count of '{0}' wafers", MAX_WAFERS); }

            // I assume i get the next wafer
            Wafer wafer = Wafers.First();
            Wafers.Remove(wafer);

            NextWaferEvent?.Invoke(this, wafer);
            return wafer;
        }

        public async Task RemoveCassette()
        {
            Wafers.Clear();
        }

        private void Init(int waferCount)
        {
            Wafers.Clear();

            for (int i = 0; i < waferCount; i++)
            { Wafers.Add(new Wafer()); }
        }
    }
}
