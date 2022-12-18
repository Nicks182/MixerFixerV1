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

        private StringBuilder _Template_Progress_HTML(string P_Id)
        {
            return new HTML()._BuildHtml(_Template_Progress(P_Id));
        }

        private HTML_Object _Template_Progress(string P_Id)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", P_Id);
            L_HTML_Object.Add_Attribute("class", "progress");

            L_HTML_Object.Add_Child(_Template_Progress_Fill(P_Id));


            return L_HTML_Object;
        }

        private HTML_Object _Template_Progress_Fill(string P_Id)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", P_Id + "_Fill");

            return L_HTML_Object;
        }


    }
}
