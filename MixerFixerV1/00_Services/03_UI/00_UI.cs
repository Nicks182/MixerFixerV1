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
        IHubContext<CommandHub> G_CommandHub;
        Srv_TimerManager G_TimerManager;
        HTML_Templates G_HTML_Templates;
        Srv_AudioCore G_Srv_AudioCore;

        //public Srv_UI(IHubContext<CommandHub> P_CommandHub, Srv_TimerManager P_TimerManager, Srv_AudioCore P_Srv_AudioCore, HTML_Templates P_HTML_Templates)
        public Srv_UI(IHubContext<CommandHub> P_CommandHub)
        {
            G_CommandHub = P_CommandHub;
            G_TimerManager = new Srv_TimerManager();
            G_Srv_AudioCore = new Srv_AudioCore();
            G_HTML_Templates = new HTML_Templates(G_Srv_AudioCore);

            G_Srv_AudioCore.DoUpdate += G_Srv_AudioCore_DoUpdate;
        }

        private void G_Srv_AudioCore_DoUpdate()
        {
            Web_InterCommMessage L_CommMessage = new Web_InterCommMessage { CommType = Web_InterCommMessage_Type.Init };
            _Init(L_CommMessage);

            G_CommandHub.Clients.All.SendAsync("ReceiveMessage", L_CommMessage);
        }

        public void _ProcessComms(Web_InterCommMessage P_Web_InterCommMessage)
        {
            try
            {
                switch (P_Web_InterCommMessage.CommType)
                {
                    case Web_InterCommMessage_Type.Init:
                        _Init(P_Web_InterCommMessage);
                        break;

                    case Web_InterCommMessage_Type.Volume_Change:
                        _VolumeChange(P_Web_InterCommMessage);
                        break;

                    case Web_InterCommMessage_Type.DataUpdate:
                        //_GetUpdate(P_CommObject);
                        break;

                    case Web_InterCommMessage_Type.SwitchPanel:
                        _SwitchPanel(P_Web_InterCommMessage);
                        break;
                }
            }
            catch (Exception ex)
            {
                P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type.ShowMessage;
                P_Web_InterCommMessage.Data.Add(new Web_InterCommMessage_Data
                {
                    DataType = Web_InterCommMessage_DataType.Text,
                    Id = ex.Message,
                    Value = ex.StackTrace// + "\n" + ex.InnerException.ToString()
                });
            }
        }

        public void _Init(Web_InterCommMessage P_Web_InterCommMessage)
        {
            G_Srv_AudioCore.Init();

            P_Web_InterCommMessage.HTMLs = new List<Web_InterCommMessage_HTML>
            {
                new Web_InterCommMessage_HTML
                {
                    ContainerId = "#Body",
                    HTML = G_HTML_Templates._Template_App_HTML().ToString()
                }
            };

            G_TimerManager.PrepareTimer(() => G_CommandHub.Clients.All.SendAsync("ReceiveMessage", _GetData()));
        }

        private void _SwitchPanel(Web_InterCommMessage P_Web_InterCommMessage)
        {
            Web_InterCommMessage_Data L_Data = P_Web_InterCommMessage.Data.Where(d => d.Id == "deviceid").FirstOrDefault();
            if(L_Data != null)
            {
                Arc_Device L_Arc_Device = G_Srv_AudioCore._Set_VisibleDevice(L_Data.Value);

                P_Web_InterCommMessage.HTMLs = new List<Web_InterCommMessage_HTML>
                {
                    new Web_InterCommMessage_HTML
                    {
                        ContainerId = "#PanelHolder",
                        HTML = G_HTML_Templates._Template_GetVisiblePanel_HTML().ToString()
                    }
                };
            }
            
        }

        public Web_InterCommMessage _GetData()
        {
            Web_InterCommMessage L_CommMessage = new Web_InterCommMessage();
            L_CommMessage.CommType = Web_InterCommMessage_Type.DataUpdate;

            _GetUpdate(L_CommMessage);
            return L_CommMessage;
        }

        private void _GetUpdate(Web_InterCommMessage P_CommObject)
        {
            P_CommObject.Data.Clear();
            Arc_Device L_Arc_Device = G_Srv_AudioCore._Get_VisibleDevice();
            if (L_Arc_Device != null)
            {
                foreach (var L_AudioObj in L_Arc_Device.AudioObjects)
                {
                    P_CommObject.Data.Add(new Web_InterCommMessage_Data
                    {
                        Id = G_HTML_Templates._Template_VolumeControl_VolumeMeter_Data_Id(L_AudioObj),
                        Value = L_AudioObj._Get_PeakVolume().ToString() + "%",
                        DataType = Web_InterCommMessage_DataType.Progress
                    });

                    P_CommObject.Data.Add(new Web_InterCommMessage_Data
                    {
                        Id = G_HTML_Templates._Template_VolumeControl_VolumeSlider_Id(L_AudioObj),
                        Value = L_AudioObj._Get_Volume().ToString(),
                        DataType = Web_InterCommMessage_DataType.Slider
                    });

                    P_CommObject.Data.Add(new Web_InterCommMessage_Data
                    {
                        Id = G_HTML_Templates._Template_VolumeControl_VolumeText_Id_Data(L_AudioObj),
                        Value = L_AudioObj._Get_Volume().ToString(),
                        DataType = Web_InterCommMessage_DataType.ButtonText
                    });
                }
            }

        }

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
