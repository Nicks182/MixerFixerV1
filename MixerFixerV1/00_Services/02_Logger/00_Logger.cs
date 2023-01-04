using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace Services
{
    public class Srv_Logger
    {
        IHubContext<CommandHub> G_CommandHub;
        public Srv_Logger(IHubContext<CommandHub> P_CommandHub)
        {
            G_CommandHub = P_CommandHub;
        }


        public void _LogError(Exception P_Ex)
        {
            Web_InterCommMessage L_CommMessage = new Web_InterCommMessage { CommType = Web_InterCommMessage_Type._Log };

            L_CommMessage.Data.Add(new Web_InterCommMessage_Data
            {
                Id = "Message",
                Value = P_Ex.Message
            });

            L_CommMessage.Data.Add(new Web_InterCommMessage_Data
            {
                Id = "StackTrace",
                Value = P_Ex.StackTrace
            });

            _Log(L_CommMessage);
        }

        public void _LogMessage(string P_Message)
        {
            Web_InterCommMessage L_CommMessage = new Web_InterCommMessage { CommType = Web_InterCommMessage_Type._Log };

            L_CommMessage.Data.Add(new Web_InterCommMessage_Data
            {
                Id = "Message",
                Value = P_Message
            });

            _Log(L_CommMessage);
        }

        private void _Log(Web_InterCommMessage P_CommMessage)
        {
            

            G_CommandHub.Clients.All.SendAsync("ReceiveMessage", P_CommMessage);
        }
    }
}
