using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public partial class HTML
    {
        private void _GetTag_IsRaw(HTML_Object P_HTML_Object, bool P_IsClosing)
        {
            if (P_IsClosing == false && P_HTML_Object.RawValue != null)
            {
                G_HtmlString.Append(P_HTML_Object.RawValue);
            }
        }


    }

}
