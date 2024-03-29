﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlGenerator;

namespace Web
{
    public partial class HTML_Templates
    {

        private HTML_Object _Template_Button(string P_Id, string P_Text)
        {
            return _Template_Button(P_Id, P_Text, HTML_Object_Icon.None, HTML_Object_Icon_Pos.None);
        }

        private HTML_Object _Template_Button(string P_Id, string P_Text, HTML_Object_Icon P_IconType)
        {
            return _Template_Button(P_Id, P_Text, P_IconType, HTML_Object_Icon_Pos.None);
        }

        private HTML_Object _Template_Button(string P_Id, string P_Text, HTML_Object_Icon P_IconType, HTML_Object_Icon_Pos P_IconPos)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsButton;

            L_HTML_Object.Add_Attribute("id", P_Id);
            L_HTML_Object.Add_Attribute("type", "button");
            L_HTML_Object.Add_Attribute("class", "button");

            if(P_IconType != HTML_Object_Icon.None && P_IconPos == HTML_Object_Icon_Pos.IsLeft)
            {
                L_HTML_Object.Add_Child(_Template_Icon(P_IconType));
            }

            if (P_Text != null)
            {
                L_HTML_Object.Add_Child(_Template_Button_Text(P_Id, P_Text));
            }

            if (P_IconType != HTML_Object_Icon.None && P_IconPos == HTML_Object_Icon_Pos.IsRight)
            {
                L_HTML_Object.Add_Child(_Template_Icon(P_IconType));
            }

            return L_HTML_Object;
        }

        private HTML_Object _Template_Button_Text(string P_Id, string P_Text)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsLabel;
            L_HTML_Object.Add_Attribute("id", P_Id + "_Text");

            L_HTML_Object.Add_Child(_Template_Button_Text_Value(P_Text));

            return L_HTML_Object;
        }

        private HTML_Object _Template_Button_Text_Value(string P_Text)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsRaw;
            L_HTML_Object.RawValue = new StringBuilder(P_Text);

            return L_HTML_Object;
        }


    }
}
