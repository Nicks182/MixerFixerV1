﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using Web;

namespace Services
{
    public partial class Srv_UI
    {
        IHubContext<CommandHub> G_CommandHub;
        Srv_Logger G_Srv_Logger;
        Srv_TimerManager G_TimerManager;
        Srv_TimerManager G_TimerDeviceManager;
        HTML_Templates G_HTML_Templates;
        Srv_AudioCore G_Srv_AudioCore;
        Srv_DB G_Srv_DB = new Srv_DB();

        Web_InterCommMessage G_DataPushMessage = new Web_InterCommMessage
        { 
            CommType = Web_InterCommMessage_Type.DataUpdate
        };

        public Srv_UI(IHubContext<CommandHub> P_CommandHub, Srv_Logger P_Srv_Logger)
        {
            G_CommandHub = P_CommandHub;
            G_Srv_Logger = P_Srv_Logger;
            G_TimerManager = new Srv_TimerManager();
            G_TimerDeviceManager = new Srv_TimerManager();
            G_Srv_AudioCore = new Srv_AudioCore(G_Srv_DB);
            G_HTML_Templates = new HTML_Templates(G_Srv_AudioCore, G_Srv_DB);

            G_Srv_AudioCore.DoUpdate += G_Srv_AudioCore_DoUpdate;
            G_Srv_AudioCore.OnVolumeChanged += G_Srv_AudioCore_OnVolumeChanged;
            G_Srv_AudioCore.OnDefaultDeviceSet += G_Srv_AudioCore_OnDefaultDeviceSet;
        }

        private void G_Srv_AudioCore_OnDefaultDeviceSet(string P_DeviceId)
        {
            G_Srv_Logger._LogMessage(G_Srv_AudioCore.Device.Device._Get_ID() + " == " + P_DeviceId);
            Web_InterCommMessage L_CommMessage = new Web_InterCommMessage { CommType = Web_InterCommMessage_Type.Init };
            _Init(L_CommMessage);

            G_CommandHub.Clients.All.SendAsync("ReceiveMessage", L_CommMessage);
        }


        private void G_Srv_AudioCore_DoUpdate()
        {
            //Thread.Sleep(1000);
            //Web_InterCommMessage L_CommMessage = new Web_InterCommMessage { CommType = Web_InterCommMessage_Type.Init };
            //_Init(L_CommMessage);

            Web_InterCommMessage L_CommMessage = new Web_InterCommMessage { CommType = Web_InterCommMessage_Type.SwitchPanel };
            _Reload(L_CommMessage);

            G_CommandHub.Clients.All.SendAsync("ReceiveMessage", L_CommMessage);
        }


        
    }
}
