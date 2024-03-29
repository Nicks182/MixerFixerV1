﻿using System;
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
        // Set volume from client input
        private void _VolumeChange(Web_InterCommMessage P_CommObject)
        {
            //Arc_Device L_Arc_Device = G_Srv_AudioCore.Device;
            //Arc_AudioObject L_AudioCore_Object = L_Arc_Device.Sessions.Where(s => s.UniqueId.ToString() == P_CommObject.Data[0].Id).FirstOrDefault();
            Arc_AudioObject L_AudioCore_Object = G_Srv_AudioCore.Device._Get_Object(P_CommObject.Data[0].Id);

            if (L_AudioCore_Object == null)
            {
                L_AudioCore_Object = G_Srv_AudioCore.Device_Mic._Get_Object(P_CommObject.Data[0].Id);
            }

            if (L_AudioCore_Object != null)
            {
                L_AudioCore_Object._Set_Volume(P_CommObject.Data[0].Value);
            }
        }


        private void G_Srv_AudioCore_OnVolumeChanged(Arc_AudioObject P_Arc_AudioObject)
        {
            Web_InterCommMessage L_CommMessage = new Web_InterCommMessage { CommType = Web_InterCommMessage_Type.DataUpdate };

            L_CommMessage.Data.AddRange(_VolumeChange_Data(P_Arc_AudioObject));
            L_CommMessage.Data.Add(_MuteChange_Data(P_Arc_AudioObject));

            G_CommandHub.Clients.All.SendAsync("ReceiveMessage", L_CommMessage);
        }

        private List<Web_InterCommMessage_Data> _VolumeChange_Data(Arc_AudioObject P_Arc_AudioObject)
        {
            return new List<Web_InterCommMessage_Data>
            {
                new Web_InterCommMessage_Data
                {
                    Id = G_HTML_Templates._Template_VolumeControl_VolumeSlider_Id(P_Arc_AudioObject),
                    Value = P_Arc_AudioObject._Get_Volume().ToString(),
                    DataType = Web_InterCommMessage_DataType.Slider
                },
                new Web_InterCommMessage_Data
                {
                    Id = G_HTML_Templates._Template_VolumeControl_VolumeText_Id_Data(P_Arc_AudioObject),
                Value = P_Arc_AudioObject._Get_Volume().ToString() + "%",
                DataType = Web_InterCommMessage_DataType.ButtonText
                }
            };

        }


        private void _VolumeChangeModal_Show(Web_InterCommMessage P_Web_InterCommMessage)
        {
            //Guid L_Id = Guid.Parse(P_Web_InterCommMessage.Data.Where(d => d.Id == "Id").FirstOrDefault().Value);

            //Arc_AudioObject L_Arc_AudioObject = G_Srv_AudioCore.Device._Get_Object(P_Web_InterCommMessage.Data.Where(d => d.Id == "Id").FirstOrDefault().Value);

            P_Web_InterCommMessage.ModalInfo = new Web_InterCommMessage_Modal
            {
                Id = G_HTML_Templates._Template_Modal_VolumeInput_Id(),
                State = 1, // Show
                Focus = G_HTML_Templates._Template_Modal_VolumeInput_Body_Input_Id()
            };

            P_Web_InterCommMessage.HTMLs.Clear();
            P_Web_InterCommMessage.HTMLs.Add(new Web_InterCommMessage_HTML
            {
                ContainerId = "#ModalHolder",
                HTML = G_HTML_Templates._Template_Modal_VolumeInput_HTML(Web_InterCommMessage_Type.Volume_ModalSet, P_Web_InterCommMessage.Data.Where(d => d.Id == "Id").FirstOrDefault().Value).ToString(),
                IsAppend = true
            }) ;
        }

        private void _VolumeChangeModal_Set(Web_InterCommMessage P_Web_InterCommMessage)
        {
            //Guid L_Id = Guid.Parse(P_Web_InterCommMessage.Data.Where(d => d.Id == "Id").FirstOrDefault().Value);

            Arc_AudioObject L_Arc_AudioObject = G_Srv_AudioCore.Device._Get_Object(P_Web_InterCommMessage.Data[0].Id);

            if (L_Arc_AudioObject == null)
            {
                L_Arc_AudioObject = G_Srv_AudioCore.Device_Mic._Get_Object(P_Web_InterCommMessage.Data[0].Id);
            }

            if (L_Arc_AudioObject != null)
            {
                L_Arc_AudioObject._Set_Volume(P_Web_InterCommMessage.Data[0].Value);

                P_Web_InterCommMessage.ModalInfo = new Web_InterCommMessage_Modal
                {
                    Id = G_HTML_Templates._Template_Modal_VolumeInput_Id(),
                    State = 0, // Show
                };

                _GetUpdate(P_Web_InterCommMessage);
                P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type.DataUpdate;
            }
        }

    }
}
