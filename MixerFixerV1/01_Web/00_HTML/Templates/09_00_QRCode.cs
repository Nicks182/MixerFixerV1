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
using QRCoder;
using Services;

namespace Web
{
    public partial class HTML_Templates
    {

        private HTML_Object _Template_App_Menu_QRCodeBtn()
        {
            HTML_Object L_HTML_Object = _Template_Button("", null, HTML_Object_Icon.border_clear, HTML_Object_Icon_Pos.IsLeft);

            L_HTML_Object.Add_Attribute("class", "MF_MixerAppMenu_QRCode");
            L_HTML_Object.Add_Attribute("onclick", "_App_QRCodeShow('" + Web_InterCommMessage_Type.QRCode_Show.ToString() + "');");

            return L_HTML_Object;
        }

        public string _Template_QRCodeModal_Id()
        {
            return "Modal_QRCode";
        }

        public StringBuilder _Template_QRCodeModal_HTML()
        {
            return new HTML()._BuildHtml(_Template_QRCodeModal());
        }

        private HTML_Object _Template_QRCodeModal()
        {
            HTML_Object L_HTML_Object = _Template_Modal(_Template_QRCodeModal_Id(), "Remote Access", _Template_QRCodeModal_Body());


            return L_HTML_Object;
        }

        private HTML_Object _Template_QRCodeModal_Body()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "QRCode");
            L_HTML_Object.Add_Attribute("class", "MF_QRCode");

            L_HTML_Object.Add_Child(_Template_QRCodeModal_Body_Panel());
            L_HTML_Object.Add_Child(_Template_QRCodeModal_Body_Footer());


            return L_HTML_Object;
        }


        private HTML_Object _Template_QRCodeModal_Body_Panel()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "QRCodePanel");
            L_HTML_Object.Add_Attribute("class", "MF_QRCodePanel");

            
            L_HTML_Object.Add_Child(_Template_QRCodeModal_Body_Panel_QRCode());

            return L_HTML_Object;
        }

        private HTML_Object _Template_QRCodeModal_Body_Panel_QRCode()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsImg;

            L_HTML_Object.Add_Attribute("src", _Template_QRCodeModal_Body_Panel_QRCode_Base64_Img());

            return L_HTML_Object;
        }

        private string _Template_QRCodeModal_Body_Panel_QRCode_Base64_Img()
        {
            string L_IP = App._GetLocalIPAddress();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(L_IP, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            string L_Base64String = "";

            using (var ms = new MemoryStream())
            {
                using (var bitmap = qrCode.GetGraphic(9, System.Drawing.Color.White, System.Drawing.Color.Black, true))
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    L_Base64String = "data:image/png;base64, " + Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                }
            }

            return L_Base64String;
        }

        //public string _GetLocalIPAddress()
        //{
        //    var host = Dns.GetHostEntry(Dns.GetHostName());
        //    foreach (var ip in host.AddressList)
        //    {
        //        if (ip.AddressFamily == AddressFamily.InterNetwork)
        //        {
        //            return "http://" + ip.ToString() + ":" + App.G_Port.ToString();
        //        }
        //    }

        //    return "NA";
        //}

    }
}
