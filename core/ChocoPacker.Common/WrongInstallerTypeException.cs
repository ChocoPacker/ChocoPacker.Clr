using System;

namespace ChocoPacker.Common
{
    public class WrongInstallerTypeException : Exception
    {
        public WrongInstallerTypeException(string message)
            : base(message)
        {
        }
        
        public WrongInstallerTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}