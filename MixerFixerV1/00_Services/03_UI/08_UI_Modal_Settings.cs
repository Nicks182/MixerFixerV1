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
        private void _Modal_Settings(Web_InterCommMessage P_Web_InterCommMessage)
        {
            switch (P_Web_InterCommMessage.CommType)
            {
                case Web_InterCommMessage_Type.ShowSettings:
                    _Modal_Settings_Show(P_Web_InterCommMessage);
                    break;

                case Web_InterCommMessage_Type.Settings_Priority_MoveUp:
                case Web_InterCommMessage_Type.Settings_Priority_MoveDown:
                    _Modal_Settings_DevicePriority_Move(P_Web_InterCommMessage);
                    break;
            }
        }

        private void _Modal_Settings_Show(Web_InterCommMessage P_Web_InterCommMessage)
        {
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


        private void _Modal_Settings_DevicePriority_Move(Web_InterCommMessage P_Web_InterCommMessage)
        {
            Web_InterCommMessage_Data L_Data = P_Web_InterCommMessage.Data.Where(d => d.Id == "DeviceId").FirstOrDefault();
            if (L_Data != null)
            {
                Guid L_DeviceId = Guid.Parse(L_Data.Value);
                DB_DevicePriority L_Device = G_Srv_DB.DevicePriority_GetOne_ById(L_DeviceId);
                if (L_Device != null)
                {
                    List<DB_DevicePriority> L_Devices = G_Srv_DB.DevicePriority_GetAll().Find(d => d.IsMic == L_Device.IsMic).OrderBy(d => d.Priority).ToList();

                    L_Device = L_Devices.Where(d => d.Id == L_DeviceId).FirstOrDefault();

                    int L_CurrentIndex = L_Devices.IndexOf(L_Device);
                    int L_NewIndex = L_CurrentIndex;

                    if (P_Web_InterCommMessage.CommType == Web_InterCommMessage_Type.Settings_Priority_MoveDown)
                    {
                        if (L_Devices.Count > (L_CurrentIndex + 1))
                        {
                            L_NewIndex = L_CurrentIndex + 1;
                        }
                    }
                    else
                    {
                        if ((L_CurrentIndex - 1) > -1)
                        {
                            L_NewIndex = L_CurrentIndex - 1;
                        }
                    }


                    L_Devices.Remove(L_Device);
                    L_Devices.Insert(L_NewIndex, L_Device);

                    for (int i = 0; i < L_Devices.Count; i++)
                    {
                        L_Devices[i].Priority = i;
                        G_Srv_DB.DevicePriority_Save(L_Devices[i]);
                    }

                    P_Web_InterCommMessage.HTMLs.Clear();

                    if (L_Device.IsMic == true)
                    {
                        P_Web_InterCommMessage.HTMLs.Add(new Web_InterCommMessage_HTML
                        {
                            ContainerId = "#Div_Settings_MicPriority",
                            HTML = G_HTML_Templates._Template_SettingsModal_Body_MicPriority_HTML().ToString()
                        });
                    }
                    else
                    {
                        P_Web_InterCommMessage.HTMLs.Add(new Web_InterCommMessage_HTML
                        {
                            ContainerId = "#Div_Settings_SoundPriority",
                            HTML = G_HTML_Templates._Template_SettingsModal_Body_SoundPriority_HTML().ToString()
                        });
                    }
                }
            }
        }

        //private void _Modal_Settings_DevicePriority_MoveDown(Web_InterCommMessage P_Web_InterCommMessage)
        //{
        //    Web_InterCommMessage_Data L_Data = P_Web_InterCommMessage.Data.Where(d => d.Id == "DeviceId").FirstOrDefault();
        //    if(L_Data != null)
        //    {
        //        Guid L_DeviceId = Guid.Parse(L_Data.Value);
        //        DB_DevicePriority L_Device = G_Srv_DB.DevicePriority_GetOne_ById(L_DeviceId);

        //        List<DB_DevicePriority> L_Devices = G_Srv_DB.DevicePriority_GetAll().Find(d => d.IsMic == L_Device.IsMic).ToList();

        //        L_Device = L_Devices.Where(d => d.Id == L_DeviceId).FirstOrDefault();
        //        if(L_Device != null)
        //        {
        //            int L_CurrentIndex = L_Devices.IndexOf(L_Device);
        //            int L_NewIndex = L_CurrentIndex + 1;

        //            if(L_Devices.Count > L_NewIndex)
        //            {
        //                L_Devices.Remove(L_Device);
        //                L_Devices.Insert(L_NewIndex, L_Device);
        //            }
        //        }
        //    }
        //}

        //private void _Modal_Settings_DevicePriority_MoveUp(Web_InterCommMessage P_Web_InterCommMessage)
        //{
        //    Web_InterCommMessage_Data L_Data = P_Web_InterCommMessage.Data.Where(d => d.Id == "DeviceId").FirstOrDefault();
        //    if (L_Data != null)
        //    {
        //        Guid L_DeviceId = Guid.Parse(L_Data.Value);
        //        DB_DevicePriority L_Device = G_Srv_DB.DevicePriority_GetOne_ById(L_DeviceId);

        //        List<DB_DevicePriority> L_Devices = G_Srv_DB.DevicePriority_GetAll().Find(d => d.IsMic == L_Device.IsMic).ToList();

        //        //DB_DevicePriority L_Device = L_Devices.Where(d => d.Id == L_DeviceId).FirstOrDefault();
        //        if (L_Device != null)
        //        {
        //            int L_CurrentIndex = L_Devices.IndexOf(L_Device);
        //            int L_NewIndex = L_CurrentIndex - 1;

        //            if (L_NewIndex > 0)
        //            {
        //                L_Devices.Remove(L_Device);
        //                L_Devices.Insert(L_NewIndex, L_Device);
        //            }
        //        }
        //    }
        //}

    }
}
