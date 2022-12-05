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
                        _Init(P_Web_InterCommMessage);
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
