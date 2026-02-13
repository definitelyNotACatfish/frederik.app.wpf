namespace frederik.app.wpf.Exceptions
{
    public class NoWaferLoadedException : Exception
    {
        public NoWaferLoadedException()
        {
        }

        public NoWaferLoadedException(string? message) : base(message)
        {
        }

        public NoWaferLoadedException(string message, params object?[] args) : base(string.Format(message, args))
        {
        }
    }
}
