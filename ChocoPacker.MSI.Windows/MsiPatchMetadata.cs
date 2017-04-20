using Microsoft.Deployment.WindowsInstaller.Linq;

namespace ChocoPacker.MSI.Windows
{
    [DatabaseTable("MsiPatchMetadata")]
    internal class MsiPatchMetadata : QRecord
    {
        public string Company 
        { 
            get 
            { 
                return this[0];
            }
            
            set
            {
                this[0] = value;
            }
        }
        
        public string Property 
        { 
            get 
            { 
                return this[1];
            }
            
            set
            {
                this[1] = value;
            }
        }
        
        public string Value 
        { 
            get 
            { 
                return this[2];
            }
            
            set
            {
                this[2] = value;
            }
        }
    }
}