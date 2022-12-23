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
            L_HTML_Object.Add_Attribute("onclick", "_App_SettingsShow('" + Web_InterCommMessage_Type.ShowSettings.ToString() + "');");

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

            L_HTML_Object.Add_Attribute("id", "SettingsPanel");
            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_UseDefaultVolume());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DefaultVolume());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority());


            return L_HTML_Object;
        }

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
            L_HTML_Object.Add_Attribute("onclick", "_App_SettingsShowSaved();");




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
            L_HTML_Object.Add_Attribute("onclick", "_App_SettingsShowSaved();");



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

        #region DevicePriority
        private HTML_Object _Template_SettingsModal_Body_DevicePriority()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DevicePriority");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriorityHelp());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Items());

            return L_HTML_Object;
        }

        #endregion DevicePriority

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
        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Items()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;


            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DevicePriorityItems");
            L_HTML_Object.Add_Attribute("onclick", "_App_SettingsShowSaved();");

            List<DB_AudioObject> L_Devices = G_Srv_DB.AudioObject_GetAll().FindAll().Where(ao => ao.IsDevice == true).OrderBy(ao => ao.Priority).ToList();

            for(int i = 0; i < L_Devices.Count; i++)
            {
                L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Item(L_Devices[i]));
            }

            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Item(DB_AudioObject P_DB_AudioObject)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;


            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DevicePriority_Item");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Item_Priority(P_DB_AudioObject));
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Item_Name(P_DB_AudioObject));
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Item_MoveUp(P_DB_AudioObject));
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Item_MoveDown(P_DB_AudioObject));


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Item_Priority(DB_AudioObject P_DB_AudioObject)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsLabel;

            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DevicePriority_Item_Priority");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder((P_DB_AudioObject.Priority + 1).ToString() + ":")
            });


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Item_Name(DB_AudioObject P_DB_AudioObject)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsLabel;

            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_DevicePriority_Item_Name");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder(P_DB_AudioObject.Name)
            });


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Item_MoveUp(DB_AudioObject P_DB_AudioObject)
        {
            HTML_Object L_HTML_Object = _Template_Button("", null, HTML_Object_Icon.arrow_drop_up, HTML_Object_Icon_Pos.IsLeft);

            //L_HTML_Object.Add_Attribute("class", "MF_MixerAppMenu_Settings");
            L_HTML_Object.Add_Attribute("onclick", "_App_Settings_PriorityItemMove('" + P_DB_AudioObject.Id + "', '" + Web_InterCommMessage_Type.Settings_Priority_MoveUp.ToString() + "');");


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_DevicePriority_Item_MoveDown(DB_AudioObject P_DB_AudioObject)
        {
            HTML_Object L_HTML_Object = _Template_Button("", null, HTML_Object_Icon.arrow_drop_down, HTML_Object_Icon_Pos.IsLeft);

            //L_HTML_Object.Add_Attribute("class", "MF_MixerAppMenu_Settings");
            L_HTML_Object.Add_Attribute("onclick", "_App_Settings_PriorityItemMove('" + P_DB_AudioObject.Id + "', '" + Web_InterCommMessage_Type.Settings_Priority_MoveDown.ToString() + "');");


            return L_HTML_Object;
        }


        #endregion DevicePriorityItems

    }
}
