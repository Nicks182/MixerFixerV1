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
        private HTML_Object _Template_QRCodeModal_Body_Footer()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "QRCodeFooter");
            L_HTML_Object.Add_Attribute("class", "MF_QRCode_Footer");

            L_HTML_Object.Add_Child(_Template_QRCodeModal_Body_Footer_OpenInBrowserBTN());
            L_HTML_Object.Add_Child(_Template_QRCodeModal_Body_Footer_CloseBtn());


            return L_HTML_Object;
        }

        private HTML_Object _Template_QRCodeModal_Body_Footer_OpenInBrowserBTN()
        {
            HTML_Object L_HTML_Object = _Template_Button("Btn_OpenInBrowser", "Open in local Browser");


            L_HTML_Object.Add_Attribute("onclick", "_App_QRCode_OpenInLocalBrowser('" + Web_InterCommMessage_Type.QRCode_OpenInLocalBrowser.ToString() + "');");

            return L_HTML_Object;
        }

        private HTML_Object _Template_QRCodeModal_Body_Footer_CloseBtn()
        {
            HTML_Object L_HTML_Object = _Template_Button("Btn_QRCodeClose", "Close");


            L_HTML_Object.Add_Attribute("onclick", _Template_Modal_CloseEvent(_Template_QRCodeModal_Id()));

            return L_HTML_Object;
        }

    }
}
