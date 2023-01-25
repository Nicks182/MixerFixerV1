using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Diagnostics;

using MixerFixerV1;
using Services.PathHelper;

namespace Services
{
    public partial class Srv_DisplaySettings
    {
        private void _StartDeviceTimer()
        {
            G_TimerDeviceManager.PrepareTimer(() => _Restore_Monitor_Setup(), 2000, 2000);

            //List<DB_DisplaySettings> L_DB_DisplaySettings = G_Srv_DB.DisplaySettings_GetAll().Find(ds => ds.IsManaged == true).ToList();

            //if (L_DB_DisplaySettings.Count > 0)
            //{
            //    G_TimerDeviceManager.PrepareTimer(() => _Restore_Monitor_Setup(), 500, 1000);
            //}
            //else
            //{
            //    G_TimerDeviceManager.StopTimer();
            //}
        }



        private void _Restore_Monitor_Setup()
        {
            List<DB_DisplaySettings> L_DB_DisplaySettings = G_Srv_DB.DisplaySettings_GetAll().Find(ds => ds.IsManaged == true).ToList();

            if (L_DB_DisplaySettings.Count > 0)
            {
                for(int i = 0; i < L_DB_DisplaySettings.Count; i++)
                {
                    WindowsDisplayAPI.DisplayConfig.PathInfo L_PathInfo = _Get_API_PathInfo(L_DB_DisplaySettings[i].DevicePath_Base64);
                    if (L_PathInfo == null && L_DB_DisplaySettings[i].IsPowered == true) // Means monitor is on and was turned on outside of MixerFixer
                    {
                        L_DB_DisplaySettings[i].IsPowered = false;
                        G_Srv_DB.DisplaySettings_Save(L_DB_DisplaySettings[i]);
                    }

                    if (L_PathInfo != null && L_PathInfo.IsInUse == true && L_DB_DisplaySettings[i].IsPowered == false && L_DB_DisplaySettings[i].IsManaged == true) // Means monitor is on and was turned on outside of MixerFixer
                    {
                        MF_PathHelper_Path L_MF_PathHelper_Path = _Get_MonitorInfo(L_DB_DisplaySettings[i]);
                        if (L_MF_PathHelper_Path != null)
                        {
                            bool? L_IsOn = _Monitor_Turn_On(L_MF_PathHelper_Path.ToPathInfo());
                            
                            if(L_IsOn.HasValue == true)
                            {
                                L_DB_DisplaySettings[i].IsPowered = L_IsOn.Value;
                                G_Srv_DB.DisplaySettings_Save(L_DB_DisplaySettings[i]);
                            }
                        }
                    }
                }
                
            }
            //else
            //{
            //    // if db has no managed object, stop timer
            //    G_TimerDeviceManager.StopTimer();
            //}
        }


    }
}

