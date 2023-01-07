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

        private HTML_Object _Template_ThemeModal_Body_Panel_Color(DB_Theme P_ThemeColor)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "ThemePanel_Color_" + P_ThemeColor.Name);
            L_HTML_Object.Add_Attribute("class", "MF_ThemePanel_Color");

            L_HTML_Object.Add_Child(_Template_ThemeModal_Body_Panel_Color_Name(P_ThemeColor));
            L_HTML_Object.Add_Child(_Template_ThemeModal_Body_Panel_Color_Description(P_ThemeColor));
            L_HTML_Object.Add_Child(_Template_ThemeModal_Body_Panel_Color_Sliders(P_ThemeColor));

            return L_HTML_Object;
        }

        private HTML_Object _Template_ThemeModal_Body_Panel_Color_Name(DB_Theme P_ThemeColor)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsLabel;

            L_HTML_Object.Add_Attribute("class", "MF_ThemePanel_Color_Name");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder(P_ThemeColor.Name)
            });

            return L_HTML_Object;
        }

        private HTML_Object _Template_ThemeModal_Body_Panel_Color_Description(DB_Theme P_ThemeColor)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsLabel;

            L_HTML_Object.Add_Attribute("class", "MF_ThemePanel_Color_Description");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder(P_ThemeColor.Description)
            });

            return L_HTML_Object;
        }

        private HTML_Object _Template_ThemeModal_Body_Panel_Color_Sliders(DB_Theme P_ThemeColor)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "ThemePanel_Color_Sliders");
            L_HTML_Object.Add_Attribute("class", "MF_ThemePanel_Color_Sliders");

            L_HTML_Object.Add_Child(_Template_ThemeModal_Body_Panel_Color_Slider_Red(P_ThemeColor));
            L_HTML_Object.Add_Child(_Template_ThemeModal_Body_Panel_Color_Slider_Green(P_ThemeColor));
            L_HTML_Object.Add_Child(_Template_ThemeModal_Body_Panel_Color_Slider_Blue(P_ThemeColor));


            return L_HTML_Object;
        }


        private HTML_Object _Template_ThemeModal_Body_Panel_Color_Slider_Red(DB_Theme P_ThemeColor)
        {
            HTML_Object L_HTML_Object = _Template_ThemeModal_Body_Panel_Color_Slider_Template(P_ThemeColor.Id, P_ThemeColor.Red, Web_InterCommMessage_Type.Theme_Color_Change_Red);

            L_HTML_Object.Add_Attribute("class", "MF_ThemePanel_Color_Slider_Red");

            return L_HTML_Object;
        }

        private HTML_Object _Template_ThemeModal_Body_Panel_Color_Slider_Green(DB_Theme P_ThemeColor)
        {
            HTML_Object L_HTML_Object = _Template_ThemeModal_Body_Panel_Color_Slider_Template(P_ThemeColor.Id, P_ThemeColor.Green, Web_InterCommMessage_Type.Theme_Color_Change_Green);

            L_HTML_Object.Add_Attribute("class", "MF_ThemePanel_Color_Slider_Green");

            return L_HTML_Object;
        }

        private HTML_Object _Template_ThemeModal_Body_Panel_Color_Slider_Blue(DB_Theme P_ThemeColor)
        {
            HTML_Object L_HTML_Object = _Template_ThemeModal_Body_Panel_Color_Slider_Template(P_ThemeColor.Id, P_ThemeColor.Blue, Web_InterCommMessage_Type.Theme_Color_Change_Blue);

            L_HTML_Object.Add_Attribute("class", "MF_ThemePanel_Color_Slider_Blue");

            return L_HTML_Object;
        }

        private HTML_Object _Template_ThemeModal_Body_Panel_Color_Slider_Template(Guid P_Id, int P_Value, Web_InterCommMessage_Type P_MessageType)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_ThemePanel_Color_Slider");

            L_HTML_Object.Add_Child(_Template_ThemeModal_Body_Panel_Color_Slider_Template_Name(P_MessageType));
            L_HTML_Object.Add_Child(_Template_ThemeModal_Body_Panel_Color_Slider_Template_Slider(P_Id, P_Value, P_MessageType));
            L_HTML_Object.Add_Child(_Template_ThemeModal_Body_Panel_Color_Slider_Template_Value(P_Id, P_Value, P_MessageType));

            return L_HTML_Object;
        }

        private HTML_Object _Template_ThemeModal_Body_Panel_Color_Slider_Template_Name(Web_InterCommMessage_Type P_MessageType)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsLabel;

            //L_HTML_Object.Add_Attribute("class", "MF_ThemePanel_Color_Slider");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder(_Template_ThemeModal_Body_Panel_Color_Slider_Template_Name_Text(P_MessageType))
            });

            return L_HTML_Object;
        }

        private HTML_Object _Template_ThemeModal_Body_Panel_Color_Slider_Template_Value(Guid P_Id, int P_Value, Web_InterCommMessage_Type P_MessageType)
        {
            HTML_Object L_HTML_Object = _Template_Button(_Template_ThemeModal_Body_Panel_Color_Slider_Template_Value_Id(P_Id, P_MessageType), P_Value.ToString());

            return L_HTML_Object;
        }

        public string _Template_ThemeModal_Body_Panel_Color_Slider_Template_Value_Id(Guid P_Id, Web_InterCommMessage_Type P_MessageType)
        {
            return "Slider_Value_" + P_Id.ToString().Replace("-", "_") + "_" + _Template_ThemeModal_Body_Panel_Color_Slider_Template_Name_Text(P_MessageType);
        }

        private string _Template_ThemeModal_Body_Panel_Color_Slider_Template_Name_Text(Web_InterCommMessage_Type P_MessageType)
        {
            switch(P_MessageType)
            {
                case Web_InterCommMessage_Type.Theme_Color_Change_Red:
                    return "R";

                case Web_InterCommMessage_Type.Theme_Color_Change_Green:
                    return "G";

                case Web_InterCommMessage_Type.Theme_Color_Change_Blue:
                    return "B";
            }

            return "";
        }

        private HTML_Object _Template_ThemeModal_Body_Panel_Color_Slider_Template_Slider(Guid P_Id, int P_Value, Web_InterCommMessage_Type P_MessageType)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsSlider;

            L_HTML_Object.Add_Attribute("id", _Template_ThemeModal_Body_Panel_Color_Slider_Id(P_Id));
            L_HTML_Object.Add_Attribute("class", "slider MF_ThemePanel_Color_Slider");
            L_HTML_Object.Add_Attribute("min", "0");
            L_HTML_Object.Add_Attribute("max", "255");
            L_HTML_Object.Add_Attribute("value", P_Value.ToString());
            L_HTML_Object.Add_Attribute("onwheel", "_App_Theme_OnChangeColor_MouseWheel(event, '" + P_Id.ToString() + "', '" + P_MessageType.ToString() + "', this.value);");
            L_HTML_Object.Add_Attribute("oninput", "_App_Theme_OnChangeColor('" + P_Id.ToString() + "', '" + P_MessageType.ToString() + "', this.value);");



            return L_HTML_Object;
        }

        public string _Template_ThemeModal_Body_Panel_Color_Slider_Id(Guid P_Id)
        {
            return "Slider_" + P_Id.ToString().Replace("-", "_");
        }

    }
}
