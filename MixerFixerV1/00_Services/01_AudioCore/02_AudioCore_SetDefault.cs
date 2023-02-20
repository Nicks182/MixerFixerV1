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
        public void SetDefault_Devices()
        {
            // Set Playback, example Speakers
            SetDefaultDevice(false);
            // Set Capture, example Microphone
            SetDefaultDevice(true);

            //CheckForVolumeChanges();
        }

        //public void CheckForVolumeChanges()
        //{
        //    try
        //    {
        //        if (G_Device != null && G_Device.Device != null)
        //        {
        //            CheckForVolumeChanges_UpdateObject(G_Device.Device);
        //        }

        //        if (G_Device != null && G_Device.Sessions != null)
        //        {
        //            for (int i = 0; i < G_Device.Sessions.Count; i++)
        //            {
        //                CheckForVolumeChanges_UpdateObject(G_Device.Sessions[i]);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //if (ex.Message.Equals("0x88890004") == false)
        //        //{
        //            throw ex;
        //        //}
        //    }
        //}

        //private void CheckForVolumeChanges_UpdateObject(Arc_AudioObject P_Arc_AudioObject)
        //{
        //    if (P_Arc_AudioObject._Get_IsActive() == true)
        //    {
        //        if (
        //            P_Arc_AudioObject._Get_Mute() != P_Arc_AudioObject._Get_DB_Mute()
        //            || P_Arc_AudioObject._Get_Volume() != P_Arc_AudioObject._Get_DB_Volume()
        //        )
        //        {
        //            if (P_Arc_AudioObject._Get_DB_Managed() == true)
        //            {
        //                P_Arc_AudioObject._Set_Mute_FromDB();
        //                P_Arc_AudioObject._Set_Volume_FromDB();
        //            }
        //            else
        //            {
        //                P_Arc_AudioObject._Update_DB_Object();
        //                OnVolumeChanged?.Invoke(P_Arc_AudioObject);
        //            }
        //        }
        //    }
        //}


        private void SetDefaultDevice(bool P_IsMic)
        {
            List<DB_DevicePriority> L_DB_Devices = G_Srv_DB.DevicePriority_GetAll().Find(d => d.EnforceDefault == true && d.IsMic == P_IsMic).OrderBy(d => d.Priority).ToList();

            if (L_DB_Devices.Count > 0)
            {
                MMDevice L_DefaultDevice = G_MMDeviceEnumerator.GetDefaultAudioEndpoint(_GetDataFlowType(P_IsMic), _GetRoleType(P_IsMic));
                if (L_DefaultDevice != null)
                {
                    MMDevice L_MMDevice = null;

                    for (int i = 0; i < L_DB_Devices.Count; i++)
                    {
                        L_MMDevice = _Get_Device(L_DB_Devices[i].Name, _GetDataFlowType(P_IsMic));
                        if (L_MMDevice != null && L_DefaultDevice.ID.Equals(L_MMDevice.ID) == true)
                        {
                            break;
                        }
                        if (L_MMDevice != null && L_DefaultDevice.ID.Equals(L_MMDevice.ID) == false)
                        {
                            SetDefaultEndpoint(L_MMDevice);
                            //OnDefaultDeviceSet?.Invoke(L_MMDevice.ID);
                            break;
                        }
                    }

                    if (P_IsMic == false && G_Device != null && G_Device.Device != null && G_Device.Device._Get_ID().Equals(L_DefaultDevice.ID) == false)
                    {

                        //Init();
                        _StartInitDelayTimer();
                        //OnDefaultDeviceSet?.Invoke(L_MMDevice.ID);
                    }
                }
            }
            else
            {
                MMDevice L_DefaultDevice = G_MMDeviceEnumerator.GetDefaultAudioEndpoint(_GetDataFlowType(P_IsMic), _GetRoleType(P_IsMic));
                if (L_DefaultDevice != null)
                {
                    if (_Get_Current_Device_ID(_GetDataFlowType(P_IsMic)).Equals(L_DefaultDevice.ID) == false)
                    {
                        //Init();
                        _StartInitDelayTimer();
                        //OnDefaultDeviceSet?.Invoke(null);
                    }
                }
            }
        }

        private void SetDefaultEndpoint(MMDevice P_MMDevice)
        {
            PolicyConfigClient client = new PolicyConfigClient();
            client.SetDefaultEndpoint(P_MMDevice.ID, ERole.eCommunications);
            client.SetDefaultEndpoint(P_MMDevice.ID, ERole.eMultimedia);
            
        }

        private string _Get_Current_Device_ID(DataFlow P_DeviceType)
        {
            if(P_DeviceType == DataFlow.Capture)
            {
                return G_Device_Mic.Device._Get_ID();
            }
            else
            {
                return G_Device.Device._Get_ID();
            }
            

        }

        private MMDevice _Get_Device(string P_Name, DataFlow P_DeviceType)
        {
            return G_MMDeviceEnumerator.EnumerateAudioEndPoints(P_DeviceType, DeviceState.Active).Where(d => d.FriendlyName.ToLower() == P_Name.ToLower()).FirstOrDefault();
            
        }

        private Role _GetRoleType(bool P_IsMic)
        {
            if(P_IsMic == true)
            {
                return Role.Communications;
            }

            return Role.Multimedia;
        }

        private DataFlow _GetDataFlowType(bool P_IsMic)
        {
            if (P_IsMic == true)
            {
                return DataFlow.Capture;
            }

            return DataFlow.Render;
        }

    }
    
}
