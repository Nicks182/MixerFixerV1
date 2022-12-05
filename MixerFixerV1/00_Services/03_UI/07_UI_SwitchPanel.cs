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
        private void _SwitchPanel(Web_InterCommMessage P_Web_InterCommMessage)
        {
            Web_InterCommMessage_Data L_Data = P_Web_InterCommMessage.Data.Where(d => d.Id == "deviceid").FirstOrDefault();
            if (L_Data != null)
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
    }
}
