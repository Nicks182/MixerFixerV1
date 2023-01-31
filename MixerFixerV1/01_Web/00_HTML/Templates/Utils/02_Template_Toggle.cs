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

        private StringBuilder _Template_Toggle_HTML(string P_Id, bool P_IsChecked)
        {
            return new HTML()._BuildHtml(_Template_Toggle(P_Id, P_IsChecked));
        }

        private HTML_Object _Template_Toggle(string P_Id, bool P_IsChecked)
        {
            return _Template_Toggle(P_Id, P_IsChecked, "");
        }

        private HTML_Object _Template_Toggle(string P_Id, bool P_IsChecked, string P_Title)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;
            
            L_HTML_Object.Add_Attribute("id", P_Id);
            L_HTML_Object.Add_Attribute("title", P_Title);
            L_HTML_Object.Add_Attribute("class", "toggle");
            L_HTML_Object.Add_Attribute("IsChecked", Convert.ToInt32(P_IsChecked).ToString());

            L_HTML_Object.Add_Child(_Template_Toggle_Fill());

            return L_HTML_Object;
        }

        private HTML_Object _Template_Toggle_Fill()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("class", "toggle__fill");

            return L_HTML_Object;
        }


    }
}
