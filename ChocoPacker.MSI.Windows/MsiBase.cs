using System;
using ChocoPacker.Common;
using Microsoft.Deployment.WindowsInstaller;

namespace ChocoPacker.MSI.Windows
{
    public abstract class MsiBase
    {
        protected T OpenMsi<T>(string msiPath, Func<Database, T> func)
        {
            Database db = null;
            try
            {
                db = new Database(msiPath, DatabaseOpenMode.ReadOnly);
                return func(db);
            }
            catch (WrongInstallerTypeException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new WrongInstallerTypeException($"Installer '{msiPath}' doesn't look like Windows Installer", ex);
            }
            finally
            {
                db?.Dispose();
            }
        }
    }
}