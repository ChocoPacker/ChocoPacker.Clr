using System;
using ChocoPacker.Common;

namespace ChocoPacker.Loader.Tests.Stubs
{
    // This classes for test purposes only
    public class TestInstallerInfoProvider : IInstallerInfoProvider
    {
        public InstallerInfo GetInstallerInfo(string installerPath)
        {
            throw new NotImplementedException();
        }
    }
    
    [SupportsInstaller("Burn")]
    public class TestInstallerInfoProviderBurn : IInstallerInfoProvider
    {
        public TestInstallerInfoProviderBurn(IGeneralConfiguration configuration)
        {
        }
        
        public InstallerInfo GetInstallerInfo(string installerPath)
        {
            throw new NotImplementedException();
        }
    }
    
    [SupportsInstaller("NSIS")]
    public class TestInstallerInfoProviderNsis : IInstallerInfoProvider
    {
        public TestInstallerInfoProviderNsis(IGeneralConfiguration configuration)
        {
        }
        
        public InstallerInfo GetInstallerInfo(string installerPath)
        {
            throw new NotImplementedException();
        }
    }
    
    public interface IUnsupportedPlugin
    {
    }
}