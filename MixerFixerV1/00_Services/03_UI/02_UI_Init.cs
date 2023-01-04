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
        
        public void _Init(Web_InterCommMessage P_Web_InterCommMessage)
        {
            G_TimerManager.StopTimer();
            G_TimerManager = new Srv_TimerManager();
            G_Srv_AudioCore.Init();

            G_Srv_Logger._LogMessage("Default Device: " + G_Srv_AudioCore.Device.Device.Name);

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

            _StartDataPush();

            G_TimerDeviceManager.PrepareTimer(() => G_Srv_AudioCore.SetDefault_Devices(), 500, 500);
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


    }
}
