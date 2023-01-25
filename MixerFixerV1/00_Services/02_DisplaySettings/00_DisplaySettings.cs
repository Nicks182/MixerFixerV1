using MixerFixerV1;
using Services.PathHelper;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public partial class Srv_DisplaySettings
    {
        Srv_DB G_Srv_DB;
        Srv_TimerManager G_TimerDeviceManager;

        public delegate void OnLogMessageDelegate(List<string> P_MessageList);
        public event OnLogMessageDelegate OnLogMessage;

        public Srv_DisplaySettings()
        {
            G_Srv_DB = App.ServiceProvider.GetService(typeof(Srv_DB)) as Srv_DB;
            G_TimerDeviceManager = new Srv_TimerManager();
        }

        public void Init()
        {
            //_Save_DB_MonitorInfo_All();

            _StartDeviceTimer();


        }

        private bool? _Monitor_Turn_On(WindowsDisplayAPI.DisplayConfig.PathInfo P_PathInfo)
        {
            if(P_PathInfo == null)
            {
                return null; // Monitor state did not change
            }
            // Get all current active Display paths and add the path of the Display we want to turn ON.
            // Only Display paths in the list will be turned ON. We need to tel Windows about ALL the monitors we want to be ON.
            List<WindowsDisplayAPI.DisplayConfig.PathInfo> L_Paths = _GetAllActivePaths().Where(d => d.DisplaySource.DisplayName != P_PathInfo.DisplaySource.DisplayName).ToList();
            L_Paths.Add(P_PathInfo);

            WindowsDisplayAPI.DisplayConfig.PathInfo.ApplyPathInfos(L_Paths);

            return true; // Monitor should be ON
        }

        private bool? _Monitor_Turn_Off(WindowsDisplayAPI.DisplayConfig.PathInfo P_PathInfo)
        {
            if (P_PathInfo == null)
            {
                return null; // Monitor state did not change
            }
            // Get all Display paths that should be ON except the one we want to be OFF.
            // Only Display paths in the list will remain on. Those not in the list will be turned OFF.
            List<WindowsDisplayAPI.DisplayConfig.PathInfo> L_Paths = _GetAllActivePaths().Where(p => p.DisplaySource.DisplayName != P_PathInfo.DisplaySource.DisplayName).ToList();

            WindowsDisplayAPI.DisplayConfig.PathInfo.ApplyPathInfos(L_Paths);

            return false; // Monitor should be off
        }

        private DB_DisplaySettings _Get_Monitor_DB(string P_Name_Base64)
        {
            DB_DisplaySettings L_DB_DisplaySettings = G_Srv_DB.DisplaySettings_GetOne(P_Name_Base64);

            if (L_DB_DisplaySettings == null)
            {
                L_DB_DisplaySettings = new DB_DisplaySettings
                {
                    DevicePath_Base64 = P_Name_Base64,
                };
            }

            return L_DB_DisplaySettings;
        }

        private WindowsDisplayAPI.DisplayConfig.PathInfo _Get_API_PathInfo(string P_Name_Base64)
        {
            return _GetAllActivePaths().Where(p => p.DisplaySource.DisplayName.EncodeBase64().Equals(P_Name_Base64)).FirstOrDefault();
        }

        private string _GetFirendlyName(WindowsDisplayAPI.DisplayConfig.PathInfo P_Path)
        {
            if (P_Path.TargetsInfo.Count() > 0 && string.IsNullOrEmpty(P_Path.TargetsInfo[0].DisplayTarget.FriendlyName) == false)
            {
                return P_Path.TargetsInfo[0].DisplayTarget.FriendlyName;
            }

            return P_Path.DisplaySource.DisplayName;
        }

        private string _Get_MonitorInfo_JSON(string P_Name_Base64)
        {
            WindowsDisplayAPI.DisplayConfig.PathInfo L_Path = _GetAllActivePaths().Where(p => p.DisplaySource.DisplayName.EncodeBase64().Equals(P_Name_Base64)).FirstOrDefault();

            return _Get_MonitorInfo_JSON(L_Path);
        }

        private string _Get_MonitorInfo_JSON(WindowsDisplayAPI.DisplayConfig.PathInfo P_Path)
        {
            if (P_Path != null)
            {
                MF_PathHelper_Path L_MF_PathHelper_Path = new MF_PathHelper_Path(P_Path);
                return PathUtils._Json_To(L_MF_PathHelper_Path);

            }

            return "";
        }

        private MF_PathHelper_Path _Get_MonitorInfo(string P_Name_Base64)
        {
            DB_DisplaySettings L_DB_DisplaySettings = G_Srv_DB.DisplaySettings_GetOne(P_Name_Base64);
            return _Get_MonitorInfo(L_DB_DisplaySettings);
            
        }

        private MF_PathHelper_Path _Get_MonitorInfo(DB_DisplaySettings P_DB_DisplaySettings)
        {
            if (P_DB_DisplaySettings != null)
            {
                return PathUtils._Json_From(P_DB_DisplaySettings.PathInfo_JSON);
            }
            return null;

        }



        
        


        

        


    }
}

