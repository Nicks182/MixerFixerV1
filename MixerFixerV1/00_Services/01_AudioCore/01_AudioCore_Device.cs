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
                _SetDevice(L_DefaultDevice);
            //}
        }

        private void _SetDevice(MMDevice P_MMDevice)
        {
            if (G_Device != null && G_Device.Device != null)
            {
                G_Device.Device.OnNewSession -= L_Arc_AudioObject_OnNewSession;
                G_Device.Device.OnVolumeHasChanged -= L_Arc_AudioObject_OnVolumeChange;
                G_Device.Device.DeviceChanged -= Device_DeviceChanged;
                G_Device.Device.Dispose();

                for (int i = 0; i < G_Device.Sessions.Count; i++)
                {
                    G_Device.Sessions[i].OnNewSession -= L_Arc_AudioObject_OnNewSession;
                    G_Device.Sessions[i].OnVolumeHasChanged -= L_Arc_AudioObject_OnVolumeChange;
                    G_Device.Sessions[i].Dispose();
                }
            }

            //if (G_Device != null)
            //{
            //    if(G_Device.Device != null)
            //    {
            //        G_Device.Device.OnNewSession -= L_Arc_AudioObject_OnNewSession;
            //        G_Device.Device.OnVolumeChange -= L_Arc_AudioObject_OnVolumeChange;
            //        G_Device.Device.DeviceChanged -= Device_DeviceChanged;

            //        for(int i = 0; i < G_Device.Sessions.Count; i++)
            //        {
            //            G_Device.Sessions[i].OnNewSession -= L_Arc_AudioObject_OnNewSession;
            //            G_Device.Sessions[i].OnVolumeChange -= L_Arc_AudioObject_OnVolumeChange;

            //        }
            //    }
            //}

            G_Device = new Arc_Device();
            G_Device.Device = new Arc_AudioObject(G_Srv_DB, P_MMDevice);
            G_Device.Device.OnNewSession += L_Arc_AudioObject_OnNewSession;
            G_Device.Device.OnVolumeHasChanged += L_Arc_AudioObject_OnVolumeChange;
            G_Device.Device.DeviceChanged += Device_DeviceChanged;
            G_Device.Device._Init();

            //G_Device = new Arc_AudioObject(G_Srv_DB, P_MMDevice);
            //G_Device.OnNewSession += L_Arc_AudioObject_OnNewSession;
            //G_Device.OnVolumeChange += L_Arc_AudioObject_OnVolumeChange;

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
                Arc_AudioObject L_Arc_AudioObject = new Arc_AudioObject(G_Srv_DB, L_SessionCollection[i]);
                L_Arc_AudioObject.OnNewSession += L_Arc_AudioObject_OnNewSession;
                L_Arc_AudioObject.OnVolumeHasChanged += L_Arc_AudioObject_OnVolumeChange;

                L_Arc_AudioObject._Init();
                G_Device.Sessions.Add(L_Arc_AudioObject);
            }

            //Arc_AudioObject L_Arc_AudioObject = null;
            //SessionCollection L_SessionCollection = G_Device._GetSessions();
            //for (int i = 0; i < L_SessionCollection.Count; i++)
            //{
            //    L_Arc_AudioObject = new Arc_AudioObject(G_Srv_DB, L_SessionCollection[i]);
            //    L_Arc_AudioObject.OnNewSession += L_Arc_AudioObject_OnNewSession;
            //    L_Arc_AudioObject.OnVolumeChange += L_Arc_AudioObject_OnVolumeChange;
            //    G_Sessions.Add(L_Arc_AudioObject);
            //}
        }

        //private void _LoadDevices()
        //{
        //    if (G_Device == null)
        //    {
        //        MMDevice L_DefaultDevice = G_MMDeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

        //        G_Device = _LoadDevice(L_DefaultDevice);
        //    }

        //    //G_Devices.Clear();

        //    //MMDevice L_DefaultDevice = G_MMDeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

        //    //G_Devices.Add(_LoadDevice(L_DefaultDevice, true));
        //    //foreach (var L_Device in G_MMDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
        //    //{
        //    //    if(L_Device.ID != L_DefaultDevice.ID)
        //    //    {
        //    //        G_Devices.Add(_LoadDevice(L_Device, false));
        //    //    }

        //    //}
        //}

        ////private Arc_Device _LoadDevice(MMDevice P_Device, bool P_IsDefault)
        //private Arc_Device _LoadDevice(MMDevice P_Device)
        //{
        //    PropertyStore props = P_Device.Properties;
        //    for (int i = 0; i < props.Count; i++)
        //    {
        //        object bla = props[i];
        //        string asdsd = "";
        //    }
        //    Arc_AudioObject L_Arc_AudioObject = new Arc_AudioObject(G_Srv_DB, P_Device);
        //    L_Arc_AudioObject.OnNewSession += L_Arc_AudioObject_OnNewSession;
        //    L_Arc_AudioObject.OnVolumeChange += L_Arc_AudioObject_OnVolumeChange;

        //    Arc_Device L_Arc_Device = new Arc_Device();
        //    L_Arc_Device.Name = L_Arc_AudioObject.Name;
        //    L_Arc_Device.IsVisible = P_IsDefault && G_Devices.Where(a => a.IsVisible == true).Count() == 0;
        //    L_Arc_Device.AudioObjects.Add(L_Arc_AudioObject);

        //    SessionCollection L_SessionCollection = L_Arc_AudioObject._GetSessions();
        //    for (int i = 0; i < L_SessionCollection.Count; i++)
        //    {
        //        L_Arc_AudioObject = new Arc_AudioObject(G_Srv_DB, L_SessionCollection[i]);
        //        L_Arc_AudioObject.OnNewSession += L_Arc_AudioObject_OnNewSession;
        //        L_Arc_AudioObject.OnVolumeChange += L_Arc_AudioObject_OnVolumeChange;
        //        L_Arc_Device.AudioObjects.Add(L_Arc_AudioObject);
        //    }

        //    return L_Arc_Device;
        //}



        private void L_Arc_AudioObject_OnVolumeChange(Arc_AudioObject P_Arc_AudioObject)
        {
            OnVolumeHasChanged?.Invoke(P_Arc_AudioObject);
        }

        private void L_Arc_AudioObject_OnNewSession(Arc_AudioObject_SessionInfo P_AudioCore_Object_SessionInfo)
        {
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
            if(Device.UniqueId == P_UniqueId)
            {
                return Device;
            }

            return Sessions.Where(s => s.UniqueId == P_UniqueId).FirstOrDefault();
        }
    }
}
