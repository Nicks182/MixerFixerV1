using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DB_Settings
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = "NA";
        //public bool ApplyDefaultAppVolume { get; set; } = false;
        //public double DefaultAppVolume { get; set; }
        public string Value { get; set; }
    }


    public partial class Srv_DB
    {
        public string G_DefaultVolume = "Default App Volume";
        public string G_DefaultVolumeEnable = "Default App Volume Enable";
        public ILiteCollection<DB_Settings> Settings_GetAll()
        {
            if(this.G_DB_Settings.Count() == 0)
            {
                Settings_SetDefaults();
            }
            return this.G_DB_Settings;
        }

        private void Settings_SetDefaults()
        {
            //MWM_DB_Settings L_MWM_DB_Settings = new MWM_DB_Settings
            //{
            //    ApplyDefaultAppVolume = false,
            //    DefaultAppVolume = 25,
            //    Name = G_DefaultVolumeName
            //};

            
            Settings_Save(new DB_Settings
            {
                Name = G_DefaultVolumeEnable,
                Value = "1"
            });

            Settings_Save(new DB_Settings
            {
                Name = G_DefaultVolume,
                Value = "10"
            });
        }

        public DB_Settings Settings_GetOne(string P_Name)
        {
            if (this.G_DB_Settings.Count() == 0)
            {
                Settings_SetDefaults();
            }
            return this.G_DB_Settings.FindOne(a => a.Name == P_Name);
        }

        public void Settings_AddNew(string P_Name)
        {
            DB_Settings L_MWM_DB_Settings = new DB_Settings
            {
                Name = P_Name,
            };

            Settings_Save(L_MWM_DB_Settings);
        }

        public void Settings_Save(DB_Settings P_MWM_DB_Settings)
        {
            if (Guid.Empty.Equals(P_MWM_DB_Settings.Id))
            {
                P_MWM_DB_Settings.Id = Guid.NewGuid();
                this.G_DB_Settings.Insert(P_MWM_DB_Settings);
            }
            else
            {
                this.G_DB_Settings.Update(P_MWM_DB_Settings);
            }
        }

        public void Settings_Delete(DB_Settings P_MWM_DB_Settings)
        {
            this.G_DB_Settings.Delete(P_MWM_DB_Settings.Id);
        }

    }
}
