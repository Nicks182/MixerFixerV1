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
        
        public void _Init(Srv_AudioCore P_Srv_AudioCore)
        {
            if (G_Srv_AudioCore == null)
            {
                G_Srv_AudioCore = P_Srv_AudioCore;

                G_Srv_AudioCore.DoUpdate -= G_Srv_AudioCore_DoUpdate;
                G_Srv_AudioCore.OnVolumeHasChanged -= G_Srv_AudioCore_OnVolumeChanged;
                G_Srv_AudioCore.OnDefaultDeviceSet -= G_Srv_AudioCore_OnDefaultDeviceSet;
                G_Srv_AudioCore.OnDeviceStateChange -= G_Srv_AudioCore_OnDeviceStateChange;

                G_Srv_AudioCore.DoUpdate += G_Srv_AudioCore_DoUpdate;
                G_Srv_AudioCore.OnVolumeHasChanged += G_Srv_AudioCore_OnVolumeChanged;
                G_Srv_AudioCore.OnDefaultDeviceSet += G_Srv_AudioCore_OnDefaultDeviceSet;
                G_Srv_AudioCore.OnDeviceStateChange += G_Srv_AudioCore_OnDeviceStateChange;

                G_HTML_Templates = new HTML_Templates(G_Srv_AudioCore, G_Srv_DB);
            }
        }

        public void _LoadUI(Web_InterCommMessage P_Web_InterCommMessage)
        {
            G_TimerManager.StopTimer();
            //G_TimerDeviceManager.StopTimer();
            //G_TimerVolumeManager.StopTimer();
            
            //G_Srv_AudioCore.Init();

            G_Srv_Logger._LogMessage("Default Device: " + G_Srv_AudioCore.Device.Device.DisplayName);

            //G_HTML_Templates = new HTML_Templates(G_Srv_AudioCore, G_Srv_DB);
            P_Web_InterCommMessage.HTMLs = new List<Web_InterCommMessage_HTML>
            {
                new Web_InterCommMessage_HTML
                {
                    ContainerId = "#Body",
                    HTML = G_HTML_Templates._Template_App_HTML().ToString(),
                    IsAppend = false
                }
            };

            P_Web_InterCommMessage.HTMLs.Add(_Modal_Theme_ColorChange_StyleUpdate());

            _StartDataPush();

            //G_TimerDeviceManager.PrepareTimer(() => G_Srv_AudioCore.SetDefault_Devices(), 500, 500);
            //G_TimerVolumeManager.PrepareTimer(() => G_Srv_AudioCore.CheckForVolumeChanges(), 500, 50);
        }

        public void _Reload(Web_InterCommMessage P_Web_InterCommMessage)
        {
            G_Srv_AudioCore.Reload();

            P_Web_InterCommMessage.HTMLs = new List<Web_InterCommMessage_HTML>
            {
                new Web_InterCommMessage_HTML
                {
                    ContainerId = "#PanelHolder",
                    HTML = G_HTML_Templates._Template_GetVisiblePanel_HTML().ToString()
                }
            };

            //_StartDataPush();
        }

        private void _StartDataPush()
        {
            G_TimerManager.StopTimer();
            G_TimerManager = new Srv_TimerManager();
            G_TimerManager.PrepareTimer(() => G_CommandHub.Clients.All.SendAsync("ReceiveMessage", _GetData()), 250, 30);
        }

        public void _StopDataPush()
        {
            if (G_TimerManager != null)
            {
                G_TimerManager.StopTimer();
            }
        }

    }
}
