using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.CoreAudioApi;

namespace Services
{
    public partial class Srv_AudioCore
    {
        private void _LoadDevice()
        {
            // Only load device if none is set.
            //if (G_Device == null)
            //{
            MMDevice L_DefaultDevice = G_MMDeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            if (L_DefaultDevice != null)
            {
                _SetDevice(L_DefaultDevice);
            }
            MMDevice L_DefaultDevice_Mic = G_MMDeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);
            if (L_DefaultDevice_Mic != null)
            {
                _SetDevice_Mic(L_DefaultDevice_Mic);
            }
            //}
        }

        private void _SetDevice_Mic(MMDevice P_MMDevice)
        {
            G_Device_Mic = new Arc_Device();
            G_Device_Mic.Device = new Arc_AudioObject(P_MMDevice, true);
            //G_Device_Mic.Device.OnNewSession += L_Arc_AudioObject_OnNewSession;
            G_Device_Mic.Device.OnVolumeHasChanged += L_Arc_AudioObject_OnVolumeChange;
            //G_Device_Mic.Device.DeviceChanged += Device_DeviceChanged;
            G_Device_Mic.Device._Init();
        }

        private void _SetDevice(MMDevice P_MMDevice)
        {
            
            //if (G_Device != null && G_Device.Device != null)
            //{
            //    G_Device.Device.OnNewSession -= L_Arc_AudioObject_OnNewSession;
            //    G_Device.Device.OnVolumeHasChanged -= L_Arc_AudioObject_OnVolumeChange;
            //    G_Device.Device.DeviceChanged -= Device_DeviceChanged;
            //    G_Device.Device.Dispose();

            //    for (int i = 0; i < G_Device.Sessions.Count; i++)
            //    {
            //        G_Device.Sessions[i].OnNewSession -= L_Arc_AudioObject_OnNewSession;
            //        G_Device.Sessions[i].OnVolumeHasChanged -= L_Arc_AudioObject_OnVolumeChange;
            //        G_Device.Sessions[i].Dispose();
            //    }
            //}


            G_Device = new Arc_Device();
            G_Device.Device = new Arc_AudioObject(P_MMDevice);
            G_Device.Device.OnNewSession += L_Arc_AudioObject_OnNewSession;
            G_Device.Device.OnVolumeHasChanged += L_Arc_AudioObject_OnVolumeChange;
            G_Device.Device.DeviceChanged += Device_DeviceChanged;
            G_Device.Device._Init();

            _LoadSessions();
        }

        private void Device_DeviceChanged(object? sender, EventArgs e)
        {
            //SetDefault_Devices();
            //DoUpdate?.Invoke();
        }

        private void _LoadSessions()
        {
            G_Device.Sessions.Clear();
            //Arc_AudioObject L_Arc_AudioObject = G_Device.Device;
            SessionCollection L_SessionCollection = G_Device.Device._GetSessions();
            for (int i = 0; i < L_SessionCollection.Count; i++)
            {
                Arc_AudioObject L_Arc_AudioObject = new Arc_AudioObject(L_SessionCollection[i]);
                L_Arc_AudioObject.OnNewSession += L_Arc_AudioObject_OnNewSession;
                L_Arc_AudioObject.OnVolumeHasChanged += L_Arc_AudioObject_OnVolumeChange;

                L_Arc_AudioObject._Init();
                G_Device.Sessions.Add(L_Arc_AudioObject);
            }

        }



        private void L_Arc_AudioObject_OnVolumeChange(Arc_AudioObject P_Arc_AudioObject)
        {
            OnVolumeHasChanged?.Invoke(P_Arc_AudioObject);
        }

        private void L_Arc_AudioObject_OnNewSession(Arc_AudioObject_SessionInfo P_AudioCore_Object_SessionInfo)
        {
            Reload();
            DoUpdate?.Invoke();
        }
    }

    public class Arc_Device
    {
        public Arc_Device()
        {
            Sessions = new List<Arc_AudioObject>();
        }
        private Guid G_Id = Guid.NewGuid();
        public Guid Id { get { return G_Id; } }
        //public bool IsVisible {get; set;}
        public Arc_AudioObject Device { get; set; }
        public List<Arc_AudioObject> Sessions { get; set; }

        public Arc_AudioObject _Get_Object(string P_UniqueId)
        {
            return _Get_Object(Guid.Parse(P_UniqueId));
        }

        public Arc_AudioObject _Get_Object(Guid P_UniqueId)
        {
            if (Device.UniqueId == P_UniqueId)
            {
                return Device;
            }

            return Sessions.Where(s => s.UniqueId == P_UniqueId).FirstOrDefault();
        }
    }
}
