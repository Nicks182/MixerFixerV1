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
        private void _Modal_Message_Show(Web_InterCommMessage P_Web_InterCommMessage, string P_Message)
        {
            P_Web_InterCommMessage.ModalInfo = new Web_InterCommMessage_Modal
            {
                Id = G_HTML_Templates._Template_MessageModal_Id(),
                State = 1 // Show
            };

            P_Web_InterCommMessage.HTMLs.Clear();
            P_Web_InterCommMessage.HTMLs.Add(new Web_InterCommMessage_HTML
            {
                ContainerId = "#ModalHolder",
                HTML = G_HTML_Templates._Template_MessageModal_HTML(P_Message).ToString(),
                IsAppend = true
            });

            P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type.ShowMessage;
        }

    }
}
