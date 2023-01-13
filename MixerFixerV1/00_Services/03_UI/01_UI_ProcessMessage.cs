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

                    case Web_InterCommMessage_Type.SwitchPanel: // Device
                        _SwitchPanel(P_Web_InterCommMessage);
                        break;
                }
            }
            catch (Exception ex)
            {
                P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type.ShowMessage;
                P_Web_InterCommMessage.Data.Add(new Web_InterCommMessage_Data
                {
                    DataType = Web_InterCommMessage_DataType.Text,
                    Id = ex.Message,
                    Value = ex.StackTrace// + "\n" + ex.InnerException.ToString()
                });
            }
        }


        

        
    }
}
