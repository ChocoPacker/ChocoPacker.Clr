using ChocoPacker.Common;
using Xunit;
using static ChocoPacker.MSI.Windows.Constants;

namespace ChocoPacker.MSI.Windows.Tests
{
    public class MsiInstallTypeDetectorTests
    {
        [Theory]
        [InlineData("NTVS 1.2 Alpha VS 2015.msi", MsiInstallerType)]
        [InlineData("patch.msp", MspInstallerType)]        
        public void GetInstallerTypeName_ProperFile_ReturnsInstallerType(string path, string expected)
        {
            var typeDetector = new MsiInstallTypeDetector();
            Assert.Equal(expected, typeDetector.GetInstallerTypeName(path.GetTestFilePath()));
        }
        
        [Fact]    
        public void GetInstallerTypeName_BadFile_WrongInstallerTypeException()
        {
            var typeDetector = new MsiInstallTypeDetector();
            Assert.Throws<WrongInstallerTypeException>(
                () => typeDetector.GetInstallerTypeName("bad.msi".GetTestFilePath()));
        }
    }
}