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
        private void _Modal_DisplaySettings(Web_InterCommMessage P_Web_InterCommMessage)
        {
            switch (P_Web_InterCommMessage.CommType)
            {
                case Web_InterCommMessage_Type.DisplaySettings_Show:
                    _Modal_DisplaySettings_Show(P_Web_InterCommMessage);
                    break;

                case Web_InterCommMessage_Type.DisplaySettings_ManagedChange:
                    _Modal_DisplaySettings_ManagedChange(P_Web_InterCommMessage);
                    break;

                case Web_InterCommMessage_Type.DisplaySettings_MonitorPower:
                    _Modal_DisplaySettings_MonitorPower(P_Web_InterCommMessage);
                    break;

            }


            
        }

        private void _Modal_DisplaySettings_Show(Web_InterCommMessage P_Web_InterCommMessage)
        {
            P_Web_InterCommMessage.ModalInfo = new Web_InterCommMessage_Modal
            {
                Id = G_HTML_Templates._Template_DisplaySettingsModal_Id(),
                State = 1 // Show
            };

            P_Web_InterCommMessage.HTMLs.Clear();
            P_Web_InterCommMessage.HTMLs.Add(new Web_InterCommMessage_HTML
            {
                ContainerId = "#ModalHolder",
                HTML = G_HTML_Templates._Template_DisplaySettingsModal_HTML().ToString(),
                IsAppend = true
            });
        }

        private void _Modal_DisplaySettings_ManagedChange(Web_InterCommMessage P_Web_InterCommMessage)
        {
            bool L_IsManaged = G_Srv_DisplaySettings._Manage_Monitor_Change(P_Web_InterCommMessage.Data[0].Value);
            string P_Id = P_Web_InterCommMessage.Data[0].Id;

            P_Web_InterCommMessage.Data.Clear();
            P_Web_InterCommMessage.Data.Add(new Web_InterCommMessage_Data
                {
                    Id = G_HTML_Templates._Template_DisplaySettingsModal_Body_Panel_DisplayItem_IsManaged_Id(P_Id),
                    Value = Convert.ToInt32(L_IsManaged).ToString(),
                    DataType = Web_InterCommMessage_DataType.Toggle
                }
            );

            P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type.DataUpdate;
        }

        private void _Modal_DisplaySettings_MonitorPower(Web_InterCommMessage P_Web_InterCommMessage)
        {
            bool? L_IsAttached = G_Srv_DisplaySettings._Monitor_OnOff(P_Web_InterCommMessage.Data[0].Value);
            if (L_IsAttached != null)
            {
                string P_Id = P_Web_InterCommMessage.Data[0].Id;

                P_Web_InterCommMessage.Data.Clear();
                P_Web_InterCommMessage.Data.Add(new Web_InterCommMessage_Data
                {
                    Id = G_HTML_Templates._Template_DisplaySettingsModal_Body_Panel_DisplayItem_IsAttached_Id(P_Id),
                    Value = Convert.ToInt32(L_IsAttached).ToString(),
                    DataType = Web_InterCommMessage_DataType.Toggle
                }
                );

                P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type.DataUpdate;
            }
            else
            {
                P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type._DoNothing;
            }
        }

    }
}
