using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoreAudioApi;
using NAudio.CoreAudioApi;

namespace Services
{
    public partial class Srv_AudioCore
    {


        private void G_NotificationClientImplementation_OnDefaultDeviceChange(DataFlow dataFlow, Role deviceRole, string defaultDeviceId)
        {
            //OnDefaultDeviceChange?.Invoke(dataFlow, deviceRole, defaultDeviceId);
        }

        private void G_NotificationClientImplementation_OnDeviceStateChange(string P_DeviceId, DeviceState P_NewState)
        {
            OnDeviceStateChange?.Invoke(P_DeviceId, P_NewState);
        }
    }
    
    class NotificationClientImplementation : NAudio.CoreAudioApi.Interfaces.IMMNotificationClient
    {
        public delegate void OnDefaultDeviceChangedDelegate(DataFlow dataFlow, Role deviceRole, string defaultDeviceId);
        public event OnDefaultDeviceChangedDelegate OnDefaultDeviceChange;

        public delegate void OnDeviceStateChangedDelegate(string P_DeviceId, DeviceState P_NewState);
        public event OnDeviceStateChangedDelegate OnDeviceStateChange;

        public void OnDefaultDeviceChanged(DataFlow dataFlow, Role deviceRole, string defaultDeviceId)
        {
            OnDefaultDeviceChange?.Invoke(dataFlow, deviceRole, defaultDeviceId);
            //Do some Work
            //Console.WriteLine("OnDefaultDeviceChanged --> {0}", dataFlow.ToString());
        }

        public void OnDeviceAdded(string deviceId)
        {
            //Do some Work
            Console.WriteLine("OnDeviceAdded -->");
        }

        public void OnDeviceRemoved(string deviceId)
        {

            Console.WriteLine("OnDeviceRemoved -->");
            //Do some Work
        }

        public void OnDeviceStateChanged(string deviceId, DeviceState newState)
        {
            OnDeviceStateChange?.Invoke(deviceId, newState);
            Console.WriteLine("OnDeviceStateChanged\n Device Id -->{0} : Device State {1}", deviceId, newState);
            //Do some Work
        }

        public NotificationClientImplementation()
        {
            //_realEnumerator.RegisterEndpointNotificationCallback();
            if (System.Environment.OSVersion.Version.Major < 6)
            {
                throw new NotSupportedException("This functionality is only supported on Windows Vista or newer.");
            }
        }

        public void OnPropertyValueChanged(string deviceId, PropertyKey propertyKey)
        {
            //Do some Work
            //fmtid & pid are changed to formatId and propertyId in the latest version NAudio
            Console.WriteLine("OnPropertyValueChanged: formatId --> {0}  propertyId --> {1}", propertyKey.formatId.ToString(), propertyKey.propertyId.ToString());
        }

    }

}
