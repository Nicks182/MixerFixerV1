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

        //private ILiteCollection<DB_Apps> G_DB_Apps;

        //private ILiteCollection<DB_Devices> G_DB_Devices;

        private ILiteCollection<DB_Settings> G_DB_Settings;


        public Srv_DB()
        {
            G_DB = new LiteDatabase(GetAppPath());
            
            this.G_DB_AudioObject = G_DB.GetCollection<DB_AudioObject>("AudioObject");
            //this.G_DB_Apps = G_DB.GetCollection<DB_Apps>("Apps");
            //this.G_DB_Devices = G_DB.GetCollection<DB_Devices>("Devices");
            this.G_DB_Settings = G_DB.GetCollection<DB_Settings>("Settings");

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
