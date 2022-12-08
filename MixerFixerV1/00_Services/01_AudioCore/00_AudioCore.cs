
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
        MMDeviceEnumerator G_MMDeviceEnumerator = new MMDeviceEnumerator();
        Srv_DB G_Srv_DB = new Srv_DB();
        
        List<Arc_Device> G_Devices = new List<Arc_Device>();
        public List<Arc_Device> Devices { get { return G_Devices; } }

        public bool G_ShowSettings = false;

        public delegate void DoUpdateDelegate();
        public event DoUpdateDelegate DoUpdate;

        public delegate void OnVolumeChangedDelegate(Arc_AudioObject P_Arc_AudioObject);
        public event OnVolumeChangedDelegate OnVolumeChanged;

        public Srv_AudioCore()
        {

        }

        public void Init()
        {
            _LoadDevices();
        }

        public Arc_Device _Get_VisibleDevice()
        {
            Arc_Device L_Arc_Device = G_Devices.Where(d => d.IsVisible == true).FirstOrDefault();
            
            return L_Arc_Device;
        }

        public Arc_Device _Set_VisibleDevice(string P_DeviceId)
        {
            Guid L_DeviceId = Guid.Parse(P_DeviceId);

            Arc_Device L_Arc_Device = G_Devices.Where(d => d.Id == L_DeviceId).FirstOrDefault();
            if (L_Arc_Device != null)
            {
                for (int i = 0; i < G_Devices.Count; i++)
                {
                    G_Devices[i].IsVisible = false;
                }

                L_Arc_Device.IsVisible = true;
            }

            return L_Arc_Device;
        }

    }
}
