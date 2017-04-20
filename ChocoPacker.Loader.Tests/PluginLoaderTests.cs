using System.Linq;
using System.Reflection;
using ChocoPacker.Common;
using ChocoPacker.Loader.Tests.Stubs;
using Moq;
using Xunit;

namespace ChocoPacker.Loader.Tests
{
    public class PluginLoaderTests
    {
        [Fact]
        public void GetPlugins_Load_All_Plugins_From_Assemblies()
        {
            var pluginLoader = InitializePluginLoader();
            var plugins = pluginLoader.GetPlugins<IInstallerInfoProvider>().ToArray();
            Assert.Equal(3, plugins.Length);
        }
        
        [Fact]
        public void GetInstallerPlugin_Load_Proper_Plugin()
        {
            var pluginLoader = InitializePluginLoader();
            var plugin = pluginLoader.GetInstallerPlugin<IInstallerInfoProvider>("NSIS");     
            Assert.Equal(typeof(TestInstallerInfoProviderNsis), plugin.GetType());       
        }
        
        [Fact]
        public void GetInstallerPlugin_Load_Bad_Plugin_UnsupportedPluginException_Thrown()
        {
            var pluginLoader = InitializePluginLoader();
            Assert.Throws<UnsupportedPluginException>(() => pluginLoader.GetInstallerPlugin<IInstallerInfoProvider>("Trash"));     
        }
        
        [Fact]
        public void GetPlugins_Load_Bad_Plugin_Empty_Result()
        {
            var pluginLoader = InitializePluginLoader();
            var plugins = pluginLoader.GetPlugins<IUnsupportedPlugin>().ToArray();     
            Assert.Empty(plugins);
        }
        
        private PluginLoader InitializePluginLoader()
        {
            var assemblyLoaderMock = new Mock<IAssemblyLoader>();
            assemblyLoaderMock.Setup(x => x.LoadPluginAssemblies())
                .Returns(new[] { typeof(TestInstallerInfoProvider).GetTypeInfo().Assembly });
            var pluginLoader = new PluginLoader(assemblyLoaderMock.Object, new GeneralConfiguration());
            return pluginLoader;
        }
    }
}