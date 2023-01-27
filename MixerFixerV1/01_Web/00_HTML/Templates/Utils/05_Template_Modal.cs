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

        private HTML_Object _Template_Modal(string P_Id, string P_Title, HTML_Object P_Body)
        {
            return _Template_Modal(P_Id, P_Title, P_Body, false);
        }

        private HTML_Object _Template_Modal(string P_Id, string P_Title, HTML_Object P_Body, bool P_IsPopup)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", P_Id);
            L_HTML_Object.Add_Attribute("class", "MF_Modal");
            L_HTML_Object.Add_Attribute("NoMask", Convert.ToInt32(P_IsPopup).ToString());

            L_HTML_Object.Add_Child(_Template_Modal_Mask());
            L_HTML_Object.Add_Child(_Template_Modal_Content(P_Id, P_Title, P_Body));

            return L_HTML_Object;
        }

        private HTML_Object _Template_Modal_Mask()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_Modal_Mask");

            return L_HTML_Object;
        }

        private HTML_Object _Template_Modal_Content(string P_Id, string P_Title, HTML_Object P_Body)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_Modal_Content");
            
            L_HTML_Object.Add_Child(_Template_Modal_Content_Header(P_Id, P_Title));
            L_HTML_Object.Add_Child(_Template_Modal_Content_Body(P_Body));
            //L_HTML_Object.Add_Child(_Template_Modal_Content_Footer());

            return L_HTML_Object;
        }

        private HTML_Object _Template_Modal_Content_Header(string P_Id, string P_Title)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_Modal_Content_Header");
            
            L_HTML_Object.Add_Child(_Template_Modal_Content_Header_Title(P_Title));
            L_HTML_Object.Add_Child(_Template_Modal_Content_Header_Close(P_Id));

            return L_HTML_Object;
        }

        private HTML_Object _Template_Modal_Content_Header_Title(string P_Title)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsLabel;

            
            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder(P_Title)
            });

            return L_HTML_Object;
        }

        private HTML_Object _Template_Modal_Content_Header_Close(string P_Id)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_Modal_Content_Header_Close");
            L_HTML_Object.Add_Attribute("onclick", _Template_Modal_CloseEvent(P_Id));

            L_HTML_Object.Add_Child(_Template_Icon(HTML_Object_Icon.close));

            return L_HTML_Object;
        }

        private HTML_Object _Template_Modal_Content_Body(HTML_Object P_Body)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_Modal_Content_Body");

            L_HTML_Object.Add_Child(P_Body);

            return L_HTML_Object;
        }

        private HTML_Object _Template_Modal_Content_Footer()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_Modal_Content_Footer");

            L_HTML_Object.Add_Child(_Template_Modal_Content_Footer_Notification());

            return L_HTML_Object;
        }

        private HTML_Object _Template_Modal_Content_Footer_Notification()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "MF_Modal_Content_Footer_Notification");

            L_HTML_Object.Add_Child(_Template_Modal_Content_Footer_Notification_Text());

            return L_HTML_Object;
        }

        private HTML_Object _Template_Modal_Content_Footer_Notification_Text()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsLabel;

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder("Saved!")
            });

            return L_HTML_Object;
        }


        private string _Template_Modal_CloseEvent(string P_Id)
        {
            return "_Modal_Close('" + P_Id + "', '" + Web_InterCommMessage_Type.Modal_Close + "');";
        }
    }
}
