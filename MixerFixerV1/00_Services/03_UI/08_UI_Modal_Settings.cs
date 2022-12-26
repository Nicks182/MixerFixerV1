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
        private void _Modal_Settings(Web_InterCommMessage P_Web_InterCommMessage)
        {
            _Modal_Settings_AddDevicestoDB();

            P_Web_InterCommMessage.ModalInfo = new Web_InterCommMessage_Modal
            {
                Id = G_HTML_Templates._Template_SettingsModal_Id(),
                State = 1 // Show
            };

            P_Web_InterCommMessage.HTMLs.Clear();
            P_Web_InterCommMessage.HTMLs.Add(new Web_InterCommMessage_HTML
            {
                ContainerId = "#Body",
                HTML = G_HTML_Templates._Template_SettingsModal_HTML().ToString(),
                IsAppend = true
            });
        }


        private void _Modal_Settings_AddDevicestoDB()
        {
            
        }

    }
}
