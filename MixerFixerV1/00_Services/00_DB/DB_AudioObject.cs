﻿using LiteDB;
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
        public string SessionId_Base64 { get; set; } = "NA";
        public string Name { get; set; } = "NA";
        public bool IsManaged { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public bool IsIgnore { get; set; } = false;
        public bool IsMute { get; set; } = false;
        public bool IsDevice { get; set; } = false;
        public int Volume { get; set; }
    }

    public partial class Srv_DB
    {
        public ILiteCollection<DB_AudioObject> AudioObject_GetAll()
        {
            return this.G_DB_AudioObject;
        }


        public DB_AudioObject AudioObject_GetOne(string P_SessionId_Base64)
        {
            return this.G_DB_AudioObject.FindOne(a => a.SessionId_Base64 == P_SessionId_Base64);
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


        public DB_AudioObject AudioObject_GetOneOrAdd(string P_Name)
        {
            DB_AudioObject L_MWM_DB_AudioObject = AudioObject_GetOne(P_Name);
            if (L_MWM_DB_AudioObject == null)
            {
                L_MWM_DB_AudioObject = new DB_AudioObject
                {
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
