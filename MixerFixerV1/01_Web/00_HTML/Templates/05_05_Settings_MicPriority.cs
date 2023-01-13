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

        
        
        
        #region MicPriority
        private HTML_Object _Template_SettingsModal_Body_MicPriority()
        {
            HTML_Object L_HTML_Object = new HTML_Object();
            L_HTML_Object.Type = HTML_Object_Type.IsDiv;

            L_HTML_Object.Add_Attribute("id", "Div_Settings_MicPriority");
            L_HTML_Object.Add_Attribute("class", "MF_SettingsPanel_MicPriority");

            L_HTML_Object.Children.AddRange(_Template_SettingsModal_Body_MicPriority_Body());

            //L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Heading("Capture Priority:"));

            //List<DB_DevicePriority> L_Devices = G_Srv_DB.DevicePriority_GetAll().Find(ao => ao.IsMic == true).OrderBy(ao => ao.Priority).ToList();
            //L_HTML_Object.Add_Child(_Template_SettingsModal_Body_DevicePriority_Items(L_Devices));

            return L_HTML_Object;
        }

        private List<HTML_Object> _Template_SettingsModal_Body_MicPriority_Body()
        {
            List<DB_DevicePriority> L_Devices = G_Srv_DB.DevicePriority_GetAll().Find(ao => ao.IsMic == true).OrderBy(ao => ao.Priority).ToList();

            return new List<HTML_Object>
            {
                _Template_SettingsModal_Body_DevicePriority_Heading("Capture Priority:"),
                _Template_SettingsModal_Body_DevicePriority_Items(L_Devices)
            };
        }

        public StringBuilder _Template_SettingsModal_Body_MicPriority_HTML()
        {
            return G_HTML._BuildHtml(_Template_SettingsModal_Body_MicPriority_Body());
        }

        #endregion MicPriority

        

    }
}
