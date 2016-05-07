using System;

namespace ChocoPacker.Common
{
    public class UnsupportedPluginException : Exception
    {
        public UnsupportedPluginException(string message)
            : base(message)
        {
        }
        
        public UnsupportedPluginException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}