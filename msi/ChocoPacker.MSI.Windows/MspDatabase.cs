using Microsoft.Deployment.WindowsInstaller;
using Microsoft.Deployment.WindowsInstaller.Linq;

namespace ChocoPacker.MSI.Windows
{   
    internal class MspDatabase : QDatabase
    {
        public QTable<MsiPatchMetadata> MsiPatchMetadata => new QTable<MsiPatchMetadata>(this);
        
        public MspDatabase(Database db)
            : base(db.Handle, true, db.FilePath, db.OpenMode)
        {
        }
    }
}