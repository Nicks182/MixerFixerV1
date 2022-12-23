
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
        Srv_DB G_Srv_DB;

        //Arc_AudioObject G_Device { get; set; }
        //List<Arc_AudioObject> G_Sessions = new List<Arc_AudioObject>();

        //List<Arc_Device> G_Devices = new List<Arc_Device>();
        //public List<Arc_Device> Devices { get { return G_Devices; } }

        Arc_Device G_Device { get; set; }
        public Arc_Device Device { get { return G_Device; } }

        public bool G_ShowSettings = false;

        public delegate void DoUpdateDelegate();
        public event DoUpdateDelegate DoUpdate;

        public delegate void OnVolumeChangedDelegate(Arc_AudioObject P_Arc_AudioObject);
        public event OnVolumeChangedDelegate OnVolumeChanged;

        public Srv_AudioCore(Srv_DB P_Srv_DB)
        {
            G_Srv_DB = P_Srv_DB;
        }

        public void Init()
        {
            _LoadDevice();
            
        }

        public void Reload()
        {
            _LoadSessions();

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
