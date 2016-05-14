using System.IO;

namespace ChocoPacker.MSI.Windows.Tests
{
    internal static class TestUtils
    {
        public static string GetTestFilePath(this string name)
            => Path.Combine(Directory.GetCurrentDirectory(), "TestFiles", name);
    } 
}