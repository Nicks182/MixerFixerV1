using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlGenerator
{
    public partial class HTML
    {
        private void _GetTag_IsImg(HTML_Object P_HTML_Object, bool P_IsClosing)
        {
            if (P_IsClosing == false)
            {
                G_HtmlString.Append("<img");
                _BuildAttributes(P_HTML_Object);
                
            }
            else
            {
                G_HtmlString.Append("/>");
            }
        }


    }

}
