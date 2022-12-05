using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlGenerator;
using Services;

namespace Web
{
    public partial class HTML_Templates
    {

        private HTML_Object _Template_App_Menu_DeviceBtn(Arc_Device P_Arc_Device)
        {
            HTML_Object L_HTML_Object = _Template_Button("", P_Arc_Device.Name);
            
            L_HTML_Object.Add_Attribute("onclick", "_MenuBtn_Click('" + P_Arc_Device.Id + "', '" + Web_InterCommMessage_Type.SwitchPanel.ToString() + "');");

            return L_HTML_Object;
        }

        private HTML_Object _Template_Device(Arc_Device P_Arc_Device)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", _Template_DeviceID(P_Arc_Device));
            L_HTML_Object.Add_Attribute("class", "MF_Device");

            for (int i = 0; i < P_Arc_Device.AudioObjects.Count; i++)
            {
                L_HTML_Object.Add_Child(_Template_VolumeControl(P_Arc_Device.AudioObjects[i]));
            }

            return L_HTML_Object;
        }

        private string _Template_DeviceID(Arc_Device P_Arc_Device)
        {
            return "Device_" + _GetHtmlId(P_Arc_Device.Id);
        }

        

    }
}
