namespace ChocoPacker.Common
{
    public interface IInstallerInfoProvider
    {
        InstallerInfo GetInstallerInfo(string installerPath);
    }
}