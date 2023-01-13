
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MixerFixerV1;
using NAudio.CoreAudioApi;

namespace Services
{
    public partial class Srv_AudioCore
    {
        MMDeviceEnumerator G_MMDeviceEnumerator = new MMDeviceEnumerator();
        Srv_DB G_Srv_DB;
        Srv_TimerManager G_TimerDeviceManager;

        //NotificationClientImplementation G_NotificationClientImplementation;
        
        Arc_Device G_Device { get; set; }
        public Arc_Device Device { get { return G_Device; } }

        public bool G_ShowSettings = false;

        public delegate void DoUpdateDelegate();
        public event DoUpdateDelegate DoUpdate;

        public delegate void OnVolumeHasChangedDelegate(Arc_AudioObject P_Arc_AudioObject);
        public event OnVolumeHasChangedDelegate OnVolumeHasChanged;

        public delegate void OnDeviceStateChangeDelegate(string P_DeviceId, DeviceState P_NewState);
        public event OnDeviceStateChangeDelegate OnDeviceStateChange;

        public delegate void OnDefaultDeviceSetDelegate(string P_DeviceId);
        public event OnDefaultDeviceSetDelegate OnDefaultDeviceSet;

        public Srv_AudioCore()
        {
            G_Srv_DB = App.ServiceProvider.GetService(typeof(Srv_DB)) as Srv_DB;
            G_TimerDeviceManager = new Srv_TimerManager();
            
            //G_NotificationClientImplementation = new NotificationClientImplementation();
            //G_NotificationClientImplementation.OnDefaultDeviceChange += G_NotificationClientImplementation_OnDefaultDeviceChange;
            //G_NotificationClientImplementation.OnDeviceStateChange += G_NotificationClientImplementation_OnDeviceStateChange;
            //G_MMDeviceEnumerator.RegisterEndpointNotificationCallback(G_NotificationClientImplementation);

        }

        public void Init()
        {
            G_TimerDeviceManager.StopTimer();

            LoadPriorityList();
            _LoadDevice();

            _StartDeviceTimer();
        }

        private void _StartDeviceTimer()
        {
            G_TimerDeviceManager.PrepareTimer(() => SetDefault_Devices(), 500, 500);
        }

        public void Reload()
        {
            _LoadSessions();
        }

        public void LoadPriorityList()
        {
            foreach (var L_Device in G_MMDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.All).OrderBy(d => d.DeviceFriendlyName))
            {
                G_Srv_DB.DevicePriority_GetOneOrAdd(L_Device.FriendlyName, LoadPriorityList_Get_DisplayText(L_Device), L_Device.DeviceFriendlyName, false);
            }

            foreach (var L_Device in G_MMDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.All).OrderBy(d => d.DeviceFriendlyName))
            {
                G_Srv_DB.DevicePriority_GetOneOrAdd(L_Device.FriendlyName, LoadPriorityList_Get_DisplayText(L_Device), L_Device.DeviceFriendlyName, true);
            }
        }


        public string LoadPriorityList_Get_DisplayText(MMDevice P_MMDevice)
        {
            return P_MMDevice.FriendlyName.Replace("(" + P_MMDevice.DeviceFriendlyName + ")", "");
        }

        //public Arc_Device _Get_VisibleDevice()
        //{
        //    Arc_Device L_Arc_Device = G_Devices.Where(d => d.IsVisible == true).FirstOrDefault();

        //    return L_Arc_Device;
        //}

        //public Arc_Device _Set_VisibleDevice(string P_DeviceId)
        //{
        //    Guid L_DeviceId = Guid.Parse(P_DeviceId);

        //    Arc_Device L_Arc_Device = G_Devices.Where(d => d.Id == L_DeviceId).FirstOrDefault();
        //    if (L_Arc_Device != null)
        //    {
        //        for (int i = 0; i < G_Devices.Count; i++)
        //        {
        //            G_Devices[i].IsVisible = false;
        //        }

        //        L_Arc_Device.IsVisible = true;
        //    }

        //    return L_Arc_Device;
        //}

    }

    
}
