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
        public App()
        {
            LoadDepedencies();
        }

        public static ServiceCollection Services { get; set; } = new ServiceCollection();
        private void LoadDepedencies()
        {
            Services.AddSingleton<Srv_MessageBus>();
            Services.AddSingleton<Srv_Server>();

            App.ServiceProvider = Services.BuildServiceProvider();

        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Orignal colors
            //SolidColorBrush L_MainThemeBG = new SolidColorBrush(Color.FromArgb(255, (byte)30, (byte)30, (byte)30));
            //SolidColorBrush L_MainThemeFG = new SolidColorBrush(Color.FromArgb(255, (byte)77, (byte)100, (byte)111));

            SolidColorBrush L_MainThemeBG = new SolidColorBrush(Color.FromArgb(255, (byte)33, (byte)33, (byte)33));
            SolidColorBrush L_MainThemeFG = new SolidColorBrush(Color.FromArgb(255, (byte)77, (byte)100, (byte)111));

            var resourceDictionary = Application.Current.Resources.MergedDictionaries[0];
            resourceDictionary["BackgroundColour"] = L_MainThemeBG;
            resourceDictionary["WindowBorderColour"] = L_MainThemeBG;
            resourceDictionary["ControlDefaultForeground"] = L_MainThemeFG;
            resourceDictionary["ControlGlythColour"] = L_MainThemeFG;
        }
    }
}
