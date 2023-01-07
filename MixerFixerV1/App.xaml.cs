using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MixerFixerV1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider;

        private Srv_MessageBus G_Srv_MessageBus;

        public App()
        {
            LoadDepedencies();
        }

        public static ServiceCollection Services { get; set; } = new ServiceCollection();
        private void LoadDepedencies()
        {
            Services.AddSingleton<Srv_MessageBus>();
            Services.AddSingleton<Srv_DB>();
            Services.AddSingleton<Srv_Server>();

            App.ServiceProvider = Services.BuildServiceProvider();

            
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Orignal colors
            //SolidColorBrush L_MainThemeBG = new SolidColorBrush(Color.FromArgb(255, (byte)30, (byte)30, (byte)30));
            //SolidColorBrush L_MainThemeFG = new SolidColorBrush(Color.FromArgb(255, (byte)77, (byte)100, (byte)111));

            this.G_Srv_MessageBus = App.ServiceProvider.GetService(typeof(Srv_MessageBus)) as Srv_MessageBus;

            this.G_Srv_MessageBus.RegisterEvent("themechanged", (status) =>
            {
                _LoadTheme();
            });

            _LoadTheme();

            //int r = 33;
            //int g = 33;
            //int b = 33;
            //int offset = 5;

            //SolidColorBrush L_MainThemeBG = new SolidColorBrush(Color.FromArgb(255, (byte)(r - offset), (byte)(g - offset), (byte)(b - offset)));
            //SolidColorBrush L_MainThemeFG = new SolidColorBrush(Color.FromArgb(255, (byte)77, (byte)100, (byte)111));

            //var resourceDictionary = Application.Current.Resources.MergedDictionaries[0];
            //resourceDictionary["BackgroundColour"] = L_MainThemeBG;
            //resourceDictionary["WindowBorderColour"] = L_MainThemeBG;
            //resourceDictionary["ControlDefaultForeground"] = L_MainThemeFG;
            //resourceDictionary["ControlGlythColour"] = L_MainThemeFG;
        }

        private void _LoadTheme()
        {
            var resourceDictionary = Application.Current.Resources.MergedDictionaries[0];

            Srv_DB L_Srv_DB = App.ServiceProvider.GetService(typeof(Srv_DB)) as Srv_DB;
            L_Srv_DB._SetDefaults();

            DB_Theme L_DB_Theme_BG = L_Srv_DB.Theme_GetOne(L_Srv_DB.MF_Theme_Background);


            if (L_DB_Theme_BG != null)
            {
                int offset = 5;
                SolidColorBrush L_MainThemeBG = new SolidColorBrush(Color.FromArgb(255, (byte)(L_DB_Theme_BG.Red - offset), (byte)(L_DB_Theme_BG.Green - offset), (byte)(L_DB_Theme_BG.Blue - offset)));
                resourceDictionary["BackgroundColour"] = L_MainThemeBG;
                resourceDictionary["WindowBorderColour"] = L_MainThemeBG;
            }

            DB_Theme L_DB_Theme_Accent = L_Srv_DB.Theme_GetOne(L_Srv_DB.MF_Theme_Accent);
            if (L_DB_Theme_Accent != null)
            {
                SolidColorBrush L_MainThemeFG = new SolidColorBrush(Color.FromArgb(255, (byte)L_DB_Theme_Accent.Red, (byte)L_DB_Theme_Accent.Green, (byte)L_DB_Theme_Accent.Blue));
                resourceDictionary["ControlDefaultForeground"] = L_MainThemeFG;
                resourceDictionary["ControlGlythColour"] = L_MainThemeFG;
            }

        }

    }
}
