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

        
        #region UseDefaultVolume
        private HTML_Object _Template_SettingsModal_Body_UseDefaultVolume()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_UseDefaultVolume");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_UseDefaultVolume_Text());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_UseDefaultVolume_Toggle());


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_UseDefaultVolume_Text()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder("Use Default Volume?")
            });


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_UseDefaultVolume_Toggle()
        {
            HTML_Object L_HTML_Object = _Template_Toggle(
                                            P_Id: _Template_SettingsModal_Body_UseDefaultVolume_Id(),
                                            P_Title: "Use Default Volume",
                                            P_IsChecked: _Template_SettingsModal_Body_UseDefaultVolume_IsChecked()
                                        );


            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_UseDefaultVolume");
            //L_HTML_Object.Add_Attribute("IsRotate", "270");
            L_HTML_Object.Add_Attribute("onclick", "_App_Settings_UseDefaultVolume_Change('" + Web_InterCommMessage_Type.Settings_UseDefault_Change.ToString() + "');");




            return L_HTML_Object;
        }

        public string _Template_SettingsModal_Body_UseDefaultVolume_Id()
        {
            return "Settings_UseDefaultVolume";
        }

        private bool _Template_SettingsModal_Body_UseDefaultVolume_IsChecked()
        {
            DB_Settings L_DB_Settings = G_Srv_DB.Settings_GetOne(G_Srv_DB.G_DefaultVolumeEnable);
            if(L_DB_Settings != null && string.IsNullOrEmpty(L_DB_Settings.Value) == false)
            {
                return Convert.ToBoolean(Convert.ToInt32(L_DB_Settings.Value));
            }
            return false;
        }

        #endregion UseDefaultVolume


        #region DefaultVolume
        private HTML_Object _Template_SettingsModal_Body_DefaultVolume()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DefaultVolume");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DefaultVolume_Text());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DefaultVolume_Btn());

            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_DefaultVolume_Text()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder("Default Volume Level:")
            });


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_DefaultVolume_Btn()
        {
            HTML_Object L_HTML_Object = _Template_Button(_Template_SettingsModal_Body_DefaultVolume_Id(), _Template_SettingsModal_Body_DefaultVolume_Value());


            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DefaultVolume");
            L_HTML_Object.Add_Attribute("onclick", "_App_Settings_VolumeInput_Show('" + Web_InterCommMessage_Type.Settings_DefaultVolume_Show.ToString() + "');");



            return L_HTML_Object;
        }

        public string _Template_SettingsModal_Body_DefaultVolume_Id()
        {
            return "Settings_DefaultVolume";
        }

        private string _Template_SettingsModal_Body_DefaultVolume_Value()
        {
            DB_Settings L_DB_Settings = G_Srv_DB.Settings_GetOne(G_Srv_DB.G_DefaultVolume);
            if (L_DB_Settings != null && string.IsNullOrEmpty(L_DB_Settings.Value) == false)
            {
                return L_DB_Settings.Value + "%";
            }
            return "NA";
        }

        #endregion DefaultVolume

        

    }
}
