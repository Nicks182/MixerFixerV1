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
            Arc_Device L_Arc_Device = G_Srv_AudioCore._Get_VisibleDevice();
            if (L_Arc_Device != null)
            {
                Arc_AudioObject_PeakVolum L_PeakVolum = null;
                foreach (var L_AudioObj in L_Arc_Device.AudioObjects)
                {
                    L_PeakVolum = L_AudioObj._Get_PeakVolume();
                    //P_CommObject.Data.Add(new Web_InterCommMessage_Data
                    //{
                    //    Id = G_HTML_Templates._Template_VolumeControl_VolumeMeter_Data_Id(L_AudioObj),
                    //    Value = L_PeakVolum.Master.ToString() + "%",
                    //    DataType = Web_InterCommMessage_DataType.Progress
                    //});

                    P_CommObject.Data.Add(new Web_InterCommMessage_Data
                    {
                        Id = G_HTML_Templates._Template_VolumeControl_VolumeMeter_Left_Id(L_AudioObj),
                        Value = L_PeakVolum.Left.ToString() + "%",
                        DataType = Web_InterCommMessage_DataType.Progress
                    });

                    P_CommObject.Data.Add(new Web_InterCommMessage_Data
                    {
                        Id = G_HTML_Templates._Template_VolumeControl_VolumeMeter_Right_Id(L_AudioObj),
                        Value = L_PeakVolum.Right.ToString() + "%",
                        DataType = Web_InterCommMessage_DataType.Progress
                    });

                    //P_CommObject.Data.Add(new Web_InterCommMessage_Data
                    //{
                    //    Id = G_HTML_Templates._Template_VolumeControl_VolumeSlider_Id(L_AudioObj),
                    //    Value = L_AudioObj._Get_Volume().ToString(),
                    //    DataType = Web_InterCommMessage_DataType.Slider
                    //});

                    //P_CommObject.Data.Add(new Web_InterCommMessage_Data
                    //{
                    //    Id = G_HTML_Templates._Template_VolumeControl_VolumeText_Id_Data(L_AudioObj),
                    //    Value = L_AudioObj._Get_Volume().ToString(),
                    //    DataType = Web_InterCommMessage_DataType.ButtonText
                    //});
                }
            }

        }

    }
}
