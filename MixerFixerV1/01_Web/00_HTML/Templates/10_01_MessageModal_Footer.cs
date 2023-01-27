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
        private HTML_Object _Template_MessageModal_Body_Footer()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "MessageModalFooter");
            L_HTML_Object.Add_Attribute("class", "MF_MessageModal_Footer");

            L_HTML_Object.Add_Child(_Template_MessageModal_Body_Footer_CloseBtn());


            return L_HTML_Object;
        }

        private HTML_Object _Template_MessageModal_Body_Footer_CloseBtn()
        {
            HTML_Object L_HTML_Object = _Template_Button("Btn_MessageModalClose", "OK");


            L_HTML_Object.Add_Attribute("onclick", _Template_Modal_CloseEvent(_Template_MessageModal_Id()));

            return L_HTML_Object;
        }

    }
}
