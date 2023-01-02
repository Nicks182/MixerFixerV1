using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public partial class HTML
    {
        private void _GetTag_IsInput(HTML_Object P_HTML_Object, bool P_IsClosing)
        {
            if (P_IsClosing == false)
            {
                G_HtmlString.Append("<input");
                _InputType(P_HTML_Object);
                _BuildAttributes(P_HTML_Object);
                
            }
            else
            {
                G_HtmlString.Append("/>");
            }
        }

        private void _InputType(HTML_Object P_HTML_Object)
        {
            G_HtmlString.Append(" type=\"");
            switch (P_HTML_Object.Type)
            {
                case HTML_Object_Type.IsText:
                    G_HtmlString.Append("text");
                    break;

                case HTML_Object_Type.IsSlider:
                    G_HtmlString.Append("range");
                    break;

                case HTML_Object_Type.IsNumber:
                    G_HtmlString.Append("number");
                    break;
            }
            G_HtmlString.Append("\"");
        }
    }

}
