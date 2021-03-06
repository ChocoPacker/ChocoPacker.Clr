using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ChocoPacker.Common;
using static ChocoPacker.MSI.Windows.Constants;

namespace ChocoPacker.MSI.Windows
{
    [SupportsInstaller(MspInstallerType)]
    public class MspInstallerInfoProvider : MsiBase, IInstallerInfoProvider
    {
        private static readonly Regex GuidRegex = new Regex("({[\\w|-]+})", 
            RegexOptions.Compiled 
            | RegexOptions.Singleline 
            | RegexOptions.IgnoreCase);
    
        
        public InstallerInfo GetInstallerInfo(string installerPath)
            => base.OpenMsi(installerPath, db => {
               var mspName = Path.GetFileName(installerPath);
               var uninstallArgs = GetUninstallString(db.AsMspQueryable());
               return new InstallerInfo
               {
                   Author = db.ReadMspProperty(MspManufacturerProperty),
                   ProductName = db.ReadMspProperty(MspProductNameProperty),
                   UninstallExecutable = string.IsNullOrEmpty(uninstallArgs) 
                        ? string.Empty 
                        : WindowsInstallerExecutable,
                   InstallExecutable = WindowsInstallerExecutable,
                   InstallArguments = $"/p \"{mspName}\" /qn REBOOT=ReallySuppress",
                   UninstallArguments = uninstallArgs
               };
            });
            
        private string GetUninstallString(MspDatabase db)
        {
            var allowRemoval = db.ReadMspProperty(MspAllowRemovalProperty);
            if (allowRemoval != "1")
            {
                return string.Empty;
            }
            
            var match = GuidRegex.Match(db.SummaryInfo.RevisionNumber);
            var patchId = match.Groups?[0]?.Value;
            var productCodes = db.SummaryInfo.Template.Split(';');
            return productCodes.Length != 1 || !GuidRegex.IsMatch(productCodes.First()) || string.IsNullOrEmpty(patchId)
                ? string.Empty
                : $"/i {productCodes.First()} /qn MSIPATCHREMOVE={patchId} REBOOT=ReallySuppress";
        }
    }
}