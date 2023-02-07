
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlGenerator;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace Services
{
    public partial class Arc_AudioObject
    {
        public void _Init()
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
                            if (G_MMDevice.IconPath.Contains("mmres.dll") == true)
                            {
                                G_Image = null;
                            }
                            else
                            {
                                G_Image = Srv_Utils_NativeMethods.GetIconFromFile(G_MMDevice.IconPath).ToBitmap();
                            }
                            try
                            {
                                G_DisplayName = G_MMDevice.FriendlyName;
                            }
                            catch
                            {
                                try
                                {
                                    G_DisplayName = G_MMDevice.DeviceFriendlyName;
                                }
                                catch
                                {
                                    G_DisplayName = "Uknown Device";
                                }
                            }
                            //G_SessionId_Base64 = G_MMDevice.ID.EncodeBase64();
                            G_SessionId_Base64 = G_DisplayName.EncodeBase64();
                        }
                    }
                    break;

                case Arc_AudioObject_Type.IsSession:
                    {
                        //G_SessionId_Base64 = G_AudioSessionControl.GetSessionIdentifier.EncodeBase64();
                        Process process = Process.GetProcessById((int)G_AudioSessionControl.GetProcessID);

                        

                        if (G_AudioSessionControl.IsSystemSoundsSession)
                        {
                            G_IsSystemSounds = true;
                            G_DisplayName = "System Sounds";
                            G_Icon = HTML_Object_Icon.music_video;
                        }
                        else
                        {
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

                            //G_Name = G_AudioSessionControl.DisplayName == "" ? process.ProcessName : process.MainWindowTitle;
                            G_DisplayName = G_AudioSessionControl.DisplayName;

                            if (string.IsNullOrEmpty(G_DisplayName) == true)
                            {
                                G_DisplayName = process.ProcessName;
                            }

                            if (string.IsNullOrEmpty(G_DisplayName) == true)
                            {
                                G_DisplayName = process.MainWindowTitle;
                            }
                        }

                        G_SessionId_Base64 = G_DisplayName.EncodeBase64();

                        G_AudioSessionControl.UnRegisterEventClient(this);
                        G_AudioSessionControl.RegisterEventClient(this);
                    }
                    break;
            }

            _Init_DBObject();

            
        }

        private void _Init_DBObject()
        {
            DB_AudioObject L_DB_AudioObject = G_Srv_DB.AudioObject_GetOne(G_SessionId_Base64);
            if (L_DB_AudioObject == null)
            {


                L_DB_AudioObject = new DB_AudioObject
                {
                    Id = Guid.Empty, // New object
                    IsActive = false,
                    IsDevice = (G_ObjectType == Arc_AudioObject_Type.IsDevice || G_ObjectType == Arc_AudioObject_Type.IsMicrophone),
                    IsManaged = false,
                    IsMute = _Get_Mute(),
                    Name = G_DisplayName,
                    SessionId_Base64 = G_SessionId_Base64,
                    Volume = _Get_Volume()
                };

                G_Srv_DB.AudioObject_Save(L_DB_AudioObject);
            }
            else
            {
                if (L_DB_AudioObject.IsManaged == true)
                {
                    if (_Get_Volume() != L_DB_AudioObject.Volume)
                    {
                        _Set_Volume(L_DB_AudioObject.Volume);
                    }

                    if (_Get_Mute() != L_DB_AudioObject.IsMute)
                    {
                        _Set_Mute(L_DB_AudioObject.IsMute);
                    }
                }
            }

            _Init_DBObject_SetAppDefaultVolume(L_DB_AudioObject);

        }

        private void _Init_DBObject_SetAppDefaultVolume(DB_AudioObject G_DB_AudioObject)
        {
            if (G_ObjectType == Arc_AudioObject_Type.IsSession && G_DB_AudioObject.IsManaged == false) // Only run when Session/Application
            {
                DB_Settings L_DefaultVolumeEnable = G_Srv_DB.Settings_GetOne(G_Srv_DB.G_DefaultVolumeEnable);
                if (L_DefaultVolumeEnable.Value == "1")
                {
                    DB_Settings L_DefaultVolume = G_Srv_DB.Settings_GetOne(G_Srv_DB.G_DefaultVolume);
                    if (_Get_Volume() != G_Srv_Utils._Volume_FromString(L_DefaultVolume.Value))
                    {
                        _Set_Volume(L_DefaultVolume.Value);
                    }
                    //this._Set_Volume(L_DefaultVolume.Value);
                    //G_AudioSessionControl.SimpleAudioVolume.Volume = Srv_Utils._Volume_FromString(L_DefaultVolume.Value);
                }
            }
        }

        public string _Get_ID()
        {
            switch (G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    return G_MMDevice.ID;

            }

            return string.Empty;
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
                        return new Arc_AudioObject_PeakVolum
                        {
                            Master = Math.Floor(G_MMDevice.AudioMeterInformation.MasterPeakValue * 100),
                            Left = _Get_PeakVolume_Left(G_MMDevice.AudioMeterInformation),
                            Right = _Get_PeakVolume_Right(G_MMDevice.AudioMeterInformation),
                        };

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
                    //if (G_MMDevice.State == DeviceState.Active)
                    //{
                        return G_MMDevice.AudioEndpointVolume.Mute;
                    //}
                    break;

                case Arc_AudioObject_Type.IsSession:
                    //if (G_AudioSessionControl.State == AudioSessionState.AudioSessionStateActive)
                    //{
                        return G_AudioSessionControl.SimpleAudioVolume.Mute;
                    //}
                    break;

            }

            return false;
        }

        public void _Set_Mute(bool P_Mute)
        {
            DB_AudioObject L_DB_AudioObject = G_Srv_DB.AudioObject_GetOne(G_SessionId_Base64);

            switch (G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    if (G_MMDevice.State == DeviceState.Active)
                    {
                        G_MMDevice.AudioEndpointVolume.Mute = P_Mute;
                    }
                    break;

                case Arc_AudioObject_Type.IsSession:
                    G_AudioSessionControl.SimpleAudioVolume.Mute = P_Mute;
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
            DB_AudioObject L_DB_AudioObject = G_Srv_DB.AudioObject_GetOne(G_SessionId_Base64);
            return L_DB_AudioObject.IsManaged;
        }

        public bool _Set_Managed()
        {
            DB_AudioObject L_DB_AudioObject = G_Srv_DB.AudioObject_GetOne(G_SessionId_Base64);

            L_DB_AudioObject.IsManaged = !L_DB_AudioObject.IsManaged;
            G_Srv_DB.AudioObject_Save(L_DB_AudioObject);
            return _Get_Managed();
        }

        public int _Get_Volume()
        {
            switch (G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    //{
                    //    if (G_MMDevice.State == DeviceState.Active)
                    //    {
                            return (int)Math.Round(G_MMDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100, 0);
                    //    }
                    //    break;
                    //}

                case Arc_AudioObject_Type.IsSession:
                    //if (G_AudioSessionControl.State == AudioSessionState.AudioSessionStateActive)
                    //{
                        return (int)Math.Round(G_AudioSessionControl.SimpleAudioVolume.Volume * 100, 0);
                    //}
                    break;
                    
            }

            return 0;
        }

        public void _Set_Volume(string P_Value)
        {
            _Update_DB_Volume(Convert.ToInt32(P_Value));
            
            _Set_Volume(Convert.ToInt32(P_Value));
            
        }

        private void _Set_Volume(int P_Value)
        {
            switch (G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    //if ((int)Math.Floor(G_MMDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100) != P_Value)
                    //{
                        G_MMDevice.AudioEndpointVolume.MasterVolumeLevelScalar = G_Srv_Utils._Volume_FromInt(P_Value);
                    //}
                    break;


                case Arc_AudioObject_Type.IsSession:
                    //if ((int)Math.Floor(G_AudioSessionControl.SimpleAudioVolume.Volume * 100) != P_Value)
                    //{
                        G_AudioSessionControl.SimpleAudioVolume.Volume = G_Srv_Utils._Volume_FromInt(P_Value);
                    //}
                    //float volume;
                    //volume = 10 / 100.0f;
                    //var newVolume = volume / 1;
                    //G_AudioSessionControl.SimpleAudioVolume.Volume = newVolume;

                    break;

            }
        }


        private void _Update_DB_Volume(int P_Volume)
        {
            DB_AudioObject L_DB_AudioObject = G_Srv_DB.AudioObject_GetOne(G_SessionId_Base64);
            L_DB_AudioObject.Volume = P_Volume;

            G_Srv_DB.AudioObject_Save(L_DB_AudioObject);
        }

        public void _Update_DB_Object()
        {
            DB_AudioObject L_DB_AudioObject = G_Srv_DB.AudioObject_GetOne(G_SessionId_Base64);

            L_DB_AudioObject.Volume = _Get_Volume();
            L_DB_AudioObject.IsMute = _Get_Mute();

            G_Srv_DB.AudioObject_Save(L_DB_AudioObject);
        }

        public void Dispose()
        {
            switch (G_ObjectType)
            {
                case Arc_AudioObject_Type.IsDevice:
                case Arc_AudioObject_Type.IsMicrophone:
                    G_MMDevice.Dispose();
                    break;

                case Arc_AudioObject_Type.IsSession:
                    G_AudioSessionControl.Dispose();
                    break;
            }
        }
    }

}
