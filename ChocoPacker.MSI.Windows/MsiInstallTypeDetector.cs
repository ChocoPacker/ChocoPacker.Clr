using System;
using ChocoPacker.Common;
using static ChocoPacker.MSI.Windows.Constants;

namespace ChocoPacker.MSI.Windows
{
    public class MsiInstallTypeDetector : MsiBase, IInstallTypeDetector
    {
        public string GetInstallerTypeName(string installerPath)
            => base.OpenMsi(installerPath, x => 
                {
                    if (installerPath.EndsWith(".msi", StringComparison.OrdinalIgnoreCase))
                    {
                        return MsiInstallerType;
                    }
                    
                    if (installerPath.EndsWith(".msp", StringComparison.OrdinalIgnoreCase))
                    {
                        return MspInstallerType;
                    }
                    
                    throw new WrongInstallerTypeException($"Installer '{installerPath}' has unsupported extension");
                });
    }
}