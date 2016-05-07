namespace ChocoPacker.Common
{
    public interface IInstallTypeDetector
    {
        // Gets installer type name        
        string GetInstallerTypeName(string installerPath);
    }
}