using System.Collections.Generic;
using System.Reflection;

namespace ChocoPacker.Loader
{
    internal interface IAssemblyLoader
    {
        IEnumerable<Assembly> LoadPluginAssemblies();
    }
}