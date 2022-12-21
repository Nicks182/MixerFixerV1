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
        _DoNothing = 0,
        Init = 1,
        DataUpdate = 2,
        Volume_Change = 3,
        Mute_Change = 4,
        Managed_Change = 5,
        Device_On = 6,
        Device_Off = 7,
        Device_Change = 8,
        ShowMessage = 9,
        SwitchPanel = 10,
        ShowSettings = 11,

    }
}
