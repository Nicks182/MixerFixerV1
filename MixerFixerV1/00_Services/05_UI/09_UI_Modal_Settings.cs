﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Win32;
using MixerFixerV1;
using Web;

namespace Services
{
    public partial class Srv_UI
    {

        private void _Modal_Settings(Web_InterCommMessage P_Web_InterCommMessage)
        {
            switch (P_Web_InterCommMessage.CommType)
            {
                case Web_InterCommMessage_Type.Settings_Show:
                    _Modal_Settings_Show(P_Web_InterCommMessage);
                    break;

                case Web_InterCommMessage_Type.Settings_Priority_MoveUp:
                case Web_InterCommMessage_Type.Settings_Priority_MoveDown:
                    _Modal_Settings_DevicePriority_Move(P_Web_InterCommMessage);
                    break;

                case Web_InterCommMessage_Type.Settings_Priority_Enforce:
                    _Modal_Settings_DevicePriority_Enforce(P_Web_InterCommMessage);
                    break;


                case Web_InterCommMessage_Type.Settings_UseDefault_Change:
                    _Modal_Settings_UseDefaultVolume_Change(P_Web_InterCommMessage);
                    break;

                case Web_InterCommMessage_Type.Settings_DefaultVolume_Show:
                    _Modal_Settings_VolumeInput_Show(P_Web_InterCommMessage);
                    break;

                case Web_InterCommMessage_Type.Settings_DefaultVolume_Change:
                    _Modal_Settings_VolumeInput_Change(P_Web_InterCommMessage);
                    break;

                case Web_InterCommMessage_Type.Settings_StartHidden_Change:
                    _Modal_Settings_StartHidden_Change(P_Web_InterCommMessage);
                    break;

                case Web_InterCommMessage_Type.Settings_StartWithWindows_Change:
                    _Modal_Settings_StartWithWindows_Change(P_Web_InterCommMessage);
                    break;
            }
        }

        private void _Modal_Settings_Show(Web_InterCommMessage P_Web_InterCommMessage)
        {
            foreach(var L_Device in G_Srv_DB.DevicePriority_GetAll().Find(d => d.IsSelected == true))
            {
                L_Device.IsSelected = false;
                G_Srv_DB.DevicePriority_Save(L_Device);
            }

            P_Web_InterCommMessage.ModalInfo = new Web_InterCommMessage_Modal
            {
                Id = G_HTML_Templates._Template_SettingsModal_Id(),
                State = 1 // Show
            };

            P_Web_InterCommMessage.HTMLs.Clear();
            P_Web_InterCommMessage.HTMLs.Add(new Web_InterCommMessage_HTML
            {
                ContainerId = "#ModalHolder",
                HTML = G_HTML_Templates._Template_SettingsModal_HTML().ToString(),
                IsAppend = true
            });
        }

        private void _Modal_Settings_VolumeInput_Show(Web_InterCommMessage P_Web_InterCommMessage)
        {
            P_Web_InterCommMessage.ModalInfo = new Web_InterCommMessage_Modal
            {
                Id = G_HTML_Templates._Template_Modal_VolumeInput_Id(),
                State = 1, // Show
                Focus = G_HTML_Templates._Template_Modal_VolumeInput_Body_Input_Id()
            };

            P_Web_InterCommMessage.HTMLs.Clear();
            P_Web_InterCommMessage.HTMLs.Add(new Web_InterCommMessage_HTML
            {
                ContainerId = "#ModalHolder",
                HTML = G_HTML_Templates._Template_Modal_VolumeInput_HTML(Web_InterCommMessage_Type.Settings_DefaultVolume_Change, G_Srv_DB.G_DefaultVolume).ToString(),
                IsAppend = true
            });
        }

        private void _Modal_Settings_VolumeInput_Change(Web_InterCommMessage P_Web_InterCommMessage)
        {
            if (P_Web_InterCommMessage.Data != null && P_Web_InterCommMessage.Data.Count > 0)
            {
                double L_NewValue = Convert.ToDouble(P_Web_InterCommMessage.Data[0].Value);
                if(L_NewValue > 100)
                {
                    L_NewValue = 100;
                }

                if(L_NewValue < 0)
                {
                    L_NewValue = 0;
                }
                DB_Settings L_DefaultVolume = G_Srv_DB.Settings_GetOne(G_Srv_DB.G_DefaultVolume);

                L_DefaultVolume.Value = L_NewValue.ToString();

                G_Srv_DB.Settings_Save(L_DefaultVolume);

                P_Web_InterCommMessage.Data = new List<Web_InterCommMessage_Data>
                {
                    new Web_InterCommMessage_Data
                    {
                        Id = G_HTML_Templates._Template_SettingsModal_Body_DefaultVolume_Id() + "_Text",
                        Value = L_DefaultVolume.Value + "%",
                        DataType = Web_InterCommMessage_DataType.ButtonText
                    }
                };

                P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type.DataUpdate;
            }

            P_Web_InterCommMessage.ModalInfo = new Web_InterCommMessage_Modal
            {
                Id = G_HTML_Templates._Template_Modal_VolumeInput_Id(),
                State = 0, // Hide
            };

            
        }

        private void _Modal_Settings_UseDefaultVolume_Change(Web_InterCommMessage P_Web_InterCommMessage)
        {
            DB_Settings L_DefaultVolumeEnable = G_Srv_DB.Settings_GetOne(G_Srv_DB.G_DefaultVolumeEnable);
            if (L_DefaultVolumeEnable.Value == "1")
            {
                L_DefaultVolumeEnable.Value = "0";
            }
            else
            {
                L_DefaultVolumeEnable.Value = "1";
            }

            G_Srv_DB.Settings_Save(L_DefaultVolumeEnable);

            P_Web_InterCommMessage.Data = new List<Web_InterCommMessage_Data>
            {
                new Web_InterCommMessage_Data
                {
                    Id = G_HTML_Templates._Template_SettingsModal_Body_UseDefaultVolume_Id(),
                    Value = L_DefaultVolumeEnable.Value,
                    DataType = Web_InterCommMessage_DataType.Toggle
                }
            };

            P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type.DataUpdate;
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

                    for(int i = 0; i < L_Devices.Count; i++)
                    {
                        L_Devices[i].IsSelected = false;
                        G_Srv_DB.DevicePriority_Save(L_Devices[i]);
                    }

                    L_Device = L_Devices.Where(d => d.Id == L_DeviceId).FirstOrDefault();
                    L_Device.IsSelected = true;

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

        private void _Modal_Settings_DevicePriority_Enforce(Web_InterCommMessage P_Web_InterCommMessage)
        {
            Web_InterCommMessage_Data L_Data = P_Web_InterCommMessage.Data.Where(d => d.Id == "DeviceId").FirstOrDefault();
            if (L_Data != null)
            {
                Guid L_DeviceId = Guid.Parse(L_Data.Value);
                DB_DevicePriority L_Device = G_Srv_DB.DevicePriority_GetOne_ById(L_DeviceId);
                if (L_Device != null)
                {
                    L_Device.EnforceDefault = !L_Device.EnforceDefault;
                    G_Srv_DB.DevicePriority_Save(L_Device);

                    P_Web_InterCommMessage.Data = new List<Web_InterCommMessage_Data>
                    {
                        new Web_InterCommMessage_Data
                        {
                            Id = G_HTML_Templates._Template_SettingsModal_Body_DevicePriority_Item_EnforceDefault_Id(L_Device),
                            Value = Convert.ToInt32(L_Device.EnforceDefault).ToString(),
                            DataType = Web_InterCommMessage_DataType.Toggle
                        }
                    };

                    P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type.DataUpdate;
                }
            }
        }

        private void _Modal_Settings_StartHidden_Change(Web_InterCommMessage P_Web_InterCommMessage)
        {
            DB_Settings L_StartHidden = G_Srv_DB.Settings_GetOne(G_Srv_DB.G_StartHidden);
            if (L_StartHidden.Value == "1")
            {
                L_StartHidden.Value = "0";
            }
            else
            {
                L_StartHidden.Value = "1";
            }

            G_Srv_DB.Settings_Save(L_StartHidden);

            P_Web_InterCommMessage.Data = new List<Web_InterCommMessage_Data>
            {
                new Web_InterCommMessage_Data
                {
                    Id = G_HTML_Templates._Template_SettingsModal_Body_StartHidden_Id(),
                    Value = L_StartHidden.Value,
                    DataType = Web_InterCommMessage_DataType.Toggle
                }
            };

            P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type.DataUpdate;
        }
        
        private void _Modal_Settings_StartWithWindows_Change(Web_InterCommMessage P_Web_InterCommMessage)
        {
            DB_Settings L_StartHidden = G_Srv_DB.Settings_GetOne(G_Srv_DB.G_StartWithWindows);
            if (L_StartHidden.Value == "1")
            {
                L_StartHidden.Value = "0";
                _Modal_Settings_StartWithWindows_Setup(false);
            }
            else
            {
                L_StartHidden.Value = "1";
                _Modal_Settings_StartWithWindows_Setup(true);
            }

            G_Srv_DB.Settings_Save(L_StartHidden);

            P_Web_InterCommMessage.Data = new List<Web_InterCommMessage_Data>
            {
                new Web_InterCommMessage_Data
                {
                    Id = G_HTML_Templates._Template_SettingsModal_Body_StartWithWindows_Id(),
                    Value = L_StartHidden.Value,
                    DataType = Web_InterCommMessage_DataType.Toggle
                }
            };

            P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type.DataUpdate;
        }

        private void _Modal_Settings_StartWithWindows_Setup(bool P_IsStartWithWindows)
        {
            if(P_IsStartWithWindows == true)
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                string str = Assembly.GetExecutingAssembly().Location;
                key.SetValue("MixerFixer", Path.Combine(App.G_BaseDir, "MixerFixerV1.exe"));
            }
            else
            {
                var path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);
                key.DeleteValue("MixerFixer", false);
            }
        }

    }
}
