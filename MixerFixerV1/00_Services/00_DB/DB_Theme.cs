using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DB_Theme
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = "NA";
        public string Description { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }

    public partial class Srv_DB
    {
        public string MF_Theme_Background = "Background";
        public string MF_Theme_Accent = "Accent";
        public string MF_Theme_Text = "Text";
        public void _SetDefaults()
        {
            DB_Theme L_DB_Theme_BG = Theme_GetOne(MF_Theme_Background);
            if (L_DB_Theme_BG == null)
            {
                L_DB_Theme_BG = new DB_Theme
                {
                    Name = MF_Theme_Background,
                    Description = "Changes here will effect the main background of the application.",
                    Red = 33,
                    Green = 33,
                    Blue = 33
                };

                Theme_Save(L_DB_Theme_BG);
            }

            DB_Theme L_DB_Theme_Accent = Theme_GetOne(MF_Theme_Accent);
            if (L_DB_Theme_Accent == null)
            {
                L_DB_Theme_Accent = new DB_Theme
                {
                    Name = MF_Theme_Accent,
                    Description = "Changes here will effect the accent color of items like buttons, toggles, sliders, ect.",
                    Red = 77,
                    Green = 100,
                    Blue = 111
                };

                Theme_Save(L_DB_Theme_Accent);
            }

            DB_Theme L_DB_Theme_Text = Theme_GetOne(MF_Theme_Text);
            if (L_DB_Theme_Text == null)
            {
                L_DB_Theme_Text = new DB_Theme
                {
                    Name = MF_Theme_Text,
                    Description = "Changes here will effect the main color for text in the application.",
                    Red = 200,
                    Green = 200,
                    Blue = 200
                };

                Theme_Save(L_DB_Theme_Text);
            }
        }

        public ILiteCollection<DB_Theme> Theme_GetAll()
        {
            return this.G_DB_Theme;
        }


        public DB_Theme Theme_GetOne(string P_Name)
        {
            return this.G_DB_Theme.FindOne(a => a.Name == P_Name);
        }

        public DB_Theme Theme_GetOne_ById(Guid P_Id)
        {
            return this.G_DB_Theme.FindOne(a => a.Id == P_Id);
        }

        public void Theme_Save(DB_Theme P_MWM_DB_Theme)
        {
            if (Guid.Empty.Equals(P_MWM_DB_Theme.Id))
            {
                P_MWM_DB_Theme.Id = Guid.NewGuid();
                this.G_DB_Theme.Insert(P_MWM_DB_Theme);
            }
            else
            {
                this.G_DB_Theme.Update(P_MWM_DB_Theme);
            }
        }

        public void Theme_Delete(DB_Theme P_MWM_DB_Theme)
        {
            this.G_DB_Theme.Delete(P_MWM_DB_Theme.Id);
        }

    }
}
