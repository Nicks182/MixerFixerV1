using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public partial class HTML
    {
        private void _BuildAttributes(HTML_Object P_HTML_Object)
        {
            // Example:
            // SomeAttributeName="SomeValue"
            for (int i = 0; i < P_HTML_Object.Attributes.Count; i++)
            {
                G_HtmlString.Append(' ');
                G_HtmlString.Append(P_HTML_Object.Attributes[i].Name);
                G_HtmlString.Append('=');
                G_HtmlString.Append('"');
                G_HtmlString.Append(P_HTML_Object.Attributes[i].Value);
                G_HtmlString.Append('"');
            }
        }

    }

}
