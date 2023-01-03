
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
                            // We need to call _Get_PeakVolume once during INIT so the objects are set when we want to use them later.
                            // If we don't, we get cross thread issues.
                            Arc_AudioObject_PeakVolum bla = this._Get_PeakVolume();
                            G_MMDevice.AudioEndpointVolume.OnVolumeNotification += AudioEndpointVolume_OnVolumeNotification;
                            G_MMDevice.AudioSessionManager.OnSessionCreated += AudioSessionManager_OnSessionCreated;
                            //G_Image = Icon.ExtractAssociatedIcon(G_MMDevice.IconPath).ToBitmap();
                            G_Image = Srv_Utils_NativeMethods.GetIconFromFile(G_MMDevice.IconPath).ToBitmap();
                            G_Name = G_MMDevice.FriendlyName;
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
                            //G_Name = G_AudioSessionControl.DisplayName == "" ? process.ProcessName : process.MainWindowTitle;
                            G_Name = G_AudioSessionControl.DisplayName;

                            if (string.IsNullOrEmpty(G_Name) == true)
                            {
                                G_Name = process.ProcessName;
                            }

                            if (string.IsNullOrEmpty(G_Name) == true)
                            {
                                G_Name = process.MainWindowTitle;
                            }
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
                    IsDevice = (G_ObjectType == Arc_AudioObject_Type.IsDevice || G_ObjectType == Arc_AudioObject_Type.IsMicrophone),
                    IsManaged = false,
                    IsMute = _Get_Mute(),
                    Name = G_Name,
                    Volume = _Get_Volume()
                };

                G_Srv_DB.AudioObject_Save(G_DB_AudioObject);
            }
            else
            {
                if(G_DB_AudioObject.IsManaged == true)
                {
                    _Set_Volume_FromDB();
                    _Set_Mute_FromDB();
                }
            }

            _Init_DBObject_SetAppDefaultVolume(G_DB_AudioObject);
        }

        private void _Init_DBObject_SetAppDefaultVolume(DB_AudioObject G_DB_AudioObject)
        {
            if (G_ObjectType == Arc_AudioObject_Type.IsSession && G_DB_AudioObject.IsManaged == false) // Only run when Session/Application
            {
                DB_Settings L_DefaultVolumeEnable = G_Srv_DB.Settings_GetOne(G_Srv_DB.G_DefaultVolumeEnable);
                if (L_DefaultVolumeEnable.Value == "1")
                {
                    DB_Settings L_DefaultVolume = G_Srv_DB.Settings_GetOne(G_Srv_DB.G_DefaultVolume);
                    this._Set_Volume(L_DefaultVolume.Value);
                    //G_AudioSessionControl.SimpleAudioVolume.Volume = Srv_Utils._Volume_FromString(L_DefaultVolume.Value);
                }
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

        public Arc_AudioObject_PeakVolum _Get_PeakVolume()
        {
            try
            {
                switch (G_ObjectType)
                {
                    case Arc_AudioObject_Type.IsDevice:
                    case Arc_AudioObject_Type.IsMicrophone:
                        return new Arc_AudioObject_PeakVolum
                        {
                            Master = Math.Floor(G_MMDevice.AudioMeterInformation.MasterPeakValue * 100),
                            Left = _Get_PeakVolume_Left(G_MMDevice.AudioMeterInformation),
                            Right = _Get_PeakVolume_Right(G_MMDevice.AudioMeterInformation),
                        };
                    //return Math.Floor(G_MMDevice.AudioMeterInformation.MasterPeakValue * 100);


                    case Arc_AudioObject_Type.IsSession:
                        return new Arc_AudioObject_PeakVolum
                        {
                            Master = Math.Floor(G_AudioSessionControl.AudioMeterInformation.MasterPeakValue * 100),
                            Left = _Get_PeakVolume_Left(G_AudioSessionControl.AudioMeterInformation),
                            Right = _Get_PeakVolume_Right(G_AudioSessionControl.AudioMeterInformation),
                        };
                    //return Math.Floor(G_AudioSessionControl.AudioMeterInformation.MasterPeakValue * 100);

                    default:
                        return new Arc_AudioObject_PeakVolum();
                        //return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private double _Get_PeakVolume_Left(AudioMeterInformation P_Info)
        {
            if(P_Info.PeakValues.Count > 0)
            {
                return Math.Floor(P_Info.PeakValues[0] * 100);
            }

            return Math.Floor(P_Info.MasterPeakValue * 100);
        }

        private double _Get_PeakVolume_Right(AudioMeterInformation P_Info)
        {
            if (P_Info.PeakValues.Count > 0)
            {
                return Math.Floor(P_Info.PeakValues[1] * 100);
            }

            return Math.Floor(P_Info.MasterPeakValue * 100);
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

        private void _Set_Mute_FromDB()
        {
            switch (G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    if (G_MMDevice.State == DeviceState.Active)
                    {
                        G_MMDevice.AudioEndpointVolume.Mute = G_DB_AudioObject.IsMute;
                    }
                    break;

                case Arc_AudioObject_Type.IsSession:
                    G_AudioSessionControl.SimpleAudioVolume.Mute = G_DB_AudioObject.IsMute;
                    break;

            }

        }

        public void _Change_Mute()
        {
            switch (G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    if (G_MMDevice.State == DeviceState.Active)
                    {
                        G_MMDevice.AudioEndpointVolume.Mute = !G_MMDevice.AudioEndpointVolume.Mute;
                    }
                    break;

                case Arc_AudioObject_Type.IsSession:
                    G_AudioSessionControl.SimpleAudioVolume.Mute = !G_AudioSessionControl.SimpleAudioVolume.Mute;
                    break;
            }

            _Update_DB_Object();
        }

        public bool _Get_Managed()
        {
            return G_DB_AudioObject.IsManaged;
        }

        public bool _Set_Managed()
        {
            G_DB_AudioObject.IsManaged = !G_DB_AudioObject.IsManaged;
            _Update_DB_Object();
            return _Get_Managed();
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
            _Set_Volume(Convert.ToDouble(P_Value));
            _Update_DB_Object();
        }

        private void _Set_Volume(double P_Value)
        {
            switch (G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    G_MMDevice.AudioEndpointVolume.MasterVolumeLevelScalar = Srv_Utils._Volume_FromDouble(P_Value);
                    break;


                case Arc_AudioObject_Type.IsSession:
                    G_AudioSessionControl.SimpleAudioVolume.Volume = Srv_Utils._Volume_FromDouble(P_Value);
                    break;

            }
        }

        private void _Set_Volume_FromDB()
        {
            _Set_Volume(G_DB_AudioObject.Volume);
        }


        private void _Update_DB_Object()
        {
            G_DB_AudioObject.Volume = _Get_Volume();
            G_DB_AudioObject.IsMute = _Get_Mute();

            G_Srv_DB.AudioObject_Save(G_DB_AudioObject);
        }
    }

}
