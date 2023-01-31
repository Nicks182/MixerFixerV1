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
        public StringBuilder _Template_SomeDiv_HTML()
        {
            return new HTML()._BuildHtml(_Template_SomeDiv("SomeId", "Some Button Text"));
        }

        private HTML_Object _Template_SomeDiv(string P_Id, string P_ButtonText)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", P_Id);
            L_HTML_Object.Add_Attribute("class", "SomeDivCSSClass");

            L_HTML_Object.Add_Child(_Template_SomeButton(P_ButtonText));


            return L_HTML_Object;
        }

        private HTML_Object _Template_SomeButton(string P_ButtonText)
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsButton;

            L_HTML_Object.Add_Attribute("onclick", "_SomeJavascriptClickHandler();");

            L_HTML_Object.Add_Child(new HTML_Object
            {
                Type = HTML_Object_Type.IsRaw,
                RawValue = new StringBuilder(P_ButtonText)
            });

            return L_HTML_Object;
        }

    }
}
