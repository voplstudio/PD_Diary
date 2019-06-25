using System;
using System.IO;
using Windows.Storage;
using Xamarin.Forms;
using PD_Diary.UWP;
using Windows.ApplicationModel.Activation;
using PD_Diary.Services;
using Windows.ApplicationModel;

[assembly: Dependency(typeof(SQLite_UWP))]
namespace PD_Diary.UWP
{
    public class SQLite_UWP : ISQLite
    {
        public SQLite_UWP() { }
        public string GetDatabasePath(string sqliteFilename)
        {
            // для доступа к файлам используем API Windows.Storage
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            return path;
        }
    }
}
