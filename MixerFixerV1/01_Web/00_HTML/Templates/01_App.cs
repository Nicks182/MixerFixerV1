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
        Srv_AudioCore G_Srv_AudioCore;
        public HTML_Templates(Srv_AudioCore P_Srv_AudioCore)
        {
            G_Srv_AudioCore = P_Srv_AudioCore;
        }

        public StringBuilder _Template_App_HTML()
        {
            return new HTML()._BuildHtml(_Template_AppPanel());
        }

        private HTML_Object _Template_AppPanel()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "MixerApp");
            L_HTML_Object.Add_Attribute("class", "MF_MixerApp");

            L_HTML_Object.Add_Child(_Template_App_Menu());
            L_HTML_Object.Add_Child(_Template_App_Mixer());

            return L_HTML_Object;
        }

        private HTML_Object _Template_App_Mixer()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "PanelHolder");

            L_HTML_Object.Add_Child(_Template_GetVisiblePanel());

            return L_HTML_Object;
        }

        public HTML_Object _Template_GetVisiblePanel()
        {
            if(G_Srv_AudioCore.G_ShowSettings == true)
            {
                return _Template_SettingsPanel();
            }
            return _Template_Device(G_Srv_AudioCore._Get_VisibleDevice());
            
        }

        public StringBuilder _Template_GetVisiblePanel_HTML()
        {
            return new HTML()._BuildHtml(_Template_GetVisiblePanel());
        }

    }
}
