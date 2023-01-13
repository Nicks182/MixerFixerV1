

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;

using NotifyIcon = System.Windows.Forms.NotifyIcon;
using System.Windows.Forms;

using Services;
using System.Diagnostics;

namespace MixerFixerV1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        //public static string G_BaseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //public static string G_BaseDir = Assembly.GetExecutingAssembly().Location.Replace("\\MixerFixerV1.dll","");
        //public static string G_BaseDir = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
        public static string G_BaseDir = Process.GetCurrentProcess().MainModule.FileName.Replace("\\MixerFixerV1.exe", "");

        public static NotifyIcon G_NotifyIcon;
        private ContextMenuStrip G_NotifyIcon_Menu;

        public static ServiceProvider ServiceProvider;

        private Srv_MessageBus G_Srv_MessageBus;
        private Srv_Server G_Srv_Server;
        private Srv_DB G_Srv_DB;
        //public Srv_AudioCore G_Srv_AudioCore;
        //private Srv_UI G_Srv_UI;

        private MainWindow G_MainWindow;
        //private Srv_DB G_Srv_DB;
        
        //public static Srv_UI G_Srv_UI;

        public App()
        {
            
        LoadDepedencies();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _Init_TrayIcon();

            base.OnStartup(e);
        }

        private void _Init_TrayIcon()
        {
            Stream L_MenuIconStream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/MixerFixerV1;component/Theme/Images/CloseIcon-16x16.png")).Stream;

            System.Drawing.Image L_ContextImage = System.Drawing.Image.FromStream(L_MenuIconStream);
            L_MenuIconStream.Dispose();

            G_NotifyIcon_Menu = new ContextMenuStrip();
            G_NotifyIcon_Menu.Items.Add("Exit!", L_ContextImage, new System.EventHandler(TrayIconExit_Click));

            App.G_NotifyIcon = new NotifyIcon();
            G_NotifyIcon.Click += new EventHandler(TrayIcon_Click);
            //G_NotifyIcon.Icon = new System.Drawing.Icon(typeof(App), "favicon.ico");
            G_NotifyIcon.Text = "MixerFixer";
            Stream L_TrayIconStream = System.Windows.Application.GetResourceStream(new Uri("pack://application:,,,/MixerFixerV1;component/Theme/Images/TrayIcon.ico")).Stream;
            G_NotifyIcon.Icon = new System.Drawing.Icon(L_TrayIconStream);
            L_TrayIconStream.Dispose();

            G_NotifyIcon.ContextMenuStrip = G_NotifyIcon_Menu;

            G_NotifyIcon.Visible = true;
        }

        private void TrayIcon_Click(Object sender, EventArgs e)
        {
            if ((e as System.Windows.Forms.MouseEventArgs).Button == System.Windows.Forms.MouseButtons.Left)
            {
                G_MainWindow = new MainWindow();
                G_MainWindow.Show();
            }
        }

        protected void TrayIconExit_Click(Object sender, System.EventArgs e)
        {
            _DoShutdown();
        }


        public static ServiceCollection Services { get; set; } = new ServiceCollection();
        private void LoadDepedencies()
        {
            Services.AddSingleton<Srv_MessageBus>();
            Services.AddSingleton<Srv_DB>();
            Services.AddSingleton<Srv_Server>();
            Services.AddSingleton<Srv_AudioCore>();
            

            App.ServiceProvider = Services.BuildServiceProvider();



            G_Srv_MessageBus = App.ServiceProvider.GetService(typeof(Srv_MessageBus)) as Srv_MessageBus;
            G_Srv_Server = App.ServiceProvider.GetService(typeof(Srv_Server)) as Srv_Server;

            G_Srv_MessageBus.RegisterEvent("serverstatuschanged", (status) =>
            {
                if (G_Srv_Server.GetServerStatus().Contains("Running") == true)
                {
                    _InitUI();
                }
            });

            G_Srv_MessageBus.RegisterEvent("windowclosed", (P_DoShutdown) =>
            {
                if (Convert.ToBoolean(P_DoShutdown) == true)
                {
                    _DoShutdown();
                }
            });

            Task.Run(() =>
            {
                G_Srv_Server.StartServer();
                
            });
        }

        private void _DoShutdown()
        {
            if (G_MainWindow != null)
            {
                //G_MainWindow.Close();
                //G_MainWindow._DisposeOfWebviewStuffs();
                G_MainWindow = null;
            }
            App.Current.Shutdown();
        }

        private void _InitUI()
        {
            DB_Settings L_StartHidden = G_Srv_DB.Settings_GetOne(G_Srv_DB.G_StartHidden);

            if (L_StartHidden.Value == "0")
            {
                G_MainWindow = new MainWindow();
                G_MainWindow.Show();
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            G_Srv_MessageBus = App.ServiceProvider.GetService(typeof(Srv_MessageBus)) as Srv_MessageBus;
            //G_Srv_DB = App.ServiceProvider.GetService(typeof(Srv_DB)) as Srv_DB;
            
            //G_Srv_AudioCore = new Srv_AudioCore(G_Srv_DB);


            G_Srv_MessageBus.RegisterEvent("themechanged", (status) =>
            {
                _LoadTheme();
            });

            _LoadTheme();

            

            string bla = "";
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

            // Orignal colors
            //SolidColorBrush L_MainThemeBG = new SolidColorBrush(Color.FromArgb(255, (byte)30, (byte)30, (byte)30));
            //SolidColorBrush L_MainThemeFG = new SolidColorBrush(Color.FromArgb(255, (byte)77, (byte)100, (byte)111));


        }

        private void _LoadTheme()
        {
            var resourceDictionary = System.Windows.Application.Current.Resources.MergedDictionaries[0];

            G_Srv_DB = App.ServiceProvider.GetService(typeof(Srv_DB)) as Srv_DB;
            G_Srv_DB.Theme_SetDefaults();

            DB_Theme L_DB_Theme_BG = G_Srv_DB.Theme_GetOne(G_Srv_DB.MF_Theme_Background);


            if (L_DB_Theme_BG != null)
            {
                int offset = 5;
                int L_R = L_DB_Theme_BG.Red - offset;
                int L_G = L_DB_Theme_BG.Green - offset;
                int L_B = L_DB_Theme_BG.Blue - offset;

                if(L_R < 0)
                    L_R = 0;

                if (L_G < 0)
                    L_G = 0;

                if (L_B < 0)
                    L_B = 0;

                SolidColorBrush L_MainThemeBG = new SolidColorBrush(Color.FromArgb(255, (byte)L_R, (byte)L_G, (byte)L_B));
                resourceDictionary["BackgroundColour"] = L_MainThemeBG;
                resourceDictionary["WindowBorderColour"] = L_MainThemeBG;
            }

            DB_Theme L_DB_Theme_Accent = G_Srv_DB.Theme_GetOne(G_Srv_DB.MF_Theme_Accent);
            if (L_DB_Theme_Accent != null)
            {
                SolidColorBrush L_MainThemeFG = new SolidColorBrush(Color.FromArgb(255, (byte)L_DB_Theme_Accent.Red, (byte)L_DB_Theme_Accent.Green, (byte)L_DB_Theme_Accent.Blue));
                resourceDictionary["ControlDefaultForeground"] = L_MainThemeFG;
                resourceDictionary["ControlGlythColour"] = L_MainThemeFG;
            }

        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            App.G_NotifyIcon.Visible = false;
            App.G_NotifyIcon.Dispose();
        }
    }
}
