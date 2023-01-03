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
        

        public Web_InterCommMessage _GetData()
        {
            G_DataPushMessage.Data.Clear();
            _GetUpdate(G_DataPushMessage);
            return G_DataPushMessage;
        }

        private void _GetUpdate(Web_InterCommMessage P_CommObject)
        {
            P_CommObject.Data.Clear();
            Arc_Device L_Arc_Device = G_Srv_AudioCore.Device;
            if (L_Arc_Device != null)
            {
                _GetUpdate_Data(P_CommObject, L_Arc_Device.Device);

                foreach (var L_AudioObj in L_Arc_Device.Sessions.ToList())
                {
                    _GetUpdate_Data(P_CommObject, L_AudioObj);
                }
            }

        }

        private void _GetUpdate_Data(Web_InterCommMessage P_CommObject, Arc_AudioObject P_Arc_AudioObject)
        {
            if (P_Arc_AudioObject != null)
            {
                try
                {
                    Arc_AudioObject_PeakVolum L_PeakVolum = P_Arc_AudioObject._Get_PeakVolume(); ;
                    P_CommObject.Data.Add(new Web_InterCommMessage_Data
                    {
                        Id = G_HTML_Templates._Template_VolumeControl_VolumeMeter_Left_Id(P_Arc_AudioObject),
                        Value = L_PeakVolum.Left.ToString() + "%",
                        DataType = Web_InterCommMessage_DataType.Progress
                    });

                    P_CommObject.Data.Add(new Web_InterCommMessage_Data
                    {
                        Id = G_HTML_Templates._Template_VolumeControl_VolumeMeter_Right_Id(P_Arc_AudioObject),
                        Value = L_PeakVolum.Right.ToString() + "%",
                        DataType = Web_InterCommMessage_DataType.Progress
                    });
                }
                catch (Exception ex)
                {

                }
            }

        }

    }
}
