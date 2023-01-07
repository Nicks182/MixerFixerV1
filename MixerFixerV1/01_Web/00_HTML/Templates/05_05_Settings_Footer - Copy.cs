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

        
        

        #region SettingsFooter
        private HTML_Object _Template_SettingsModal_Body_Footer()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "SettingsFooter");
            L_HTML_Object.Add_Attribute("class", "MF_Settings_Footer");

            L_HTML_Object.Add_Child(_Template_Modal_Content_Footer_Notification());

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_Footer_SaveBtn());


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_Footer_SaveBtn()
        {
            HTML_Object L_HTML_Object = _Template_Button("Btn_SettingsClose", "Close");


            L_HTML_Object.Add_Attribute("onclick", _Template_Modal_CloseEvent(_Template_SettingsModal_Id()));

            return L_HTML_Object;
        }

        #endregion SettingsFooter

    }
}
