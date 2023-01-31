using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using MixerFixerV1;
using Web;

namespace Services
{
    public partial class Srv_UI
    {
        private void _Modal_QRCode_Show(Web_InterCommMessage P_Web_InterCommMessage)
        {

            P_Web_InterCommMessage.ModalInfo = new Web_InterCommMessage_Modal
            {
                Id = G_HTML_Templates._Template_QRCodeModal_Id(),
                State = 1 // Show
            };

            P_Web_InterCommMessage.HTMLs.Clear();
            P_Web_InterCommMessage.HTMLs.Add(new Web_InterCommMessage_HTML
            {
                ContainerId = "#ModalHolder",
                HTML = G_HTML_Templates._Template_QRCodeModal_HTML().ToString(),
                IsAppend = true
            });
        }


        private void _Modal_QRCode_OpenInLocalBrowser(Web_InterCommMessage P_Web_InterCommMessage)
        {
            Process.Start(new ProcessStartInfo(App._GetLocalIPAddress()) { UseShellExecute = true });

            P_Web_InterCommMessage.ModalInfo = new Web_InterCommMessage_Modal
            {
                Id = G_HTML_Templates._Template_QRCodeModal_Id(),
                State = 0 // Hide
            };

            P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type._DoNothing;
        }


    }
}
