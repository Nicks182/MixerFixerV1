using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using Web;

namespace Services
{
    public partial class Srv_UI
    {
        private void _Modal_Theme(Web_InterCommMessage P_Web_InterCommMessage)
        {
            switch (P_Web_InterCommMessage.CommType)
            {
                case Web_InterCommMessage_Type.Theme_Show:
                    _Modal_Theme_Show(P_Web_InterCommMessage);
                    break;

                case Web_InterCommMessage_Type.Theme_Color_Change_Red:
                case Web_InterCommMessage_Type.Theme_Color_Change_Green:
                case Web_InterCommMessage_Type.Theme_Color_Change_Blue:
                    _Modal_Theme_ColorChange(P_Web_InterCommMessage);
                    break;

                case Web_InterCommMessage_Type.Theme_Reset:
                    _Modal_Theme_Reset(P_Web_InterCommMessage);
                    break;

            }


            
        }

        private void _Modal_Theme_Show(Web_InterCommMessage P_Web_InterCommMessage)
        {
            P_Web_InterCommMessage.ModalInfo = new Web_InterCommMessage_Modal
            {
                Id = G_HTML_Templates._Template_ThemeModal_Id(),
                State = 1 // Show
            };

            P_Web_InterCommMessage.HTMLs.Clear();
            P_Web_InterCommMessage.HTMLs.Add(new Web_InterCommMessage_HTML
            {
                ContainerId = "#ModalHolder",
                HTML = G_HTML_Templates._Template_ThemeModal_HTML().ToString(),
                IsAppend = true
            });
        }

        private void _Modal_Theme_ColorChange(Web_InterCommMessage P_Web_InterCommMessage)
        {
            Guid L_Id = Guid.Parse(P_Web_InterCommMessage.Data.Where(d => d.Id == "Id").FirstOrDefault().Value);
            int L_Value = Convert.ToInt32(P_Web_InterCommMessage.Data.Where(d => d.Id == "Value").FirstOrDefault().Value);

            DB_Theme L_DB_Theme = G_Srv_DB.Theme_GetOne_ById(L_Id);

            switch(P_Web_InterCommMessage.CommType)
            {
                case Web_InterCommMessage_Type.Theme_Color_Change_Red:
                    L_DB_Theme.Red = L_Value;
                    break;

                case Web_InterCommMessage_Type.Theme_Color_Change_Green:
                    L_DB_Theme.Green = L_Value;
                    break;

                case Web_InterCommMessage_Type.Theme_Color_Change_Blue:
                    L_DB_Theme.Blue = L_Value;
                    break;
            }

            G_Srv_DB.Theme_Save(L_DB_Theme);

            this.G_Srv_MessageBus.Emit("themechanged", "yes");

            P_Web_InterCommMessage.HTMLs.Clear();
            P_Web_InterCommMessage.HTMLs.Add(_Modal_Theme_ColorChange_StyleUpdate());

            P_Web_InterCommMessage.Data = new List<Web_InterCommMessage_Data>
            {
                new Web_InterCommMessage_Data
                {
                    Id = G_HTML_Templates._Template_ThemeModal_Body_Panel_Color_Slider_Template_Value_Id(L_Id, P_Web_InterCommMessage.CommType) + "_Text",
                    Value = L_Value.ToString(),
                    DataType = Web_InterCommMessage_DataType.ButtonText
                }
            };

            P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type.Theme_Color_Changed;
        }


        private void _Modal_Theme_Reset(Web_InterCommMessage P_Web_InterCommMessage)
        {
            List<DB_Theme> L_Themes = G_Srv_DB.Theme_GetAll().FindAll().ToList();

            for(int i = 0; i < L_Themes.Count; i++)
            {
                G_Srv_DB.Theme_Delete(L_Themes[i]);
            }

            G_Srv_DB.Theme_SetDefaults();

            P_Web_InterCommMessage.HTMLs.Clear();
            P_Web_InterCommMessage.HTMLs.Add(new Web_InterCommMessage_HTML
            {
                ContainerId = "#ThemePanel",
                HTML = G_HTML_Templates._Template_ThemeModal_Body_Panel_HTML().ToString()
            });
            P_Web_InterCommMessage.HTMLs.Add(_Modal_Theme_ColorChange_StyleUpdate());

            P_Web_InterCommMessage.CommType = Web_InterCommMessage_Type.Theme_Color_Changed;

            this.G_Srv_MessageBus.Emit("themechanged", "yes");
        }

        private Web_InterCommMessage_HTML _Modal_Theme_ColorChange_StyleUpdate()
        {
            StringBuilder L_Style = new StringBuilder();

            L_Style.Append(":root{");

            DB_Theme L_DB_Theme_BG = G_Srv_DB.Theme_GetOne(G_Srv_DB.MF_Theme_Background);
            L_Style.Append("--MF_BG_Theme_Red: " + L_DB_Theme_BG.Red + ";");
            L_Style.Append("--MF_BG_Theme_Green: " + L_DB_Theme_BG.Green + ";");
            L_Style.Append("--MF_BG_Theme_Blue: " + L_DB_Theme_BG.Blue + ";");


            DB_Theme L_DB_Theme_Accent = G_Srv_DB.Theme_GetOne(G_Srv_DB.MF_Theme_Accent);
            L_Style.Append("--MF_AC_Theme_Red: " + L_DB_Theme_Accent.Red + ";");
            L_Style.Append("--MF_AC_Theme_Green: " + L_DB_Theme_Accent.Green + ";");
            L_Style.Append("--MF_AC_Theme_Blue: " + L_DB_Theme_Accent.Blue + ";");


            DB_Theme L_DB_Theme_Text = G_Srv_DB.Theme_GetOne(G_Srv_DB.MF_Theme_Text);
            L_Style.Append("--MF_T_Theme_Red: " + L_DB_Theme_Text.Red + ";");
            L_Style.Append("--MF_T_Theme_Green: " + L_DB_Theme_Text.Green + ";");
            L_Style.Append("--MF_T_Theme_Blue: " + L_DB_Theme_Text.Blue + ";");

            L_Style.Append("}");

            return new Web_InterCommMessage_HTML
            {
                ContainerId = "#Style_Theme",
                HTML = L_Style.ToString()
            };
        }

    }
}
