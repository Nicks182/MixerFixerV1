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

        
        private HTML_Object _Template_SettingsModal_Body_StartWithWindows()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_StartWithWindows");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_StartWithWindows_Text());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_StartWithWindows_Toggle());


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_StartWithWindows_Text()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder("Start with Windows?")
            });


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_StartWithWindows_Toggle()
        {
            HTML_Object L_HTML_Object = _Template_Toggle(
                                            P_Id: _Template_SettingsModal_Body_StartWithWindows_Id(),
                                            P_Title: "If checked, MixerFixer will start automatically after you log into Windows.",
                                            P_IsChecked: _Template_SettingsModal_Body_SStartWithWindows_IsChecked()
                                        );


            //L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_Starthidden");
            //L_HTML_Object.Add_Attribute("IsRotate", "270");
            L_HTML_Object.Add_Attribute("onclick", "_App_Settings_StartWithWindows_Change('" + Web_InterCommMessage_Type.Settings_StartWithWindows_Change.ToString() + "');");




            return L_HTML_Object;
        }

        public string _Template_SettingsModal_Body_StartWithWindows_Id()
        {
            return "Settings_StartWithWindows";
        }

        private bool _Template_SettingsModal_Body_SStartWithWindows_IsChecked()
        {
            DB_Settings L_DB_Settings = G_Srv_DB.Settings_GetOne(G_Srv_DB.G_StartWithWindows);
            if(L_DB_Settings != null && string.IsNullOrEmpty(L_DB_Settings.Value) == false)
            {
                return Convert.ToBoolean(Convert.ToInt32(L_DB_Settings.Value));
            }
            return false;
        }



        

    }
}
