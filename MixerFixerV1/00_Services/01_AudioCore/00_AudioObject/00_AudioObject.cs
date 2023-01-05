
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        DB_AudioObject G_DB_AudioObject = null;

        private Guid G_UniqueId = Guid.NewGuid(); // used only during running of app. Only temporarily
        public Guid UniqueId{get { return G_UniqueId; }}

        private string G_Name { get; set; }
        public string Name{get { return G_Name; }}
        
        private Bitmap G_Image { get; set; }
        public Bitmap Image{get { return G_Image; }}

        //private bool G_IsManaged { get; set; }
        //public bool IsManaged { get { return G_IsManaged; } }

        private bool G_IsMute { get; set; }
        public bool IsMute { get { return G_IsMute; } }


        public Arc_AudioObject(Srv_DB P_Srv_DB, MMDevice P_MMDevice)
        {
            G_ObjectType = Arc_AudioObject_Type.IsDevice;
            G_Srv_DB = P_Srv_DB;
            G_MMDevice = P_MMDevice;

            _Init();
        }

        public Arc_AudioObject(Srv_DB P_Srv_DB, AudioSessionControl P_AudioSessionControl)
        {
            G_ObjectType = Arc_AudioObject_Type.IsSession;
            G_Srv_DB = P_Srv_DB;
            G_AudioSessionControl = P_AudioSessionControl;

            _Init();
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
