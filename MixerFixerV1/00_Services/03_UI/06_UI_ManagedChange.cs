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
        // Set Managed fron client input
        private void _ManamgedChange(Web_InterCommMessage P_CommObject)
        {
            //Arc_Device L_Arc_Device = G_Srv_AudioCore.Device;
            //Arc_AudioObject L_AudioCore_Object = L_Arc_Device.AudioObjects.Where(s => s.UniqueId.ToString() == P_CommObject.Data[0].Id).FirstOrDefault();
            Arc_AudioObject L_AudioCore_Object = G_Srv_AudioCore.Device._Get_Object(P_CommObject.Data[0].Id);

            if (L_AudioCore_Object != null)
            {
                //_Set_Managed will set to True if False or set to False if True
                L_AudioCore_Object._Set_Managed();

                P_CommObject.Data.Clear();
                P_CommObject.Data.Add(_ManamgedChange_Data(L_AudioCore_Object));



                P_CommObject.CommType = Web_InterCommMessage_Type.DataUpdate;
            }
            else
            {
                P_CommObject.CommType = Web_InterCommMessage_Type._DoNothing;
            }
        }

        private Web_InterCommMessage_Data _ManamgedChange_Data(Arc_AudioObject P_Arc_AudioObject)
        {
            return new Web_InterCommMessage_Data
            {
                Id = G_HTML_Templates._Template_VolumeControl_IsMananged_Id(P_Arc_AudioObject),
                Value = Convert.ToInt32(P_Arc_AudioObject._Get_Managed()).ToString(),
                DataType = Web_InterCommMessage_DataType.Toggle
            };

        }

    }
}
