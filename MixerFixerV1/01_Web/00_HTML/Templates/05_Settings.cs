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

            L_HTML_Object.Add_Attribute("id", "Settings");
            L_HTML_Object.Add_Attribute("class", "MF_Settings");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_Panel());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_Footer());


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_Panel()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "SettingsPanel");
            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_UseDefaultVolume());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DefaultVolume());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriorityHelp());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_SoundPriority());
            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_MicPriority());


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

        #region SoundPriority
        private HTML_Object _Template_SettingsModal_Body_SoundPriority()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "Div_Settings_SoundPriority");
            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_SoundPriority");

            L_HTML_Object.Children.AddRange(_Template_SettingsModal_Body_SoundPriority_Body());

            //L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Heading("Playback Priority:"));

            //List<DB_DevicePriority> L_Devices = G_Srv_DB.DevicePriority_GetAll().Find(ao => ao.IsMic == false).OrderBy(ao => ao.Priority).ToList();
            //L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Items(L_Devices));

            return L_HTML_Object;
        }

        private List<HTML_Object> _Template_SettingsModal_Body_SoundPriority_Body()
        {
            List<DB_DevicePriority> L_Devices = G_Srv_DB.DevicePriority_GetAll().Find(ao => ao.IsMic == false).OrderBy(ao => ao.Priority).ToList();

            return new List<HTML_Object>
            {
                _Template_SettingsModal_Body_DevicePriority_Heading("Playback Priority:"),
                _Template_SettingsModal_Body_DevicePriority_Items(L_Devices)
            };
        }

        public StringBuilder _Template_SettingsModal_Body_SoundPriority_HTML()
        {
            return G_HTML._BuildHtml(_Template_SettingsModal_Body_SoundPriority_Body());
        }

        #endregion SoundPriority

        #region MicPriority
        private HTML_Object _Template_SettingsModal_Body_MicPriority()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "Div_Settings_MicPriority");
            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_MicPriority");

            L_HTML_Object.Children.AddRange(_Template_SettingsModal_Body_MicPriority_Body());

            //L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Heading("Capture Priority:"));

            //List<DB_DevicePriority> L_Devices = G_Srv_DB.DevicePriority_GetAll().Find(ao => ao.IsMic == true).OrderBy(ao => ao.Priority).ToList();
            //L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Items(L_Devices));

            return L_HTML_Object;
        }

        private List<HTML_Object> _Template_SettingsModal_Body_MicPriority_Body()
        {
            List<DB_DevicePriority> L_Devices = G_Srv_DB.DevicePriority_GetAll().Find(ao => ao.IsMic == true).OrderBy(ao => ao.Priority).ToList();

            return new List<HTML_Object>
            {
                _Template_SettingsModal_Body_DevicePriority_Heading("Capture Priority:"),
                _Template_SettingsModal_Body_DevicePriority_Items(L_Devices)
            };
        }

        public StringBuilder _Template_SettingsModal_Body_MicPriority_HTML()
        {
            return G_HTML._BuildHtml(_Template_SettingsModal_Body_MicPriority_Body());
        }

        #endregion MicPriority

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



        #region SettingsFooter
        private HTML_Object _Template_SettingsModal_Body_Footer()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "SettingsFooter");
            L_HTML_Object.Add_Attribute("class", "MF_Settings_Footer");

            L_HTML_Object.Add_Child(_Template_SettingsModal_Body_Footer_SaveBtn());


            return L_HTML_Object;
        }

        private HTML_Object _Template_SettingsModal_Body_Footer_SaveBtn()
        {
            HTML_Object L_HTML_Object = _Template_Button("Btn_SettingsSave", "Save");


            return L_HTML_Object;
        }

        #endregion SettingsFooter

    }
}
