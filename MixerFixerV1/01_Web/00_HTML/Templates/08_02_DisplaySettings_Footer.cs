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
        private HTML_Object _Template_DisplaySettingsModal_Body_Footer()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "DisplaySettingsFooter");
            L_HTML_Object.Add_Attribute("class", "MF_DisplaySettings_Footer");

            L_HTML_Object.Add_Child(_Template_DisplaySettingsModal_Body_Footer_ReloadBtn());
            L_HTML_Object.Add_Child(_Template_DisplaySettingsModal_Body_Footer_CloseBtn());


            return L_HTML_Object;
        }

        private HTML_Object _Template_DisplaySettingsModal_Body_Footer_ReloadBtn()
        {
            HTML_Object L_HTML_Object = _Template_Button("Btn_DisplaySettingsReload", "Reload/Reset Monitor Info");


            L_HTML_Object.Add_Attribute("onclick", "_App_DisplaySettings_Reload('" + Web_InterCommMessage_Type.DisplaySettings_Reload.ToString() + "');");

            return L_HTML_Object;
        }

        private HTML_Object _Template_DisplaySettingsModal_Body_Footer_CloseBtn()
        {
            HTML_Object L_HTML_Object = _Template_Button("Btn_DisplaySettingsClose", "Close");


            L_HTML_Object.Add_Attribute("onclick", _Template_Modal_CloseEvent(_Template_DisplaySettingsModal_Id()));

            return L_HTML_Object;
        }

    }
}
