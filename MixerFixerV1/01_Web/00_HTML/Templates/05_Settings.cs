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
            HTML_Object L_HTML_Object = _Template_Button("", "Settings");

            L_HTML_Object.Add_Attribute("class", "MF_MixerAppMenu_Settings");
            L_HTML_Object.Add_Attribute("onclick", "_MenuBtn_Click('settings', " + (int)Web_InterCommMessage_Type.SwitchPanel + ");");

            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsPanel()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "SettingsPanel");
            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder("Settings goes here!!!!!")
            });


            return L_HTML_Object;
        }

        


    }
}
