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

        private HTML_Object _Template_App_Menu_ThemeBtn()
        {
            HTML_Object L_HTML_Object = _Template_Button("", null, HTML_Object_Icon.color_lens, HTML_Object_Icon_Pos.IsLeft);

            L_HTML_Object.Add_Attribute("class", "MF_MixerAppMenu_Theme");
            L_HTML_Object.Add_Attribute("onclick", "_App_ThemeShow('" + Web_InterCommMessage_Type.Theme_Show.ToString() + "');");

            return L_HTML_Object;
        }

        public string _Template_ThemeModal_Id()
        {
            return "Modal_Theme";
        }

        public StringBuilder _Template_ThemeModal_HTML()
        {
            return new HTML()._BuildHtml(_Template_ThemeModal());
        }

        private HTML_Object _Template_ThemeModal()
        {
            HTML_Object L_HTML_Object = _Template_Modal(_Template_ThemeModal_Id(), "Theme", _Template_ThemeModal_Body());


            return L_HTML_Object;
        }

        private HTML_Object _Template_ThemeModal_Body()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "Theme");
            L_HTML_Object.Add_Attribute("class", "MF_Theme");

            L_HTML_Object.Add_Child(_Template_ThemeModal_Body_Panel());
            L_HTML_Object.Add_Child(_Template_ThemeModal_Body_Footer());


            return L_HTML_Object;
        }

        private HTML_Object _Template_ThemeModal_Body_Panel()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "ThemePanel");
            L_HTML_Object.Add_Attribute("class", "MF_ThemePanel");

            List<DB_Theme> L_ThemeColors = G_Srv_DB.Theme_GetAll().FindAll().ToList();

            for(int i = 0; i < L_ThemeColors.Count; i++)
            {
                L_HTML_Object.Add_Child(_Template_ThemeModal_Body_Panel_Color(L_ThemeColors[i]));
            }

            return L_HTML_Object;
        }

        

    }
}
