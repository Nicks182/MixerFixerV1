using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DB_AudioObject
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = "NA";
        public bool IsManaged { get; set; } = false;
        public bool IsDefault { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public bool IsMute { get; set; } = false;
        public bool IsDevice { get; set; } = false;
        public double Volume { get; set; }
        public int Priority { get; set; }
        public int DisplayOrder { get; set; }
    }

    public partial class Srv_DB
    {
        public ILiteCollection<DB_AudioObject> AudioObject_GetAll()
        {
            return this.G_DB_AudioObject;
        }


        public DB_AudioObject AudioObject_GetOne(string P_Name)
        {
            return this.G_DB_AudioObject.FindOne(a => a.Name == P_Name);
        }

        public DB_AudioObject AudioObject_AddNew(string P_Name)
        {
            DB_AudioObject L_MWM_DB_AudioObject = new DB_AudioObject
            {
                Name = P_Name,
            };

            AudioObject_Save(L_MWM_DB_AudioObject);

            return L_MWM_DB_AudioObject;
        }

        public DB_AudioObject AudioObject_GetOneOrAdd(string P_Name, int P_Priority)
        {
            DB_AudioObject L_MWM_DB_AudioObject = AudioObject_GetOne(P_Name);
            if (L_MWM_DB_AudioObject == null)
            {
                L_MWM_DB_AudioObject = new DB_AudioObject
                {
                    Priority = P_Priority,
                    Name = P_Name
                };
                AudioObject_Save(L_MWM_DB_AudioObject);
            }

            return L_MWM_DB_AudioObject;
        }

        public DB_AudioObject AudioObject_GetOneOrAdd(string P_Name)
        {
            DB_AudioObject L_MWM_DB_AudioObject = AudioObject_GetOne(P_Name);
            if (L_MWM_DB_AudioObject == null)
            {
                L_MWM_DB_AudioObject = new DB_AudioObject
                {
                    Priority = G_DB_AudioObject.Count(),
                    Name = P_Name
                };
                AudioObject_Save(L_MWM_DB_AudioObject);
            }

            return L_MWM_DB_AudioObject;
        }

        public void AudioObject_Save(DB_AudioObject P_MWM_DB_AudioObject)
        {
            if (Guid.Empty.Equals(P_MWM_DB_AudioObject.Id))
            {
                P_MWM_DB_AudioObject.Id = Guid.NewGuid();
                this.G_DB_AudioObject.Insert(P_MWM_DB_AudioObject);
            }
            else
            {
                this.G_DB_AudioObject.Update(P_MWM_DB_AudioObject);
            }
        }

        public void AudioObject_Delete(DB_AudioObject P_MWM_DB_AudioObject)
        {
            this.G_DB_AudioObject.Delete(P_MWM_DB_AudioObject.Id);
        }

    }
}
