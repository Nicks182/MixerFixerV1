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
        }

        private void SetDefaultDevice(bool P_IsMic)
        {
            List<DB_DevicePriority> L_DB_Devices = G_Srv_DB.DevicePriority_GetAll().Find(d => d.EnforceDefault == true && d.IsMic == P_IsMic).OrderBy(d => d.Priority).ToList();

            if (L_DB_Devices.Count > 0)
            {
                MMDevice L_DefaultDevice = G_MMDeviceEnumerator.GetDefaultAudioEndpoint(_GetDataFlowType(P_IsMic), _GetRoleType(P_IsMic));
                MMDevice L_MMDevice = null;

                for(int i = 0; i < L_DB_Devices.Count; i++)
                {
                    L_MMDevice = _Get_Device(L_DB_Devices[i].Name, _GetDataFlowType(P_IsMic));
                    if(L_MMDevice != null && L_DefaultDevice != null && L_DefaultDevice.ID != L_MMDevice.ID)
                    {
                        SetDefaultEndpoint(L_MMDevice);
                        break;
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
