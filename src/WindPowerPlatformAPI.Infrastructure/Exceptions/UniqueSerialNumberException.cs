using System;

namespace WindPowerPlatformAPI.Infrastructure.Exceptions
{
    public class UniqueSerialNumberException : Exception
    {
        public UniqueSerialNumberException()
        {
        }

        public UniqueSerialNumberException(string message) : base(message)
        {
        }
    }
}
