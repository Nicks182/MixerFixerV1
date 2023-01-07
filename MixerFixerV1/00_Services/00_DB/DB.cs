using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public partial class Srv_DB
    {
        const string G_DB_Name = "MWM_DB";
        private LiteDatabase G_DB;

        private ILiteCollection<DB_AudioObject> G_DB_AudioObject;
        private ILiteCollection<DB_DevicePriority> G_DB_DevicePriority;
        private ILiteCollection<DB_Settings> G_DB_Settings;
        private ILiteCollection<DB_Theme> G_DB_Theme;


        public Srv_DB()
        {
            G_DB = new LiteDatabase(GetAppPath());
            
            this.G_DB_AudioObject = G_DB.GetCollection<DB_AudioObject>("AudioObject");
            this.G_DB_DevicePriority = G_DB.GetCollection<DB_DevicePriority>("DevicePriority");
            this.G_DB_Settings = G_DB.GetCollection<DB_Settings>("Settings");
            this.G_DB_Theme = G_DB.GetCollection<DB_Theme>("Theme");
        }

        public void Dispose()
        {
            G_DB.Dispose();
        }

        private string GetAppPath()
        {
            return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory , G_DB_Name);
        }

        public void ClearDB()
        {
            this.G_DB_Settings.DeleteAll();
            this.G_DB_AudioObject.DeleteAll();
            //this.G_DB_Apps.DeleteAll();
            //this.G_DB_Devices.DeleteAll();
        }

    }
}
