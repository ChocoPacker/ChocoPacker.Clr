namespace ChocoPacker.Common
{
    public interface IGeneralConfiguration
    {
        string SevenZipPath { get; }
        
        string PluginsDirectory { get; }
    }
}