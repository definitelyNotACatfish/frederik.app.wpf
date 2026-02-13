namespace frederik.app.wpf.Models
{
    internal class Station
    {
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
            await Task.Run(() => { Occupied = true; }); 
        }

        /// <summary>
        /// Do some stuff, if station is no longer occupied
        /// </summary>
        /// <returns></returns>
        public async Task DeOccupy()
        {
            await Task.Run(() => { Occupied = false; });
        }
    }
}
