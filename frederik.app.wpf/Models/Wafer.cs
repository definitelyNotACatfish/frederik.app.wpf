namespace frederik.app.wpf.Models
{
    /// <summary>
    /// Empty for future use, if the wafer should contain any additional info, like status or so...
    /// </summary>
    public class Wafer
    {
        public Wafer(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = string.Empty;
    }
}
