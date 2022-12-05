using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using Web;

namespace Services
{
    public partial class Srv_UI
    {
        private void _VolumeChange(Web_InterCommMessage P_CommObject)
        {
            Arc_Device L_Arc_Device = G_Srv_AudioCore._Get_VisibleDevice();
            Arc_AudioObject L_AudioCore_Object = L_Arc_Device.AudioObjects.Where(s => s.UniqueId.ToString() == P_CommObject.Data[0].Id).FirstOrDefault();

            if (L_AudioCore_Object != null)
            {
                L_AudioCore_Object._Set_Volume(P_CommObject.Data[0].Value);
            }
        }
    }
}
