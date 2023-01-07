using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Web_InterCommMessage
    {
        public Web_InterCommMessage()
        {
            HTMLs = new List<Web_InterCommMessage_HTML>();
            Data = new List<Web_InterCommMessage_Data>();
        }

        public Web_InterCommMessage_Type CommType { get; set; }
        public string ClientId { get; set; }

        public List<Web_InterCommMessage_HTML> HTMLs { get; set; }
        public List<Web_InterCommMessage_Data> Data { get; set; }

        public Web_InterCommMessage_Modal ModalInfo { get; set; }

    }

    public class Web_InterCommMessage_Modal
    {
        public string Id { get; set; }
        public string Focus { get; set; }
        public int State { get; set; }
    }

    public class Web_InterCommMessage_Data
    {
        public string Id { get;set; }
        public string Value { get; set; }
        public Web_InterCommMessage_DataType DataType { get; set; }

    }

    public enum Web_InterCommMessage_DataType
    {
        Progress = 0,
        Text = 1,
        Slider = 2,
        ButtonText = 3,
        Toggle = 4,
    }

    public class Web_InterCommMessage_HTML
    {
        public string ContainerId { get; set; }
        public string HTML { get; set; }
        public bool IsAppend {get;set;}
    }

    public enum Web_InterCommMessage_Type
    {
        _DoNothing,
        _Log,
        Init,
        DataUpdate,
        Volume_Change,
        Volume_ModalShow,
        Volume_ModalSet,
        Mute_Change,
        Managed_Change,
        Device_On,
        Device_Off,
        Device_Change,
        ShowMessage,
        SwitchPanel,
        Settings_Show,
        Settings_Priority_Enforce,
        Settings_Priority_MoveUp,
        Settings_Priority_MoveDown,
        Settings_UseDefault_Change,
        Settings_DefaultVolume_Show,
        Settings_DefaultVolume_Change,

        Theme_Show,
        Theme_Reset,
        Theme_Color_Changed,
        Theme_Color_Change_Red,
        Theme_Color_Change_Green,
        Theme_Color_Change_Blue,

    }
}
