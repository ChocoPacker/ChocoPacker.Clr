namespace ChocoPacker.Common
{
    public class InstallerInfo
    {
        public string ProductName { get; set; }

        public string ProductVersion { get; set; }

        public string Author { get; set; }

        public string InstallExecutable { get; set; }
        
        public string InstallArguments { get; set; }

        public string UninstallExecutable { get; set; }
        
        public string UninstallArguments { get; set; }
    }
}