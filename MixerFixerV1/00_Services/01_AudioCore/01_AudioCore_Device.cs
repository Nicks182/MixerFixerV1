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
        private void _LoadDevices()
        {
            G_Devices.Clear();
            
            MMDevice L_DefaultDevice = G_MMDeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            
            G_Devices.Add(_LoadDevice(L_DefaultDevice, true));
            //Arc_Device L_Arc_Device = null;
            //Arc_AudioObject L_Arc_AudioObject = null;
            foreach (var L_Device in G_MMDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
            {
                if(L_Device.ID != L_DefaultDevice.ID)
                {
                    G_Devices.Add(_LoadDevice(L_Device, false));
                }
                //L_Arc_AudioObject = new Arc_AudioObject(G_Srv_DB, L_Device);
                //L_Arc_AudioObject.OnNewSession += L_Arc_AudioObject_OnNewSession;
                //L_Arc_AudioObject.OnVolumeChange += L_Arc_AudioObject_OnVolumeChange;

                //L_Arc_Device = new Arc_Device();
                //L_Arc_Device.Name = L_Arc_AudioObject.Name;
                ////L_Arc_Device.IsVisible = L_Device.State == DeviceState.Active && G_Devices.Where(a => a.IsVisible == true).Count() == 0;
                //L_Arc_Device.IsVisible = L_Device.DeviceFriendlyName == L_DefaultDevice.DeviceFriendlyName && G_Devices.Where(a => a.IsVisible == true).Count() == 0; ;
                //L_Arc_Device.AudioObjects.Add(L_Arc_AudioObject);

                //if(L_Device.State == DeviceState.Active)
                //{
                //    SessionCollection L_SessionCollection = L_Arc_AudioObject._GetSessions();
                //    for(int i = 0; i < L_SessionCollection.Count; i++)
                //    {
                //        L_Arc_AudioObject = new Arc_AudioObject(G_Srv_DB, L_SessionCollection[i]);
                //        L_Arc_AudioObject.OnNewSession += L_Arc_AudioObject_OnNewSession;
                //        L_Arc_AudioObject.OnVolumeChange += L_Arc_AudioObject_OnVolumeChange;
                //        L_Arc_Device.AudioObjects.Add(L_Arc_AudioObject);
                //    }
                //}

                //G_Devices.Add(L_Arc_Device);
            }
        }

        private Arc_Device _LoadDevice(MMDevice P_Device, bool P_IsDefault)
        {

            Arc_AudioObject L_Arc_AudioObject = new Arc_AudioObject(G_Srv_DB, P_Device);
            L_Arc_AudioObject.OnNewSession += L_Arc_AudioObject_OnNewSession;
            L_Arc_AudioObject.OnVolumeChange += L_Arc_AudioObject_OnVolumeChange;

            Arc_Device L_Arc_Device = new Arc_Device();
            L_Arc_Device.Name = L_Arc_AudioObject.Name;
            L_Arc_Device.IsVisible = P_IsDefault && G_Devices.Where(a => a.IsVisible == true).Count() == 0;
            L_Arc_Device.AudioObjects.Add(L_Arc_AudioObject);

            SessionCollection L_SessionCollection = L_Arc_AudioObject._GetSessions();
            for (int i = 0; i < L_SessionCollection.Count; i++)
            {
                L_Arc_AudioObject = new Arc_AudioObject(G_Srv_DB, L_SessionCollection[i]);
                L_Arc_AudioObject.OnNewSession += L_Arc_AudioObject_OnNewSession;
                L_Arc_AudioObject.OnVolumeChange += L_Arc_AudioObject_OnVolumeChange;
                L_Arc_Device.AudioObjects.Add(L_Arc_AudioObject);
            }

            return L_Arc_Device;
        }


        private void L_Arc_AudioObject_OnVolumeChange(Arc_AudioObject P_Arc_AudioObject)
        {
            OnVolumeChanged?.Invoke(P_Arc_AudioObject);
        }

        private void L_Arc_AudioObject_OnNewSession(Arc_AudioObject_SessionInfo P_AudioCore_Object_SessionInfo)
        {
            DoUpdate?.Invoke();
        }
    }
    public class Arc_Device
    {
        private Guid G_Id = Guid.NewGuid();
        public Guid Id {get {return G_Id;}}
        public bool IsVisible {get; set;}
        public string Name { get; set; }
        public List<Arc_AudioObject> AudioObjects = new List<Arc_AudioObject>();

    }
}
