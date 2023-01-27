using MixerFixerV1;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public partial class Srv_DisplaySettings
    {

        public void _Save_DB_MonitorInfo_All()
        {
            List<DB_DisplaySettings> L_DB_DisplaySettings = G_Srv_DB.DisplaySettings_GetAll().FindAll().ToList();

            for(int i = 0; i < L_DB_DisplaySettings.Count; i++)
            {
                G_Srv_DB.DisplaySettings_Delete(L_DB_DisplaySettings[i]);
            }

            WindowsDisplayAPI.DisplayConfig.PathInfo[] L_ActivePaths = _GetAllActivePaths();

            foreach(var L_Path in L_ActivePaths)
            {
                DB_DisplaySettings L_DB_DisplaySetting = G_Srv_DB.DisplaySettings_GetOne(L_Path.DisplaySource.DisplayName.EncodeBase64());

                if (L_DB_DisplaySetting == null)
                {
                    L_DB_DisplaySetting = new DB_DisplaySettings
                    {
                        DevicePath_Base64 = L_Path.DisplaySource.DisplayName.EncodeBase64(),
                    };
                }

                if (L_DB_DisplaySetting.IsManaged == false)
                {
                    L_DB_DisplaySetting.DisplayName = L_Path.DisplaySource.DisplayName;
                    L_DB_DisplaySetting.FriendlyName = _GetFirendlyName(L_Path);
                    L_DB_DisplaySetting.IsPowered = true;
                    L_DB_DisplaySetting.PathInfo_JSON = _Get_MonitorInfo_JSON(L_Path);
                }
                G_Srv_DB.DisplaySettings_Save(L_DB_DisplaySetting);
            }

        }

        private WindowsDisplayAPI.DisplayConfig.PathInfo[] _GetAllActivePaths()
        {
            return WindowsDisplayAPI.DisplayConfig.PathInfo.GetActivePaths();
        }

    }
}

