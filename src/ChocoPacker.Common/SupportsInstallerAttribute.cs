using System;

namespace ChocoPacker.Common
{
    [AttributeUsageAttribute(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class SupportsInstallerAttribute : Attribute
    {
        public string InstallerType { get; }

        public SupportsInstallerAttribute(string installerType)
        {
            if (string.IsNullOrEmpty(installerType))
            {
                throw new ArgumentNullException(nameof(installerType));
            }

            InstallerType = installerType;
        }
    }
}