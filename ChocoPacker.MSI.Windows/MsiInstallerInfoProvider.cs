using System.IO;
using ChocoPacker.Common;
using static ChocoPacker.MSI.Windows.Constants;

namespace ChocoPacker.MSI.Windows
{
    [SupportsInstaller(MsiInstallerType)]
    public class MsiInstallerInfoProvider : MsiBase, IInstallerInfoProvider
    {
        public InstallerInfo GetInstallerInfo(string installerPath)
            => OpenMsi(installerPath, db => {
                var productCode = db.ReadProperty(ProductCodeProperty);
                var fileName = Path.GetFileName(installerPath);
                return new InstallerInfo
                {             
                    Author = db.ReadProperty(ManufacturerProperty),
                    ProductName = db.ReadProperty(ProductNameProperty),
                    ProductVersion = db.ReadProperty(ProductVersionProperty),
                    UninstallString = $"msiexec /x {productCode} /qn REBOOT=ReallySuppress",
                    InstallString = $"msiexec /i \"{fileName}\" /qn REBOOT=ReallySuppress"
                };
            });
    }
}