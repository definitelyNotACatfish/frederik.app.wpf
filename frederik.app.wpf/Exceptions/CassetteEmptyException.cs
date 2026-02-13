namespace frederik.app.wpf.Exceptions
{
    public class CassetteEmptyException : Exception
    {
        public CassetteEmptyException(string? message) : base(message)
        {
        }

        public CassetteEmptyException(string message, params object?[] args) : base(string.Format(message, args))
        {
        }
    }
}
