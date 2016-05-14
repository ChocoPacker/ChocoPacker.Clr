using System.Linq;
using Microsoft.Deployment.WindowsInstaller;

namespace ChocoPacker.MSI.Windows
{
    internal static class DatabaseExtensions
    {
        public static string ReadProperty(this Database db, string propertyName)
            => db.AsMspQueryable().Properties?.SingleOrDefault(x => x.Property == propertyName)?.Value;
            
        public static string ReadMspProperty(this Database db, string propertyName)
            => db.AsMspQueryable()
                .MsiPatchMetadata?
                .SingleOrDefault(x => x.Company == string.Empty && x.Property == propertyName)?
                .Value;
            
        public static MspDatabase AsMspQueryable(this Database db) 
            => db is MspDatabase 
            ? (MspDatabase)db 
            : new MspDatabase(db);
    }
}