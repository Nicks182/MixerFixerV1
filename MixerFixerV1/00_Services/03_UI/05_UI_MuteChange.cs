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
        private void _MuteChange(Web_InterCommMessage P_CommObject)
        {
            Arc_Device L_Arc_Device = G_Srv_AudioCore._Get_VisibleDevice();
            Arc_AudioObject L_AudioCore_Object = L_Arc_Device.AudioObjects.Where(s => s.UniqueId.ToString() == P_CommObject.Data[0].Id).FirstOrDefault();

            if (L_AudioCore_Object != null)
            {
                bool L_IsMute = L_AudioCore_Object._Set_Mute();

                P_CommObject.Data.Clear();
                P_CommObject.Data.Add(new Web_InterCommMessage_Data
                {
                    Id = G_HTML_Templates._Template_VolumeControl_IsMute_Id(L_AudioCore_Object),
                    Value = Convert.ToInt32(L_IsMute).ToString(),
                    DataType = Web_InterCommMessage_DataType.Toggle
                });

                P_CommObject.CommType = Web_InterCommMessage_Type.DataUpdate;
            }
            else
            {
                P_CommObject.CommType = Web_InterCommMessage_Type._DoNothing;
            }
        }
    }
}
