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
        

        private HTML_Object _Template_VolumeControl(Arc_AudioObject P_AudioCore_Object)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", _Template_VolumeControl_Id(P_AudioCore_Object));
            L_HTML_Object.Add_Attribute("class", "MF_AppControl");


            L_HTML_Object.Add_Child(_Template_VolumeControl_Image(P_AudioCore_Object));
            L_HTML_Object.Add_Child(_Template_VolumeControl_Label(P_AudioCore_Object));
            L_HTML_Object.Add_Child(_Template_VolumeControl_IsMute(P_AudioCore_Object));
            L_HTML_Object.Add_Child(_Template_VolumeControl_IsMananged(P_AudioCore_Object));
            L_HTML_Object.Add_Child(_Template_VolumeControl_VolumeText(P_AudioCore_Object));
            L_HTML_Object.Add_Child(_Template_VolumeControl_Volume(P_AudioCore_Object));

            return L_HTML_Object;
        }

        private string _Template_VolumeControl_Id(Arc_AudioObject P_AudioCore_Object)
        {
            return "ObjectControl_" + _GetHtmlId(P_AudioCore_Object.UniqueId);
        }

        private HTML_Object _Template_VolumeControl_Image(Arc_AudioObject P_AudioCore_Object)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsImg;

            L_HTML_Object.Add_Attribute("class", "MF_AppControl_Image");
            L_HTML_Object.Add_Attribute("src", _Img_GetSrc(P_AudioCore_Object.Image));

            return L_HTML_Object;
        }

        private HTML_Object _Template_VolumeControl_Label(Arc_AudioObject P_AudioCore_Object)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsLabel;

            L_HTML_Object.Add_Attribute("class", "MF_AppControl_Label");

            // Add actual text as Child object with raw value
            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder(P_AudioCore_Object.Name)
            });

            return L_HTML_Object;
        }

        private HTML_Object _Template_VolumeControl_IsMute(Arc_AudioObject P_AudioCore_Object)
        {
            HTML_Object L_HTML_Object = _Template_Toggle(
                                            P_Id:           "IsMute_" + _GetHtmlId(P_AudioCore_Object.UniqueId),
                                            P_Title:        "Set Mute",
                                            P_IsChecked:    P_AudioCore_Object._Get_Mute()
                                        );

            L_HTML_Object.Add_Attribute("class", "MF_AppControl_IsMute");

            return L_HTML_Object;
        }

        private HTML_Object _Template_VolumeControl_IsMananged(Arc_AudioObject P_AudioCore_Object)
        {
            HTML_Object L_HTML_Object = _Template_Toggle(
                                            P_Id:           "IsManaged_" + _GetHtmlId(P_AudioCore_Object.UniqueId),
                                            P_Title:        "Is Managed",
                                            P_IsChecked:    P_AudioCore_Object.IsManaged
                                        );

            L_HTML_Object.Add_Attribute("class", "MF_AppControl_IsManaged");

            return L_HTML_Object;
        }


        #region VolumeText

        private HTML_Object _Template_VolumeControl_VolumeText(Arc_AudioObject P_AudioCore_Object)
        {
            HTML_Object L_HTML_Object = _Template_Button(_Template_VolumeControl_VolumeText_Id(P_AudioCore_Object), P_AudioCore_Object._Get_Volume().ToString());

            L_HTML_Object.Add_Attribute("class", "MF_AppControl_Text");


            return L_HTML_Object;
        }

        private string _Template_VolumeControl_VolumeText_Id(Arc_AudioObject P_AudioCore_Object)
        {
            return "VolumeText_" + _GetHtmlId(P_AudioCore_Object.UniqueId);
        }

        public string _Template_VolumeControl_VolumeText_Id_Data(Arc_AudioObject P_AudioCore_Object)
        {
            return "VolumeText_" + _GetHtmlId(P_AudioCore_Object.UniqueId) + "_Text";
        }

        #endregion VolumeText


        private HTML_Object _Template_VolumeControl_Volume(Arc_AudioObject P_AudioCore_Object)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "Volume_" + _GetHtmlId(P_AudioCore_Object.UniqueId));
            L_HTML_Object.Add_Attribute("class", "MF_AppControl_Volume");

            L_HTML_Object.Add_Child(_Template_VolumeControl_VolumeMeter(P_AudioCore_Object));
            L_HTML_Object.Add_Child(_Template_VolumeControl_VolumeSlider(P_AudioCore_Object));
            

            return L_HTML_Object;
        }


        #region VolumeSlider
        private HTML_Object _Template_VolumeControl_VolumeSlider(Arc_AudioObject P_AudioCore_Object)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsSlider;

            L_HTML_Object.Add_Attribute("id", _Template_VolumeControl_VolumeSlider_Id(P_AudioCore_Object));
            L_HTML_Object.Add_Attribute("class", "slider MF_AppControl_Slider");
            L_HTML_Object.Add_Attribute("min", "0");
            L_HTML_Object.Add_Attribute("max", "100");
            L_HTML_Object.Add_Attribute("value", "60");
            L_HTML_Object.Add_Attribute("oninput", "_App_VolumeChange('" + P_AudioCore_Object.UniqueId.ToString() + "', this.value);");


            return L_HTML_Object;
        }

        public string _Template_VolumeControl_VolumeSlider_Id(Arc_AudioObject P_AudioCore_Object)
        {
            return "VolumeSlider_" + _GetHtmlId(P_AudioCore_Object.UniqueId);
        }

        #endregion VolumeSlider


        #region VolumeMeter
        private HTML_Object _Template_VolumeControl_VolumeMeter(Arc_AudioObject P_AudioCore_Object)
        {
            HTML_Object L_HTML_Object = _Template_Progress(_Template_VolumeControl_VolumeMeter_Id(P_AudioCore_Object));
            L_HTML_Object.Add_Attribute("class", "MF_AppControl_VolumeMeter");

            return L_HTML_Object;
        }

        private string _Template_VolumeControl_VolumeMeter_Id(Arc_AudioObject P_AudioCore_Object)
        {
            return "VolumeMeter_" + _GetHtmlId(P_AudioCore_Object.UniqueId);
        }

        public string _Template_VolumeControl_VolumeMeter_Data_Id(Arc_AudioObject P_AudioCore_Object)
        {
            return _Template_VolumeControl_VolumeMeter_Id(P_AudioCore_Object) + "_Fill";
        }

        #endregion VolumeMeter

    }
}
