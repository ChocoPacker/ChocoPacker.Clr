using System.Linq;
using Microsoft.Deployment.WindowsInstaller;
using Xunit;

namespace ChocoPacker.MSI.Windows.Tests
{
    public class MspDatabaseTests
    {
        [Fact]
        public void MsiPatchMetadata_ReturnProperCollection()
        {
            var db = new Database("patch.msp".GetTestFilePath());
            var mDb = db.AsMspQueryable();
            var results = mDb.MsiPatchMetadata
                .Where(x => x.Company == null)
                .Select(x => new { x.Property, x.Value })
                .ToArray();
                
           Assert.Equal(8, results.Length);
           Assert.Equal("Sample Patch", results.Single(x => x.Property == "DisplayName").Value);
           Assert.Equal("Small Update Patch", results.Single(x => x.Property == "Description").Value);
           Assert.Equal("1", results.Single(x => x.Property == "AllowRemoval").Value);
           Assert.Equal("Update", results.Single(x => x.Property == "Classification").Value);
           Assert.Equal("Dynamo Corp", results.Single(x => x.Property == "ManufacturerName").Value);
           Assert.Equal("http://www.dynamocorp.com/", results.Single(x => x.Property == "MoreInfoURL").Value);
           Assert.Equal("Sample", results.Single(x => x.Property == "TargetProductName").Value);
           Assert.Equal("05-13-2016 04:18", results.Single(x => x.Property == "CreationTimeUTC").Value);
        }
    }
}