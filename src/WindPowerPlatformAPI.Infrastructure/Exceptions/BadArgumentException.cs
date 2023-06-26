using System;

namespace WindPowerPlatformAPI.Infrastructure.Exceptions
{
    public class BadArgumentException : ArgumentNullException
    {
        public BadArgumentException()
        {
        }

        public BadArgumentException(string message) : base(message)
        {
        }

        public BadArgumentException(string message, string parameterName) : base(parameterName, message)
        {
        }
    }
}
