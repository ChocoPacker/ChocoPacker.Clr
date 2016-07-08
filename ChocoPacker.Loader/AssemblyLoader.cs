using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using ChocoPacker.Common;

namespace ChocoPacker.Loader
{
    internal class AssemblyLoader : IAssemblyLoader
    {
        private readonly IGeneralConfiguration _configuration;
        
        public AssemblyLoader(IGeneralConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));    
            }
            
            _configuration = configuration;
        }
        
        public IEnumerable<Assembly> LoadPluginAssemblies()
        {
            var assemblyPaths = Directory.GetFiles(_configuration.PluginsDirectory, 
                "*.dll", 
                SearchOption.AllDirectories);
            
            return assemblyPaths
                .Select(LoadAssembly)
                .Where(x => x != null);
        }
        
        private Assembly LoadAssembly(string path)
        {
            try 
            {
                var assemblyName = AssemblyLoadContext.GetAssemblyName(path);
                return Assembly.Load(assemblyName);
            } 
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Assembly '{path}' doesn't looks like assembly '{ex.Message}");
                return null;
            }
        }
    }
}