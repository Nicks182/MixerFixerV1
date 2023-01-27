using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using HtmlGenerator;
using MixerFixerV1;
using Services;

namespace Web
{
    public partial class HTML_Templates
    {

        //private HTML_Object _Template_App_Menu_MessageModalBtn()
        //{
        //    HTML_Object L_HTML_Object = _Template_Button("", null, HTML_Object_Icon.border_clear, HTML_Object_Icon_Pos.IsLeft);

        //    L_HTML_Object.Add_Attribute("class", "MF_MixerAppMenu_MessageModal");
        //    L_HTML_Object.Add_Attribute("onclick", "_App_MessageModalShow('" + Web_InterCommMessage_Type.MessageModal_Show.ToString() + "');");

        //    return L_HTML_Object;
        //}

        public string _Template_MessageModal_Id()
        {
            return "Modal_MessageModal";
        }

        public StringBuilder _Template_MessageModal_HTML(string P_Message)
        {
            return new HTML()._BuildHtml(_Template_MessageModal(P_Message));
        }

        private HTML_Object _Template_MessageModal(string P_Message)
        {
            HTML_Object L_HTML_Object = _Template_Modal(_Template_MessageModal_Id(), "Info...", _Template_MessageModal_Body(P_Message));


            return L_HTML_Object;
        }

        private HTML_Object _Template_MessageModal_Body(string P_Message)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "MessageModal");
            L_HTML_Object.Add_Attribute("class", "MF_MessageModal");

            L_HTML_Object.Add_Child(_Template_MessageModal_Body_Panel(P_Message));
            L_HTML_Object.Add_Child(_Template_MessageModal_Body_Footer());


            return L_HTML_Object;
        }


        private HTML_Object _Template_MessageModal_Body_Panel(string P_Message)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "MessageModalPanel");
            L_HTML_Object.Add_Attribute("class", "MF_MessageModalPanel");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder(P_Message)
            }) ;

            return L_HTML_Object;
        }



    }
}
