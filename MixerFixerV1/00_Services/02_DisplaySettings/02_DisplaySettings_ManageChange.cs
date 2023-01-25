using MixerFixerV1;
using Services.PathHelper;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public partial class Srv_DisplaySettings
    {

        public bool _Manage_Monitor_Change(string P_Name_Base64)
        {
            DB_DisplaySettings L_DB_DisplaySettings = G_Srv_DB.DisplaySettings_GetOne(P_Name_Base64);
            //MF_PathHelper_Path L_MF_PathHelper_Path = _Get_MonitorInfo(L_DB_DisplaySettings);
            if (L_DB_DisplaySettings != null)
            {
                WindowsDisplayAPI.DisplayConfig.PathInfo L_Path = _GetAllActivePaths().Where(p => p.DisplaySource.DisplayName.EncodeBase64().Equals(P_Name_Base64)).FirstOrDefault();
                if (L_Path == null || L_Path.IsInUse == false)
                {
                    OnLogMessage?.Invoke(new List<string>
                    {
                        "Monitor must be on to Activate"
                    });
                }
                else
                {
                    L_DB_DisplaySettings.IsManaged = !L_DB_DisplaySettings.IsManaged;
                    L_DB_DisplaySettings.PathInfo_JSON = _Get_MonitorInfo_JSON(P_Name_Base64);
                    G_Srv_DB.DisplaySettings_Save(L_DB_DisplaySettings);
                }
                //_StartDeviceTimer();

                return L_DB_DisplaySettings.IsManaged;
            }

            return false;
        }



    }
}

