using ChocoPacker.Common;
using Xunit;

namespace ChocoPacker.MSI.Windows.Tests
{
    public class MspInstallerInfoProviderTests
    {
        [Fact]
        public void GetInstallerInfo_UninstallablePath_ProperInfo()
        {
            var mspInfoProvider = new MspInstallerInfoProvider();
            var info = mspInfoProvider.GetInstallerInfo("patch.msp".GetTestFilePath());
            Assert.Equal("Dynamo Corp", info.Author);
            Assert.Equal("msiexec /p \"patch.msp\" /qn REBOOT=ReallySuppress", info.InstallString);
            Assert.Equal("Sample Patch", info.ProductName);
            Assert.Equal("msiexec /i {48C49ACE-90CF-4161-9C6E-9162115A54DD} /qn MSIPATCHREMOVE={224C316C-5894-4771-BABF-21A3AC1F75FF} REBOOT=ReallySuppress", info.UninstallString);
            Assert.Null(info.ProductVersion);
        }
        
        [Fact]
        public void GetInstallerInfo_NonUninstallablePath_ProperInfo()
        {
            var mspInfoProvider = new MspInstallerInfoProvider();
            var info = mspInfoProvider.GetInstallerInfo("patch_no_remove.msp".GetTestFilePath());
            Assert.Equal("Dynamo Corp", info.Author);
            Assert.Equal("msiexec /p \"patch_no_remove.msp\" /qn REBOOT=ReallySuppress", info.InstallString);
            Assert.Equal("Sample Patch", info.ProductName);
            Assert.Equal(string.Empty, info.UninstallString);
            Assert.Null(info.ProductVersion);
        }
        
        [Fact]
        public void GetInstallerInfo_InvalidPatch_WrongInstallerTypeException()
        {
            var mspInfoProvider = new MspInstallerInfoProvider();
            Assert.Throws<WrongInstallerTypeException>(
                () => mspInfoProvider.GetInstallerInfo("NTVS 1.2 Alpha VS 2015.msi".GetTestFilePath()));
        }
    }
}