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

        private HTML_Object _Template_DisplaySettingsModal_Body_Panel_DisplayItem(DB_DisplaySettings P_DB_DisplaySettings)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_DisplaySettings_Panel_Item");

            L_HTML_Object.Add_Child(_Template_DisplaySettingsModal_Body_Panel_DisplayItem_IsManaged(P_DB_DisplaySettings));
            L_HTML_Object.Add_Child(_Template_DisplaySettingsModal_Body_Panel_DisplayItem_Name(P_DB_DisplaySettings));
            L_HTML_Object.Add_Child(_Template_DisplaySettingsModal_Body_Panel_DisplayItem_IsAttached(P_DB_DisplaySettings));

            return L_HTML_Object;
        }

        // Is Managed toggle
        private HTML_Object _Template_DisplaySettingsModal_Body_Panel_DisplayItem_IsManaged(DB_DisplaySettings P_DB_DisplaySettings)
        {
            HTML_Object L_HTML_Object = _Template_Toggle(
                                            P_Id: _Template_DisplaySettingsModal_Body_Panel_DisplayItem_IsManaged_Id(P_DB_DisplaySettings.Id.ToString()),
                                            P_Title: "Is Managed?",
                                            P_IsChecked: P_DB_DisplaySettings.IsManaged
                                        );


            L_HTML_Object.Add_Attribute("onclick", "_App_DisplaySettings_ManagedChange('" + Web_InterCommMessage_Type.DisplaySettings_ManagedChange.ToString() + "', '" + P_DB_DisplaySettings.Id.ToString() + "', '" + P_DB_DisplaySettings.DevicePath_Base64 + "');");


            return L_HTML_Object;
        }

        public string _Template_DisplaySettingsModal_Body_Panel_DisplayItem_IsManaged_Id(string P_Id)
        {
            return "DisplaySettings_IsManaged_" + P_Id.Replace("-", "_");
        }

        // Display Name
        private HTML_Object _Template_DisplaySettingsModal_Body_Panel_DisplayItem_Name(DB_DisplaySettings P_DB_DisplaySettings)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsLabel;

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder(P_DB_DisplaySettings.FriendlyName)
            });

            return L_HTML_Object;
        }

        // Is Attached
        private HTML_Object _Template_DisplaySettingsModal_Body_Panel_DisplayItem_IsAttached(DB_DisplaySettings P_DB_DisplaySettings)
        {
            HTML_Object L_HTML_Object = _Template_Toggle(
                                            P_Id: _Template_DisplaySettingsModal_Body_Panel_DisplayItem_IsAttached_Id(P_DB_DisplaySettings.Id.ToString()),
                                            P_Title: "Change monitor power state",
                                            P_IsChecked: P_DB_DisplaySettings.IsPowered
                                        );

            L_HTML_Object.Add_Attribute("IsRotate", "270");
            L_HTML_Object.Add_Attribute("onclick", "_App_DisplaySettings_PowerChange('" + Web_InterCommMessage_Type.DisplaySettings_MonitorPower.ToString() + "', '" + P_DB_DisplaySettings.Id.ToString() + "', '" + P_DB_DisplaySettings.DevicePath_Base64 + "');");


            return L_HTML_Object;
        }

        public string _Template_DisplaySettingsModal_Body_Panel_DisplayItem_IsAttached_Id(string P_Id)
        {
            return "DisplaySettings_IsAttached_" + P_Id.Replace("-", "_");
        }


    }
}
