using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DB_DevicePriority
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = "NA";
        public string DisplayText { get; set; } = "NA";
        public string DeviceName { get; set; } = "NA";
        public bool EnforceDefault { get; set; } = false;
        public bool IsDefault { get; set; } = false;
        public int Priority { get; set; }
    }

    public partial class Srv_DB
    {
        public ILiteCollection<DB_DevicePriority> DevicePriority_GetAll()
        {
            return this.G_DB_DevicePriority;
        }


        public DB_DevicePriority DevicePriority_GetOne(string P_Name)
        {
            return this.G_DB_DevicePriority.FindOne(a => a.Name == P_Name);
        }

        public DB_DevicePriority DevicePriority_AddNew(string P_Name)
        {
            DB_DevicePriority L_MWM_DB_DevicePriority = new DB_DevicePriority
            {
                Name = P_Name,
            };

            DevicePriority_Save(L_MWM_DB_DevicePriority);

            return L_MWM_DB_DevicePriority;
        }

        public DB_DevicePriority DevicePriority_GetOneOrAdd(string P_Name, string P_DisplayText, string P_DeviceName, int P_Priority)
        {
            DB_DevicePriority L_MWM_DB_DevicePriority = DevicePriority_GetOne(P_Name);
            if (L_MWM_DB_DevicePriority == null)
            {
                L_MWM_DB_DevicePriority = new DB_DevicePriority
                {
                    Priority = P_Priority,
                    Name = P_Name,
                    DisplayText = P_DisplayText,
                    DeviceName = P_DeviceName
                };
                DevicePriority_Save(L_MWM_DB_DevicePriority);
            }

            return L_MWM_DB_DevicePriority;
        }

        public DB_DevicePriority DevicePriority_GetOneOrAdd(string P_Name, string P_DisplayText, string P_DeviceName)
        {
            DB_DevicePriority L_MWM_DB_DevicePriority = DevicePriority_GetOne(P_Name);
            if (L_MWM_DB_DevicePriority == null)
            {
                L_MWM_DB_DevicePriority = new DB_DevicePriority
                {
                    Priority = G_DB_DevicePriority.Count(),
                    Name = P_Name,
                    DisplayText = P_DisplayText,
                    DeviceName = P_DeviceName
                };
                DevicePriority_Save(L_MWM_DB_DevicePriority);
            }

            return L_MWM_DB_DevicePriority;
        }

        public void DevicePriority_Save(DB_DevicePriority P_MWM_DB_DevicePriority)
        {
            if (Guid.Empty.Equals(P_MWM_DB_DevicePriority.Id))
            {
                P_MWM_DB_DevicePriority.Id = Guid.NewGuid();
                this.G_DB_DevicePriority.Insert(P_MWM_DB_DevicePriority);
            }
            else
            {
                this.G_DB_DevicePriority.Update(P_MWM_DB_DevicePriority);
            }
        }

        public void DevicePriority_Delete(DB_DevicePriority P_MWM_DB_DevicePriority)
        {
            this.G_DB_DevicePriority.Delete(P_MWM_DB_DevicePriority.Id);
        }

    }
}
