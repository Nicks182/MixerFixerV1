
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace Services
{
    public partial class Arc_AudioObject
    {
        private void _Init()
        {
            switch(G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    {
                        if (G_MMDevice.State == DeviceState.Active)
                        {
                            double bla = this._Get_PeakVolume();
                            G_MMDevice.AudioEndpointVolume.OnVolumeNotification += AudioEndpointVolume_OnVolumeNotification;
                            G_MMDevice.AudioSessionManager.OnSessionCreated += AudioSessionManager_OnSessionCreated;
                            //G_Image = Icon.ExtractAssociatedIcon(G_MMDevice.IconPath).ToBitmap();
                            G_Image = Srv_Utils_NativeMethods.GetIconFromFile(G_MMDevice.IconPath).ToBitmap();
                            G_Name = G_MMDevice.DeviceFriendlyName;
                        }
                    }
                    break;

                case Arc_AudioObject_Type.IsSession:
                    {
                        Process process = Process.GetProcessById((int)G_AudioSessionControl.GetProcessID);

                        Icon L_Icon = Srv_Utils_NativeMethods.GetIconFromFile(G_AudioSessionControl.IconPath);
                        if (L_Icon == null)
                        {
                            try
                            {
                                G_Image = Srv_Utils_NativeMethods.GetIconFromFile(process.MainModule.FileName).ToBitmap();
                            }
                            catch { }
                        }
                        else
                        {
                            G_Image = L_Icon.ToBitmap();
                        }

                        if (G_AudioSessionControl.IsSystemSoundsSession)
                        {
                            G_Name = "System Sounds";
                        }
                        else
                        {
                            G_Name = G_AudioSessionControl.DisplayName != "" ? process.ProcessName : process.MainWindowTitle;
                        }

                        
                        G_AudioSessionControl.RegisterEventClient(this);
                    }
                    break;
            }

            _Init_DBObject();

            
        }

        private void _Init_DBObject()
        {
            G_DB_AudioObject = G_Srv_DB.AudioObject_GetOne(G_Name);

            if (G_DB_AudioObject == null)
            {
                G_DB_AudioObject = new DB_AudioObject
                {
                    Id = Guid.Empty, // New object
                    IsActive = false,
                    IsDefault = false,
                    IsManaged = IsManaged,
                    IsMute = _Get_Mute(),
                    Name = G_Name,
                    Priority = 0,
                    Volume = _Get_Volume()
                };

                G_Srv_DB.AudioObject_Save(G_DB_AudioObject);
            }
        }

        public SessionCollection _GetSessions()
        {
            if (G_ObjectType == Arc_AudioObject_Type.IsDevice)
            {
                G_MMDevice.AudioSessionManager.RefreshSessions();
                return G_MMDevice.AudioSessionManager.Sessions;
            }

            return null;
        }

        public double _Get_PeakVolume()
        {
            switch (G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    return Math.Floor(G_MMDevice.AudioMeterInformation.MasterPeakValue * 100);


                case Arc_AudioObject_Type.IsSession:
                    return Math.Floor(G_AudioSessionControl.AudioMeterInformation.MasterPeakValue * 100);

                default:
                    return 0;
            }

        }

        public bool _Get_Mute()
        {
            switch (G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    {
                        if (G_MMDevice.State == DeviceState.Active)
                        {
                            return G_MMDevice.AudioEndpointVolume.Mute;
                        }
                        break;
                    }

                case Arc_AudioObject_Type.IsSession:
                    return G_AudioSessionControl.SimpleAudioVolume.Mute;

            }

            return false;
        }

        public bool _Set_Mute()
        {
            switch (G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    {
                        if (G_MMDevice.State == DeviceState.Active)
                        {
                            G_MMDevice.AudioEndpointVolume.Mute = !G_MMDevice.AudioEndpointVolume.Mute;
                            return G_MMDevice.AudioEndpointVolume.Mute;
                        }
                        break;
                    }

                case Arc_AudioObject_Type.IsSession:
                    {
                        G_AudioSessionControl.SimpleAudioVolume.Mute = !G_AudioSessionControl.SimpleAudioVolume.Mute;
                        return G_AudioSessionControl.SimpleAudioVolume.Mute;
                    }

            }

            return false;
        }

        public bool _Get_Managed()
        {
            return G_IsManaged;
        }

        public bool _Set_Managed()
        {
            G_IsManaged = !G_IsManaged;
            return G_IsManaged;
        }

        public double _Get_Volume()
        {
            switch (G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    {
                        if (G_MMDevice.State == DeviceState.Active)
                        {
                            return Math.Floor(G_MMDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
                        }
                        break;
                    }

                case Arc_AudioObject_Type.IsSession:
                    return Math.Floor(G_AudioSessionControl.SimpleAudioVolume.Volume * 100);
                    
            }

            return 0;
        }

        public void _Set_Volume(string P_Value)
        {
            if (G_DB_AudioObject != null)
            {
                G_DB_AudioObject.Volume = Convert.ToDouble(P_Value);
            }
            switch (G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    //G_MMDevice = new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                    G_MMDevice.AudioEndpointVolume.MasterVolumeLevelScalar = Srv_Utils._VolumeFromUI(P_Value);
                    break;


                case Arc_AudioObject_Type.IsSession:
                    G_AudioSessionControl.SimpleAudioVolume.Volume = Srv_Utils._VolumeFromUI(P_Value);
                    break;

            }
        }

    }

}
