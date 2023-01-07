using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace Services
{
    public partial class Arc_AudioObject
    {
        public event EventHandler DeviceChanged;
        public event EventHandler<MuteEventArgs> MuteChanged;
        public event EventHandler<VolumeEventArgs> VolumeChanged;

        public delegate void OnNewSessionDelegate(Arc_AudioObject_SessionInfo P_AudioCore_Object_SessionInfo);
        public event OnNewSessionDelegate OnNewSession;

        public delegate void OnVolumeHasChangedDelegate(Arc_AudioObject sender);
        public event OnVolumeHasChangedDelegate OnVolumeHasChanged;



        private void AudioSessionManager_OnSessionCreated(object sender, IAudioSessionControl newSession)
        {
            OnNewSession?.Invoke(new Arc_AudioObject_SessionInfo { Sender = sender, NewSession = newSession });
        }

        void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            DB_AudioObject L_DB_AudioObject = G_Srv_DB.AudioObject_GetOne(G_Name);
            if (L_DB_AudioObject.IsManaged == true)
            {
                if (Convert.ToInt32(data.MasterVolume * 100) != L_DB_AudioObject.Volume)
                {
                    _Set_Volume(L_DB_AudioObject.Volume);
                }

                if (data.Muted != L_DB_AudioObject.IsMute)
                {
                    _Set_Mute(L_DB_AudioObject.IsMute);
                }
            }
            else
            {
                _Update_DB_Object();
                OnVolumeHasChanged?.Invoke(this);
            }
        }

        public void OnVolumeChanged(float volume, bool isMuted)
        {
            DB_AudioObject L_DB_AudioObject = G_Srv_DB.AudioObject_GetOne(G_Name);

            if (L_DB_AudioObject.IsManaged == true)
            {
                if (Convert.ToInt32(volume * 100) != L_DB_AudioObject.Volume)
                {
                    _Set_Volume(L_DB_AudioObject.Volume);
                }

                if (isMuted != L_DB_AudioObject.IsMute)
                {
                    _Set_Mute(L_DB_AudioObject.IsMute);
                }
            }
            else
            {
                _Update_DB_Object();
                
            }
            
            OnVolumeHasChanged?.Invoke(this);
        }


        public void OnDisplayNameChanged(string displayName)
        {

        }

        public void OnIconPathChanged(string iconPath)
        {
        }

        public void OnChannelVolumeChanged(uint channelCount, IntPtr newVolumes, uint channelIndex)
        {
        }

        public void OnGroupingParamChanged(ref Guid groupingId)
        {
        }

        public void OnStateChanged(AudioSessionState state)
        {
            if (G_AudioSessionControl != null && G_AudioSessionControl.State == AudioSessionState.AudioSessionStateExpired)
            {
                string bla = "";

                OnNewSession?.Invoke(new Arc_AudioObject_SessionInfo());
            }
        }

        public void OnSessionDisconnected(AudioSessionDisconnectReason disconnectReason)
        {
            string bla = "";
            //OnNewSession?.Invoke(new Arc_AudioObject_SessionInfo { disconnectReason = disconnectReason });
        }
    }

    public class Arc_AudioObject_SessionInfo
    {
        public object Sender { get; set; }
        public IAudioSessionControl NewSession { get; set; }
        public AudioSessionDisconnectReason disconnectReason { get; set; }
    }

    public class MuteEventArgs : EventArgs
    {
        public MuteEventArgs(bool muted)
        {
            Muted = muted;
        }

        public bool Muted { get; private set; }
    }

    public class VolumeEventArgs : EventArgs
    {
        public VolumeEventArgs(float volume)
        {
            Volume = volume;
        }

        public float Volume { get; private set; }
    }
}
