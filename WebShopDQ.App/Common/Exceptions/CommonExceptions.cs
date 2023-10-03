using System.Globalization;

namespace WebShopDQ.App.Common.Exceptions
{
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class ValidateException : Exception
    {
        public ValidateException() : base() { }

        public ValidateException(string message) : base(message) { }

        public ValidateException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base() { }

        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class InvalidOperationException : Exception
    {
        public InvalidOperationException() : base() { }

        public InvalidOperationException(string message) : base(message) { }

        public InvalidOperationException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class DuplicateException : Exception
    {
        public DuplicateException() : base() { }

        public DuplicateException(string message) : base(message) { }

        public DuplicateException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class PasswordException : Exception
    {
        public PasswordException() : base() { }

        public PasswordException(string message) : base(message) { }

        public PasswordException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
