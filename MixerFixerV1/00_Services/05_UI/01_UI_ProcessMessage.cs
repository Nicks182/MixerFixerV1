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
        
        public void _ProcessComms(Web_InterCommMessage P_Web_InterCommMessage)
        {
            try
            {
                switch (P_Web_InterCommMessage.CommType)
                {
                    case Web_InterCommMessage_Type.Init:
                        _LoadUI(P_Web_InterCommMessage);
                        break;

                    case Web_InterCommMessage_Type.Volume_Change:
                        _VolumeChange(P_Web_InterCommMessage);
                        break;

                    case Web_InterCommMessage_Type.Mute_Change:
                        _MuteChange(P_Web_InterCommMessage);
                        break;

                    case Web_InterCommMessage_Type.Managed_Change:
                        _ManamgedChange(P_Web_InterCommMessage);
                        break;

                    case Web_InterCommMessage_Type.Volume_ModalShow:
                        _VolumeChangeModal_Show(P_Web_InterCommMessage);
                        break;

                    case Web_InterCommMessage_Type.Volume_ModalSet:
                        _VolumeChangeModal_Set(P_Web_InterCommMessage);
                        break;

                    case Web_InterCommMessage_Type.Settings_Show:
                    case Web_InterCommMessage_Type.Settings_Priority_MoveUp:
                    case Web_InterCommMessage_Type.Settings_Priority_MoveDown:
                    case Web_InterCommMessage_Type.Settings_Priority_Enforce:
                    case Web_InterCommMessage_Type.Settings_UseDefault_Change:
                    case Web_InterCommMessage_Type.Settings_DefaultVolume_Show:
                    case Web_InterCommMessage_Type.Settings_DefaultVolume_Change:
                    case Web_InterCommMessage_Type.Settings_StartHidden_Change:
                    case Web_InterCommMessage_Type.Settings_StartWithWindows_Change:
                        _Modal_Settings(P_Web_InterCommMessage);
                        break;

                    case Web_InterCommMessage_Type.Theme_Show:
                    case Web_InterCommMessage_Type.Theme_Reset:
                    case Web_InterCommMessage_Type.Theme_Color_Change_Red:
                    case Web_InterCommMessage_Type.Theme_Color_Change_Green:
                    case Web_InterCommMessage_Type.Theme_Color_Change_Blue:
                        _Modal_Theme(P_Web_InterCommMessage);
                        break;

                    case Web_InterCommMessage_Type.DisplaySettings_Show:
                    case Web_InterCommMessage_Type.DisplaySettings_ManagedChange:
                    case Web_InterCommMessage_Type.DisplaySettings_MonitorPower:
                    case Web_InterCommMessage_Type.DisplaySettings_Reload:
                        _Modal_DisplaySettings(P_Web_InterCommMessage);
                        break;

                    case Web_InterCommMessage_Type.SwitchPanel:
                        _SwitchPanel(P_Web_InterCommMessage);
                        break;

                    case Web_InterCommMessage_Type.QRCode_Show:
                        _Modal_QRCode_Show(P_Web_InterCommMessage);
                        break;

                    case Web_InterCommMessage_Type.Modal_Close:
                        _Modal_Close(P_Web_InterCommMessage);
                        break;
                }
            }
            catch (Exception ex)
            {
                _Modal_Message_Show(P_Web_InterCommMessage, ex.Message);

                P_Web_InterCommMessage.Data.Add(new Web_InterCommMessage_Data
                {
                    DataType = Web_InterCommMessage_DataType.Text,
                    Id = "MsgStack",
                    Value = ex.StackTrace// + "\n" + ex.InnerException.ToString()
                });
            }
        }


        
        private void _Modal_Close(Web_InterCommMessage P_Web_InterCommMessage)
        {
            P_Web_InterCommMessage.ModalInfo = new Web_InterCommMessage_Modal
            {
                Id = P_Web_InterCommMessage.Data[0].Value,
                State = 0 // Hide
            };

            P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type._DoNothing;
        }
        
    }
}
