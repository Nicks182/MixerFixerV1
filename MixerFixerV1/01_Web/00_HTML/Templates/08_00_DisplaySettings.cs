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

        private HTML_Object _Template_App_Menu_DisplaySettingsBtn()
        {
            HTML_Object L_HTML_Object = _Template_Button("", null, HTML_Object_Icon.personal_video, HTML_Object_Icon_Pos.IsLeft);

            L_HTML_Object.Add_Attribute("class", "MF_MixerAppMenu_Display");
            L_HTML_Object.Add_Attribute("onclick", "_App_DisplaySettingsShow('" + Web_InterCommMessage_Type.DisplaySettings_Show.ToString() + "');");

            return L_HTML_Object;
        }

        public string _Template_DisplaySettingsModal_Id()
        {
            return "Modal_DisplaySettings";
        }

        public StringBuilder _Template_DisplaySettingsModal_HTML()
        {
            return new HTML()._BuildHtml(_Template_DisplaySettingsModal());
        }

        private HTML_Object _Template_DisplaySettingsModal()
        {
            HTML_Object L_HTML_Object = _Template_Modal(_Template_DisplaySettingsModal_Id(), "Display Settings", _Template_DisplaySettingsModal_Body());


            return L_HTML_Object;
        }

        private HTML_Object _Template_DisplaySettingsModal_Body()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "DisplaySettings");
            L_HTML_Object.Add_Attribute("class", "MF_DisplaySettings");

            L_HTML_Object.Add_Child(_Template_DisplaySettingsModal_Body_Panel());
            L_HTML_Object.Add_Child(_Template_DisplaySettingsModal_Body_Footer());


            return L_HTML_Object;
        }


        private HTML_Object _Template_DisplaySettingsModal_Body_Panel()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "DisplaySettingsPanel");
            L_HTML_Object.Add_Attribute("class", "MF_DisplaySettingsPanel");

            L_HTML_Object.Add_Child(_Template_DisplaySettingsModal_Body_Panel_Help());

            L_HTML_Object.Add_Child(_Template_DisplaySettingsModal_Body_Panel_Monitors());

            return L_HTML_Object;
        }

        private HTML_Object _Template_DisplaySettingsModal_Body_Panel_Monitors()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "DisplaySettingsPanel_Monitors");
            L_HTML_Object.Add_Attribute("class", "MF_DisplaySettingsPanel_Monitors");

            L_HTML_Object.Add_Child(_Template_DisplaySettingsModal_Body_Panel_Heading());
            L_HTML_Object.Add_Child(_Template_DisplaySettingsModal_Body_Panel_Items());

            return L_HTML_Object;
        }

        private HTML_Object _Template_DisplaySettingsModal_Body_Panel_Help()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_DisplaySettings_Help");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = _Template_DisplaySettingsModal_Body_Panel_Help_Text()
            });

            return L_HTML_Object;
        }

        private StringBuilder _Template_DisplaySettingsModal_Body_Panel_Help_Text()
        {
            StringBuilder L_Text = new StringBuilder();
            L_Text.Append("When monitor is set to be Managed, MixerFixer will store the monitor settings as they currently are and try to restore those settings when monitor is active in Windows.");
            L_Text.Append("<br />");
            L_Text.Append("<br />");
            L_Text.Append("1. Use Windows Display Settings to configure your monitor settings.");
            L_Text.Append("<br />");
            L_Text.Append("2. Mark which monitor's settings should MixerFixer store and try to restore by enabling the Is Managed switch.");
            L_Text.Append("<br />");

            return L_Text;
        }

        private HTML_Object _Template_DisplaySettingsModal_Body_Panel_Heading()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "DisplaySettingsPanel_Heading");
            L_HTML_Object.Add_Attribute("class", "MF_DisplaySettingsPanelHeading");

            L_HTML_Object.Children.Add(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder("Monitors:")
            });

            return L_HTML_Object;
        }

        private HTML_Object _Template_DisplaySettingsModal_Body_Panel_Items()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "DisplaySettingsPanel_Items");
            L_HTML_Object.Add_Attribute("class", "MF_DisplaySettingsPanel_Items");

            L_HTML_Object.Children = _Template_DisplaySettingsModal_Body_Panel_Children();

            return L_HTML_Object;
        }

        public StringBuilder _Template_DisplaySettingsModal_Body_Panel_HTML()
        {
            return G_HTML._BuildHtml(_Template_DisplaySettingsModal_Body_Panel_Children());
        }

        private List<HTML_Object> _Template_DisplaySettingsModal_Body_Panel_Children()
        {
            List<HTML_Object> L_Items = new List<HTML_Object>();

            List<DB_DisplaySettings> L_DisplaySettings = G_Srv_DB.DisplaySettings_GetAll().FindAll().ToList();

            for (int i = 0; i < L_DisplaySettings.Count; i++)
            {
                L_Items.Add(_Template_DisplaySettingsModal_Body_Panel_DisplayItem(L_DisplaySettings[i]));
            }

            return L_Items;
        }



    }
}
