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
        // Update Ignore from client input
        private void _IgnoreChange(Web_InterCommMessage P_CommObject)
        {
            Arc_AudioObject L_AudioCore_Object = G_Srv_AudioCore.Device._Get_Object(P_CommObject.Data[0].Id);

            if (L_AudioCore_Object == null)
            {
                L_AudioCore_Object = G_Srv_AudioCore.Device_Mic._Get_Object(P_CommObject.Data[0].Id);
            }

            if (L_AudioCore_Object != null)
            {
                //_Change_Ignore will set to True if False or set to False if True
                L_AudioCore_Object._Change_Ignore();

                P_CommObject.Data.Clear();
                P_CommObject.Data.Add(_IgnoreChange_Data(L_AudioCore_Object));
                
                P_CommObject.CommType = Web_InterCommMessage_Type.DataUpdate;
            }
            else
            {
                P_CommObject.CommType = Web_InterCommMessage_Type._DoNothing;
            }
        }

        private Web_InterCommMessage_Data _IgnoreChange_Data(Arc_AudioObject P_Arc_AudioObject)
        {
            return new Web_InterCommMessage_Data
            {
                Id = G_HTML_Templates._Template_VolumeControl_IsIgnore_Id(P_Arc_AudioObject),
                Value = Convert.ToInt32(P_Arc_AudioObject.IsIgnore).ToString(),
                DataType = Web_InterCommMessage_DataType.Toggle
            };

        }

    }
}
