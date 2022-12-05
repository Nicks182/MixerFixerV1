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

        Web_InterCommMessage G_DataPushMessage = new Web_InterCommMessage
        { 
            CommType = Web_InterCommMessage_Type.DataUpdate
        };

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


        
    }
}
