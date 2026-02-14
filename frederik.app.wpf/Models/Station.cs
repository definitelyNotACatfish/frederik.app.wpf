namespace frederik.app.wpf.Models
{
    public class Station
    {
        public event EventHandler? Changed;

        public bool Occupied { get; private set; }

        public string Name { get; private set; }

        public Station(string name)
        {
            Name = name;
            Occupied = false;
        }

        /// <summary>
        ///  Do some stuffe if the station is occupied
        /// </summary>
        /// <returns></returns>
        public async Task Occupy()
        { 
            Occupied = true;
            Changed?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Do some stuff, if station is no longer occupied
        /// </summary>
        /// <returns></returns>
        public async Task DeOccupy()
        {
            Occupied = false;
            Changed?.Invoke(this, new EventArgs());
        }
    }
}
