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

        private HTML_Object _Template_App_Menu_SettingsBtn()
        {
            HTML_Object L_HTML_Object = _Template_Button("", null, HTML_Object_Icon.settings, HTML_Object_Icon_Pos.IsLeft);

            L_HTML_Object.Add_Attribute("class", "MF_MixerAppMenu_Settings");
            L_HTML_Object.Add_Attribute("onclick", "_App_SettingsShow('" + Web_InterCommMessage_Type.Settings_Show.ToString() + "');");

            return L_HTML_Object;
        }

        public string _Template_SettingsModal_Id()
        {
            return "Modal_Settings";
        }

        public StringBuilder _Template_SettingsModal_HTML()
        {
            return new HTML()._BuildHtml(_Template_SettingsModal());
        }

        private HTML_Object _Template_SettingsModal()
        {
            HTML_Object L_HTML_Object = _Template_Modal(_Template_SettingsModal_Id(), "Settings", _Template_SettingsModal_Body());


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "Settings");
            L_HTML_Object.Add_Attribute("class", "MF_Settings");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_Panel());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_Footer());


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_Panel()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "SettingsPanel");
            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_UseDefaultVolume());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DefaultVolume());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriorityHelp());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_SoundPriority());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_MicPriority());


            return L_HTML_Object;
        }

        

    }
}
