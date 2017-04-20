using ChocoPacker.Common;
using Xunit;

namespace ChocoPacker.MSI.Windows.Tests
{
    public class MsiInstallerInfoProviderTests
    {
        [Fact]
        public void GetInstallerInfo_CorrectMsi_DataObtained()
        {
            var infoProvider = new MsiInstallerInfoProvider();
            var info = infoProvider.GetInstallerInfo("NTVS 1.2 Alpha VS 2015.msi".GetTestFilePath());
            Assert.Equal("Microsoft Corporation", info.Author);
            Assert.Equal("/i \"NTVS 1.2 Alpha VS 2015.msi\" /qn REBOOT=ReallySuppress", info.InstallArguments);
            Assert.Equal("Node.js Tools 1.2 Alpha for Visual Studio 2015", info.ProductName);
            Assert.Equal("1.2.40330.02", info.ProductVersion);
            Assert.Equal("/x {8FD8CF77-9630-4CDA-98A6-6BEA8A85F0A9} /qn REBOOT=ReallySuppress", info.UninstallArguments);   
            Assert.Equal("msiexec", info.InstallExecutable);
            Assert.Equal("msiexec", info.UninstallExecutable);                                                                                                      
        }
        
        [Fact]    
        public void GetInstallerInfo_BadFile_WrongInstallerTypeException()
        {
            var typeDetector = new MsiInstallerInfoProvider();
            Assert.Throws<WrongInstallerTypeException>(
                () => typeDetector.GetInstallerInfo("bad.msi".GetTestFilePath()));
        }
    }
}