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

        private HTML_Object _Template_App_Menu()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "MixerAppMenu");
            L_HTML_Object.Add_Attribute("class", "MF_MixerAppMenu");

            L_HTML_Object.Add_Child(_Template_App_Menu_DeviceBtn(G_Srv_AudioCore.Device));

            L_HTML_Object.Add_Child(_Template_App_Menu_SettingsBtn());
            L_HTML_Object.Add_Child(_Template_App_Menu_ThemeBtn());
            L_HTML_Object.Add_Child(_Template_App_Menu_DisplaySettingsBtn());

            //for (int i = 0; i < G_Srv_AudioCore.Devices.Count; i++)
            //{
            //    L_HTML_Object.Add_Child(_Template_App_Menu_DeviceBtn(G_Srv_AudioCore.Devices[i]));
            //}



            return L_HTML_Object;
        }

    }
}
