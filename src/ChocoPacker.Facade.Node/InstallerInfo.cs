using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChocoPacker.Common;
using ChocoPacker.Loader;

namespace ChocoPacker.Facade.Node
{
    public static class InstallerInfo
    {
        public static Task<object> GetInstallerInfoAsync(dynamic input)
            => Task.Factory.StartNew<object>(() => GetInstallerInfo(input));

        private static object GetInstallerInfo(dynamic input)
        {
            string installerPath = input.InstallerPath;
            var loader = new PluginLoader(new GeneralConfiguration
            {
                PluginsDirectory = input.ChocoPackerPlugins,
                SevenZipPath = input.SevenZipPath
            });

            var typeDetectors = loader.GetPlugins<IInstallTypeDetector>();
            var installerType = typeDetectors.GetInstallerType(installerPath);
            var infoProvider = loader.GetInstallerPlugin<IInstallerInfoProvider>(installerType);

            var info = infoProvider.GetInstallerInfo(installerPath);
            return info;
        }

        private static string GetInstallerType(this IEnumerable<IInstallTypeDetector> installTypeDetectors, string installerPath)
        {
            var sb = new StringBuilder();
            foreach (var item in installTypeDetectors)
            {
                try
                {
                    return item.GetInstallerTypeName(installerPath);
                }
                catch (WrongInstallerTypeException ex)
                {
                    sb.AppendLine(ex.Message);
                }
            }

            throw new WrongInstallerTypeException($"Can't detect installer for: '{installerPath}'. Details: \n {sb.ToString()}");
        }
    }
}
