
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlGenerator;
using MixerFixerV1;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace Services
{
    public partial class Arc_AudioObject : IAudioSessionEventsHandler
    {
        Arc_AudioObject_Type G_ObjectType { get; set; }
        Srv_DB G_Srv_DB = null;
        Srv_Utils G_Srv_Utils = new Srv_Utils();
        MMDevice G_MMDevice = null;
        AudioSessionControl G_AudioSessionControl = null;

        //DB_AudioObject G_DB_AudioObject = null;

        private Guid G_UniqueId = Guid.NewGuid(); // used only during running of app. Only temporarily
        public Guid UniqueId{get { return G_UniqueId; }}

        private string G_DisplayName { get; set; }
        public string DisplayName { get { return G_DisplayName; }}

        private string G_SessionId_Base64 { get; set; }
        public string SessionId_Base64 { get { return G_SessionId_Base64; } }

        private Bitmap G_Image { get; set; }
        public Bitmap Image{get { return G_Image; }}

        //private bool G_IsManaged { get; set; }
        //public bool IsManaged { get { return G_IsManaged; } }

        private bool G_IsIgnore { get; set; }
        public bool IsIgnore { get { return G_IsIgnore; } }

        private bool G_IsMute { get; set; }
        public bool IsMute { get { return G_IsMute; } }

        private bool G_IsSystemSounds { get; set; } = false;
        public bool IsSystemSounds { get { return G_IsSystemSounds; } }

        public HTML_Object_Icon G_Icon { get; set; }

        //public Arc_AudioObject(Srv_DB P_Srv_DB, MMDevice P_MMDevice)
        public Arc_AudioObject(MMDevice P_MMDevice)
        {
            G_Srv_DB = App.ServiceProvider.GetService(typeof(Srv_DB)) as Srv_DB;

            G_ObjectType = Arc_AudioObject_Type.IsDevice;
            //G_Srv_DB = P_Srv_DB;
            G_MMDevice = P_MMDevice;
            G_Icon = HTML_Object_Icon.speaker;
            //_Init();
        }

        public Arc_AudioObject(MMDevice P_MMDevice, bool P_IsMic)
        {
            G_Srv_DB = App.ServiceProvider.GetService(typeof(Srv_DB)) as Srv_DB;

            G_ObjectType = Arc_AudioObject_Type.IsMicrophone;
            //G_Srv_DB = P_Srv_DB;
            G_MMDevice = P_MMDevice;
            G_Icon = HTML_Object_Icon.mic;
            //_Init();
        }

        //public Arc_AudioObject(Srv_DB P_Srv_DB, AudioSessionControl P_AudioSessionControl)
        public Arc_AudioObject(AudioSessionControl P_AudioSessionControl)
        {
            G_Srv_DB = App.ServiceProvider.GetService(typeof(Srv_DB)) as Srv_DB;

            G_ObjectType = Arc_AudioObject_Type.IsSession;
            //G_Srv_DB = P_Srv_DB;
            G_AudioSessionControl = P_AudioSessionControl;
            G_Icon = HTML_Object_Icon.blur_circular;
            //_Init();
        }

    }

    public enum Arc_AudioObject_Type
    {
        IsDevice,
        IsMicrophone,
        IsSession,
    }

    public class Arc_AudioObject_PeakVolum
    {
        public double Master { get; set; }
        public double Left { get; set; }
        public double Right { get; set; }
    }
}
