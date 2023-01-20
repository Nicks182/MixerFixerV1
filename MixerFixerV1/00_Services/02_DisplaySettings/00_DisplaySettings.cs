using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

using MixerFixerV1;
using DisplaySettings;
using System.Windows.Interop;
using System.Threading;

namespace Services
{
    public partial class Srv_DisplaySettings
    {
        
        Srv_DB G_Srv_DB;
        Srv_TimerManager G_TimerDeviceManager;

        public Srv_DisplaySettings()
        {
            G_Srv_DB = App.ServiceProvider.GetService(typeof(Srv_DB)) as Srv_DB;
            G_TimerDeviceManager = new Srv_TimerManager();
        }

        public void Init()
        {
            //_Save_DB_MonitorInfo_All();

            //_StartDeviceTimer();


        }

        public bool? _Monitor_OnOff(string P_Name)
        {
            DisplayInformation.Monitor L_Monitor = _Get_GetMonitor(P_Name);
            if(L_Monitor != null)
            {
                DisplaySettings.DisplaySettings L_Settings = _Get_GetMonitor_Settings(L_Monitor);

                DisplayMode L_Mode = DisplayMode.Extend;

                if(L_Settings.IsAttached == true)
                {
                    L_Mode = DisplayMode.Internal;
                }

                SetDisplayMode(L_Mode);
                Thread.Sleep(2000);
                L_Monitor = _Get_GetMonitor(P_Name);
                L_Settings = _Get_GetMonitor_Settings(L_Monitor);

                // Update DB
                DB_DisplaySettings L_DB_DisplaySettings = G_Srv_DB.DisplaySettings_GetOne(P_Name);

                L_DB_DisplaySettings.IsAttached = L_Settings.IsAttached;

                return L_DB_DisplaySettings.IsAttached;
            }

            return null;
        }

        public bool _Manage_Monitor_Change(string P_Name)
        {
            DB_DisplaySettings L_DB_DisplaySettings = G_Srv_DB.DisplaySettings_GetOne(P_Name);
            if (L_DB_DisplaySettings != null)
            {
                L_DB_DisplaySettings.IsManaged = !L_DB_DisplaySettings.IsManaged;

                _Save_DB_MonitorInfo(P_Name, L_DB_DisplaySettings);

                _StartDeviceTimer();

                return L_DB_DisplaySettings.IsManaged;
            }

            return false;
        }


        private void _StartDeviceTimer()
        {
            List<DB_DisplaySettings> L_DB_DisplaySettings = G_Srv_DB.DisplaySettings_GetAll().Find(ds => ds.IsManaged == true).ToList();

            if (L_DB_DisplaySettings.Count > 0)
            {
                G_TimerDeviceManager.PrepareTimer(() => _Enforce_Monitor_Setup(), 500, 1000);
            }
            else
            {
                G_TimerDeviceManager.StopTimer();
            }
        }

        private void _Save_DB_MonitorInfo_All()
        {

            DisplayInformation[] L_DisplayInfos = DisplaySettings.DisplayInformation.EnumerateAllDisplays(false);

            DisplayInformation.Monitor L_Monitor = null;
            for (int d = 0; d < L_DisplayInfos.Count(); d++)
            {
                
                for (int m = 0; m < L_DisplayInfos[d].Monitors.Count(); m++)
                {
                    L_Monitor = L_DisplayInfos[d].Monitors[m];

                    DB_DisplaySettings L_DB_DisplaySettings = G_Srv_DB.DisplaySettings_GetOne(L_Monitor.InterfaceName.EncodeBase64());
                    DisplaySettings.DisplaySettings L_Settings = _Get_GetMonitor_Settings(L_Monitor);
                    if (L_DB_DisplaySettings == null)
                    {
                        L_DB_DisplaySettings = new DB_DisplaySettings
                        {
                            Name = L_Monitor.InterfaceName.EncodeBase64()
                        };
                    }


                    if (L_DB_DisplaySettings.IsManaged == false)
                    {
                        L_DB_DisplaySettings.DisplayName = (L_DisplayInfos[d].DisplayIndex + 1).ToString() + ": " + L_DisplayInfos[d].AdapterDescription;
                        L_DB_DisplaySettings.Screen_Pos_X = L_Settings.DesktopPosition.X;
                        L_DB_DisplaySettings.Screen_Pos_X = L_Settings.DesktopPosition.Y;
                        L_DB_DisplaySettings.Screen_Res_X = L_Settings.Mode.Width;
                        L_DB_DisplaySettings.Screen_Res_Y = L_Settings.Mode.Height;
                        L_DB_DisplaySettings.IsAttached = L_Settings.IsAttached;
                    }

                    G_Srv_DB.DisplaySettings_Save(L_DB_DisplaySettings);
                }
            }
        }

        private void _Enforce_Monitor_Setup()
        {
            List<DB_DisplaySettings> L_DB_DisplaySettings = G_Srv_DB.DisplaySettings_GetAll().Find(ds => ds.IsManaged == true).ToList();

            if(L_DB_DisplaySettings.Count > 0)
            {
                DisplayInformation.Monitor L_Monitor = null;
                for (int ds = 0; ds < L_DB_DisplaySettings.Count; ds++)
                {
                    L_Monitor = _Get_GetMonitor(L_DB_DisplaySettings[ds].Name);
                    _Set_Monitor_FromDB(L_Monitor, L_DB_DisplaySettings[ds]);
                }
            }
        }

        private void _Set_Monitor_FromDB(DisplayInformation.Monitor P_Monitor, DB_DisplaySettings P_DB_DisplaySettings)
        {
            if(P_Monitor != null)
            {
                DisplaySettings.DisplaySettings L_Settings = _Get_GetMonitor_Settings(P_Monitor);
                if (L_Settings != null)
                {
                    var L_DesktopPosition = L_Settings.DesktopPosition;
                    L_DesktopPosition.X = P_DB_DisplaySettings.Screen_Pos_X;
                    L_DesktopPosition.Y = P_DB_DisplaySettings.Screen_Pos_X;
                    L_Settings.DesktopPosition = L_DesktopPosition;

                    L_Settings.Mode.Width = P_DB_DisplaySettings.Screen_Res_X;
                    L_Settings.Mode.Height = P_DB_DisplaySettings.Screen_Res_Y;

                    var result = DisplaySettings.DisplaySettings.ChangeDisplaySettings(L_Settings);
                }
            }
        }

        //public void _Update_DB_MonitorInfo(string P_Name)
        //{
        //    DisplayInformation.Monitor L_Monitor = _Get_GetMonitor(P_Name); ;
        //    if (L_Monitor != null)
        //    {
        //        _Save_DB_MonitorInfo(L_Monitor);
        //    }
        //}

        private void _Save_DB_MonitorInfo(DisplayInformation.Monitor P_Monitor)
        {
            DB_DisplaySettings L_DB_DisplaySettings = G_Srv_DB.DisplaySettings_GetOne(P_Monitor.InterfaceName.EncodeBase64());

            _Save_DB_MonitorInfo(P_Monitor, L_DB_DisplaySettings);
        }

        private void _Save_DB_MonitorInfo(string P_Name, DB_DisplaySettings P_DB_DisplaySettings)
        {
            DisplayInformation.Monitor L_Monitor = _Get_GetMonitor(P_Name);
            if (L_Monitor != null)
            {
                _Save_DB_MonitorInfo(L_Monitor, P_DB_DisplaySettings);
            }
        }

        private void _Save_DB_MonitorInfo(DisplayInformation.Monitor P_Monitor, DB_DisplaySettings P_DB_DisplaySettings)
        {
            DisplaySettings.DisplaySettings L_Settings = _Get_GetMonitor_Settings(P_Monitor);
            if (P_DB_DisplaySettings == null)
            {
                P_DB_DisplaySettings = new DB_DisplaySettings
                {
                    Name = P_Monitor.InterfaceName.EncodeBase64()
                };
            }


            if (P_DB_DisplaySettings.IsManaged == false)
            {
                //P_DB_DisplaySettings.DisplayName = L_Settings.DisplayIndex.ToString() + ": "  + L_Settings.;
                P_DB_DisplaySettings.Screen_Pos_X = L_Settings.DesktopPosition.X;
                P_DB_DisplaySettings.Screen_Pos_X = L_Settings.DesktopPosition.Y;
                P_DB_DisplaySettings.Screen_Res_X = L_Settings.Mode.Width;
                P_DB_DisplaySettings.Screen_Res_Y = L_Settings.Mode.Height;
                P_DB_DisplaySettings.IsAttached = L_Settings.IsAttached;
            }

            G_Srv_DB.DisplaySettings_Save(P_DB_DisplaySettings);
        }



        private DisplayInformation.Monitor _Get_GetMonitor(string P_Name)
        {
            DisplayInformation.Monitor L_Monitor = null;

            DisplayInformation[] L_DisplayInfos = DisplaySettings.DisplayInformation.EnumerateAllDisplays(false);
            //DisplayInformation.Monitor[] L_Monitors = null;

            //DB_DisplaySettings L_DB_DisplaySettings = null;
            for (int d = 0; d < L_DisplayInfos.Count(); d++)
            {
                for(int m = 0; m < L_DisplayInfos[d].Monitors.Count(); m++)
                {
                    if (L_DisplayInfos[d].Monitors[m].InterfaceName.EncodeBase64().Equals(P_Name) == true)
                    {
                        return L_DisplayInfos[d].Monitors[m];
                    }

                    //if (Srv_Utils._StringToBase64(L_DisplayInfos[d].Monitors[m].InterfaceName).Equals(P_Name) == true)
                    //{
                    //    return L_DisplayInfos[d].Monitors[m];
                    //}
                }

                //L_Monitor = L_DisplayInfos[d].Monitors.Where(m => m.InterfaceName.Equals(P_Name)).FirstOrDefault();
                //if (L_Monitor != null)
                //{
                //    break;
                //}

            }

            return null;
        }

        private DisplaySettings.DisplaySettings _Get_GetMonitor_Settings(DisplayInformation.Monitor P_Monitor)
        {
            if(P_Monitor == null)
            {
                return null;
            }
            DisplaySettings.DisplaySettings L_Settings = DisplaySettings.DisplaySettings.GetDisplaySettings(P_Monitor.MonitorIndex, DisplaySettings.DisplaySettings.SettingsType.Registry);

            return L_Settings;
        }


    }
}

