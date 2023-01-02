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

       
        
        
        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Heading(string P_Text)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DevicePriorityHeading");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder(P_Text)
            });

            
            return L_HTML_Object;
        }


        #region DevicePriorityHelp
        private HTML_Object _Template_SettingsModal_Body_DevicePriorityHelp()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DevicePriorityHelp");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder("Device Priority: <br />MixerFixer will attempt to set the default device according to this priority if the device is active.")
            });

            return L_HTML_Object;
        }

        #endregion DevicePriorityHelp


        #region DevicePriorityItems
        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Items(List<DB_DevicePriority> P_Devices)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;


            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DevicePriorityItems");

            for(int i = 0; i < P_Devices.Count; i++)
            {
                L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Item(P_Devices[i]));
            }

            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Item(DB_DevicePriority P_DB_DevicePriority)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;


            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DevicePriority_Item");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Item_EnforceDefault(P_DB_DevicePriority));
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Item_Name(P_DB_DevicePriority));
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Item_MoveUp(P_DB_DevicePriority));
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Item_MoveDown(P_DB_DevicePriority));


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Item_EnforceDefault(DB_DevicePriority P_DB_DevicePriority)
        {
            HTML_Object L_HTML_Object = _Template_Toggle(
                                            P_Id: _Template_SettingsModal_Body_DevicePriority_Item_EnforceDefault_Id(P_DB_DevicePriority),
                                            P_Title: "Enforce Priority",
                                            P_IsChecked: P_DB_DevicePriority.EnforceDefault
                                        );

            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DevicePriority_Item_EnforceDefault");
            //L_HTML_Object.Add_Attribute("IsRotate", "270");
            L_HTML_Object.Add_Attribute("onclick", _Template_SettingsModal_Body_DevicePriority_Item_EnforceDefault_Event(P_DB_DevicePriority));


            return L_HTML_Object;
        }

        public string _Template_SettingsModal_Body_DevicePriority_Item_EnforceDefault_Id(DB_DevicePriority P_DB_DevicePriority)
        {
            return "IsMute_" + _GetHtmlId(P_DB_DevicePriority.Id);
        }

        public string _Template_SettingsModal_Body_DevicePriority_Item_EnforceDefault_Event(DB_DevicePriority P_DB_DevicePriority)
        {
            return "_App_MuteChange(event, '" + P_DB_DevicePriority.Id.ToString() + "');";
        }

        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Item_Name(DB_DevicePriority P_DB_DevicePriority)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DevicePriority_Item_Name");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Item_Name_Text(P_DB_DevicePriority));
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Item_Name_Device(P_DB_DevicePriority));


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Item_Name_Text(DB_DevicePriority P_DB_DevicePriority)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsLabel;

            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DevicePriority_Item_Name");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder((P_DB_DevicePriority.Priority + 1).ToString() + ": " + P_DB_DevicePriority.DisplayText)
            });


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Item_Name_Device(DB_DevicePriority P_DB_DevicePriority)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsLabel;

            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DevicePriority_Item_Name_Device");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder(P_DB_DevicePriority.DeviceName)
            });


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Item_MoveUp(DB_DevicePriority P_DB_DevicePriority)
        {
            HTML_Object L_HTML_Object = _Template_Button("Btn_DevicePriority_MoveUp", null, HTML_Object_Icon.arrow_drop_up, HTML_Object_Icon_Pos.IsLeft);

            //L_HTML_Object.Add_Attribute("class", "MF_MixerAppMenu_Settings");
            L_HTML_Object.Add_Attribute("onclick", "_App_Settings_PriorityItemMove('" + P_DB_DevicePriority.Id + "', '" + Web_InterCommMessage_Type.Settings_Priority_MoveUp.ToString() + "');");


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Item_MoveDown(DB_DevicePriority P_DB_DevicePriority)
        {
            HTML_Object L_HTML_Object = _Template_Button("Btn_DevicePriority_MoveDown", null, HTML_Object_Icon.arrow_drop_down, HTML_Object_Icon_Pos.IsLeft);

            //L_HTML_Object.Add_Attribute("class", "MF_MixerAppMenu_Settings");
            L_HTML_Object.Add_Attribute("onclick", "_App_Settings_PriorityItemMove('" + P_DB_DevicePriority.Id + "', '" + Web_InterCommMessage_Type.Settings_Priority_MoveDown.ToString() + "');");


            return L_HTML_Object;
        }


        #endregion DevicePriorityItems



        
    }
}
