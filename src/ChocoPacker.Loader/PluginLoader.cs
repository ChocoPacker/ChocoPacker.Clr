using System.Linq;
using ChocoPacker.Common;
using Autofac;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace ChocoPacker.Loader
{
    public class PluginLoader
    {
        private struct Plugin 
        {
            public Type Type;
            public Type[] Plugins;
            public string Name;
        }
        
        private static readonly IReadOnlyCollection<Type> SupportedPlugins 
            = new [] { typeof(IInstallerInfoProvider), typeof(IInstallTypeDetector) };
        
        private readonly IAssemblyLoader _assemblyLoader;
        
        private readonly IContainer _container;
        
        public PluginLoader(GeneralConfiguration config) 
            : this(new AssemblyLoader(config), config)
        {
        }
        
        internal PluginLoader(IAssemblyLoader loader, IGeneralConfiguration config)
        {
            _assemblyLoader = loader;
            var assemblies = _assemblyLoader
                .LoadPluginAssemblies()
                .ToArray();
                
            var builder = new ContainerBuilder();
            RegisterPlugins(assemblies, builder);
            builder.RegisterInstance<IGeneralConfiguration>(config);
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(x => !SupportedPlugins.Any(i => i.GetTypeInfo().IsAssignableFrom(x)))
                   .AsImplementedInterfaces()
                   .PreserveExistingDefaults();
            _container = builder.Build();
        }
        
        private void RegisterPlugins(IReadOnlyCollection<Assembly> assemblies, ContainerBuilder builder)
        {
            var pluginTypes = assemblies.SelectMany(x => x.GetTypes())
                .Where(x => !x.GetTypeInfo().IsAbstract && SupportedPlugins.Any(i => i.GetTypeInfo().IsAssignableFrom(x)))
                .Select(x => new Plugin{
                    Type = x,
                    Plugins = SupportedPlugins.Where(i => i.GetTypeInfo().IsAssignableFrom(x)).ToArray(),
                    Name = x.GetTypeInfo().CustomAttributes.Any(a => a.AttributeType == typeof(SupportsInstallerAttribute))
                        ? x.GetTypeInfo().GetCustomAttribute<SupportsInstallerAttribute>().InstallerType
                        : null
                })
                .ToArray();
                
            foreach (var pluginType in pluginTypes)
            {
                if (string.IsNullOrEmpty(pluginType.Name))
                {
                    RegisterSimplePlugin(pluginType, builder);
                    continue;
                }
                
                RegisterNamedPlugin(pluginType, builder);
            }
        }
        
        private void RegisterSimplePlugin(Plugin plugin, ContainerBuilder builder)
            => plugin.Plugins.ForEach(x => builder.RegisterType(plugin.Type).As(x));
        
        private void RegisterNamedPlugin(Plugin plugin, ContainerBuilder builder)
            => plugin.Plugins.ForEach(x =>
                builder.RegisterType(plugin.Type)
                       .As(x)
                       .Named($"{x.FullName}_{plugin.Name}", x));
        
        public IEnumerable<T> GetPlugins<T>() =>  _container.Resolve<IEnumerable<T>>();
        
        public T GetInstallerPlugin<T>(string installerTypeName)
        {
            var name = $"{typeof(T).FullName}_{installerTypeName}";
            try
            {
                return _container.ResolveNamed<T>(name); 
            }
            catch (Exception ex)
            {
                throw new UnsupportedPluginException($"Can't load plugin of type '{typeof(T).FullName}' with name: '{name}'. {ex.Message}", ex);    
            }
        }
    }
}