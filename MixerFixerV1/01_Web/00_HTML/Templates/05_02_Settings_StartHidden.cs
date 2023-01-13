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

        
        private HTML_Object _Template_SettingsModal_Body_StartHidden()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_StartHidden");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_StartHidden_Text());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_StartHidden_Toggle());


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_StartHidden_Text()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder("Start with main window hidden?")
            });


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_StartHidden_Toggle()
        {
            HTML_Object L_HTML_Object = _Template_Toggle(
                                            P_Id: _Template_SettingsModal_Body_StartHidden_Id(),
                                            P_Title: "If checked, the main window will not show when app starts up. Only icon on taskbar will show.",
                                            P_IsChecked: _Template_SettingsModal_Body_StartHidden_IsChecked()
                                        );


            //L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_Starthidden");
            //L_HTML_Object.Add_Attribute("IsRotate", "270");
            L_HTML_Object.Add_Attribute("onclick", "_App_Settings_StartHidden_Change('" + Web_InterCommMessage_Type.Settings_StartHidden_Change.ToString() + "');");




            return L_HTML_Object;
        }

        public string _Template_SettingsModal_Body_StartHidden_Id()
        {
            return "Settings_StartHidden";
        }

        private bool _Template_SettingsModal_Body_StartHidden_IsChecked()
        {
            DB_Settings L_DB_Settings = G_Srv_DB.Settings_GetOne(G_Srv_DB.G_StartHidden);
            if(L_DB_Settings != null && string.IsNullOrEmpty(L_DB_Settings.Value) == false)
            {
                return Convert.ToBoolean(Convert.ToInt32(L_DB_Settings.Value));
            }
            return false;
        }



        

    }
}
