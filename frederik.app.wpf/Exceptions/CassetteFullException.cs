namespace frederik.app.wpf.Exceptions
{
    public class CassetteFullException : Exception
    {
        public CassetteFullException(string? message) : base(message)
        {
        }

        public CassetteFullException(string message, params object?[] args) : base(string.Format(message, args))
        {
        }
    }
}
