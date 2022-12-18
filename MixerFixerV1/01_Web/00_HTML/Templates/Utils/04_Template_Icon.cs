using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlGenerator;

namespace Web
{
    public partial class HTML_Templates
    {

        private HTML_Object _Template_Icon(string P_Id, HTML_Object_Icon P_HTML_Object_Icon)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsIcon;

            L_HTML_Object.Add_Attribute("id", P_Id);
            L_HTML_Object.Add_Attribute("class", "MF_Icon");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder(P_HTML_Object_Icon.ToString())
            });

            return L_HTML_Object;
        }

        

    }
}
