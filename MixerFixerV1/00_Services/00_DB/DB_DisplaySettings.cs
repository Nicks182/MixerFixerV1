using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DB_DisplaySettings
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = "NA";
        public string DisplayName { get; set; } = "NA";
        public bool IsManaged { get; set; } = false;
        public bool IsAttached { get; set; } = false;
        public int Screen_Pos_Y { get; set; }
        public int Screen_Pos_X { get; set; }
        public int Screen_Res_Y { get; set; }
        public int Screen_Res_X { get; set; }
    }

    public partial class Srv_DB
    {
        public ILiteCollection<DB_DisplaySettings> DisplaySettings_GetAll()
        {
            return this.G_DB_DisplaySettings;
        }


        public DB_DisplaySettings DisplaySettings_GetOne(string P_Name)
        {
            return this.G_DB_DisplaySettings.FindOne(a => a.Name == P_Name);
        }

        public DB_DisplaySettings DisplaySettings_GetOne_ById(Guid P_Id)
        {
            return this.G_DB_DisplaySettings.FindOne(a => a.Id == P_Id);
        }

        public void DisplaySettings_Save(DB_DisplaySettings P_MWM_DB_DisplaySettings)
        {
            if (Guid.Empty.Equals(P_MWM_DB_DisplaySettings.Id))
            {
                P_MWM_DB_DisplaySettings.Id = Guid.NewGuid();
                this.G_DB_DisplaySettings.Insert(P_MWM_DB_DisplaySettings);
            }
            else
            {
                this.G_DB_DisplaySettings.Update(P_MWM_DB_DisplaySettings);
            }
        }

        public void DisplaySettings_Delete(DB_DisplaySettings P_MWM_DB_DisplaySettings)
        {
            this.G_DB_DisplaySettings.Delete(P_MWM_DB_DisplaySettings.Id);
        }

    }
}
