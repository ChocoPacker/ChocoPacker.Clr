using Microsoft.DotNet.PlatformAbstractions;
using System.IO;

namespace ChocoPacker.MSI.Windows.Tests
{
    internal static class TestUtils
    {
        public static string GetTestFilePath(this string name)
            => Path.Combine(ApplicationEnvironment.ApplicationBasePath, 
                "TestFiles", 
                name);
    } 
}