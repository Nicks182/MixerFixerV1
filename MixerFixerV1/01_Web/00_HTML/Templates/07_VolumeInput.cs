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

        public string _Template_Modal_VolumeInput_Id()
        {
            return "Modal_VolumeInput";
        }

        public StringBuilder _Template_Modal_VolumeInput_HTML(Web_InterCommMessage_Type P_CommType, string P_ObjectId)
        {
            return new HTML()._BuildHtml(_Template_Modal_VolumeInput(P_CommType, P_ObjectId));
        }

        private HTML_Object _Template_Modal_VolumeInput(Web_InterCommMessage_Type P_CommType, string P_ObjectId)
        {
            HTML_Object L_HTML_Object = _Template_Modal(_Template_Modal_VolumeInput_Id(), "Input Volume", _Template_Modal_VolumeInput_Body(P_CommType, P_ObjectId));


            return L_HTML_Object;
        }

        private HTML_Object _Template_Modal_VolumeInput_Body(Web_InterCommMessage_Type P_CommType, string P_ObjectId)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_VolumeInput");

            L_HTML_Object.Add_Child(_Template_Modal_VolumeInput_Body_Panel());
            L_HTML_Object.Add_Child(_Template_Modal_VolumeInput_Body_Footer(P_CommType, P_ObjectId));

            return L_HTML_Object;
        }

        private HTML_Object _Template_Modal_VolumeInput_Body_Panel()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_VolumeInput_Panel");

            L_HTML_Object.Add_Child(_Template_Modal_VolumeInput_Body_Input());

            return L_HTML_Object;
        }

        private HTML_Object _Template_Modal_VolumeInput_Body_Input()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsNumber;

            L_HTML_Object.Add_Attribute("id", _Template_Modal_VolumeInput_Body_Input_Id());
            L_HTML_Object.Add_Attribute("class", "MF_VolumeInput_Input");
            //L_HTML_Object.Add_Attribute("onkeydown", "_App_VolumeInput_OnKeyDown(event);");

            return L_HTML_Object;
        }


        public string _Template_Modal_VolumeInput_Body_Input_Id()
        {
            return "Txt_VolumeInput";
        }


        private HTML_Object _Template_Modal_VolumeInput_Body_Footer(Web_InterCommMessage_Type P_CommType, string P_ObjectId)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_VolumeInput_Footer");

            L_HTML_Object.Add_Child(_Template_Modal_Content_Footer_Notification());

            L_HTML_Object.Add_Child(_Template_Modal_VolumeInput_Body_Footer_SaveBtn(P_CommType, P_ObjectId));

            return L_HTML_Object;
        }

        private HTML_Object _Template_Modal_VolumeInput_Body_Footer_SaveBtn(Web_InterCommMessage_Type P_CommType, string P_ObjectId)
        {
            HTML_Object L_HTML_Object = _Template_Button("Btn_VolumeInputClose", "Set");

            L_HTML_Object.Add_Attribute("onclick", "_App_VolumeInput_Save('" + P_CommType.ToString() + "', '" + P_ObjectId + "', '" + _Template_Modal_VolumeInput_Body_Input_Id() + "');");

            return L_HTML_Object;
        }
    }
}
