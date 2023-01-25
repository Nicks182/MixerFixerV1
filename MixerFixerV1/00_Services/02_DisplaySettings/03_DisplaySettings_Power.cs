using MixerFixerV1;
using Services.PathHelper;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public partial class Srv_DisplaySettings
    {

        public bool? _Manage_Monitor_Power(string P_Name_Base64)
        {
            DB_DisplaySettings L_DB_DisplaySettings = _Get_Monitor_DB(P_Name_Base64);
            if (L_DB_DisplaySettings != null)
            {
                WindowsDisplayAPI.DisplayConfig.PathInfo L_PathInfo = _Get_API_PathInfo(P_Name_Base64);
                if (L_PathInfo != null && L_PathInfo.IsInUse == true)
                {
                    L_DB_DisplaySettings.PathInfo_JSON = _Get_MonitorInfo_JSON(L_PathInfo);
                    G_Srv_DB.DisplaySettings_Save(L_DB_DisplaySettings);

                    // Turn display OFF
                    // Will return FALSE if turned off without errors
                    bool? L_IsOn = _Monitor_Turn_Off(L_PathInfo);
                    if (L_IsOn.HasValue == true)
                    {
                        L_DB_DisplaySettings.IsPowered = L_IsOn.Value;
                    }
                }
                else
                {
                    if(string.IsNullOrEmpty(L_DB_DisplaySettings.PathInfo_JSON) == true)
                    {
                        // show message
                        OnLogMessage?.Invoke(
                                new List<string>
                                {
                                    "No settings for selected Display.",
                                    "",
                                    "Use 'Windows Display Settings' to setup the Display first.",
                                    "Then open Display Settings in MixerFixer again and the settings will be reloaded."
                                });
                        return null;
                    }
                    else
                    {
                        MF_PathHelper_Path L_MF_PathHelper_Path = _Get_MonitorInfo(L_DB_DisplaySettings);
                        if (L_MF_PathHelper_Path == null)
                        {
                            // show message
                            OnLogMessage?.Invoke(
                                new List<string>
                                {
                                    "Something is wrong with the saved settings for the selected Display.", 
                                    "",
                                    "Click the 'Clear Settings' icon next to he selected Disaply.",
                                    "Then open Display Settings in MixerFixer again and the settings will be reloaded."
                                });
                            return null;
                        }
                        L_PathInfo = L_MF_PathHelper_Path.ToPathInfo();

                        // Turn display ON
                        // Will return TRUE if turned on without errors
                        bool? L_IsOn = _Monitor_Turn_On(L_PathInfo);
                        if (L_IsOn.HasValue == true)
                        {
                            L_DB_DisplaySettings.IsPowered = L_IsOn.Value;
                        }
                    }
                }
                return L_DB_DisplaySettings.IsPowered;
            }

            return null;
        }



    }
}

