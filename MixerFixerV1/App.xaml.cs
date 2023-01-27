

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
using System.Net;
using System.Net.Sockets;

namespace MixerFixerV1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static IntPtr G_hwnd = new IntPtr(0xFFFF);
        public static string G_BaseDir = Process.GetCurrentProcess().MainModule.FileName.Replace("\\MixerFixerV1.exe", "");
        public static int G_Port = 0;
        //public static string G_LocalIP = "NA";

        public static NotifyIcon G_NotifyIcon;
        private ContextMenuStrip G_NotifyIcon_Menu;

        public static ServiceProvider ServiceProvider;

        private Srv_MessageBus G_Srv_MessageBus;
        private Srv_Server G_Srv_Server;
        private Srv_DB G_Srv_DB;

        private MainWindow G_MainWindow;

        public App()
        {
            G_Port = _Get_port();
            //G_LocalIP = _GetLocalIPAddress();
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
            Services.AddSingleton<Srv_DisplaySettings>();
            

            App.ServiceProvider = Services.BuildServiceProvider();


            _MessageBusRegisterEvents();


            G_Srv_Server = App.ServiceProvider.GetService(typeof(Srv_Server)) as Srv_Server;

            

            Task.Run(() =>
            {
                G_Srv_Server.StartServer();
                
            });
        }

        private void _MessageBusRegisterEvents()
        {
            G_Srv_MessageBus = App.ServiceProvider.GetService(typeof(Srv_MessageBus)) as Srv_MessageBus;

            G_Srv_MessageBus.RegisterEvent("serverstatuschanged", (status) =>
            {
                if (G_Srv_Server.GetServerStatus().Contains("Running") == true)
                {
                    _InitUI();
                }
            });

            G_Srv_MessageBus.RegisterEvent("windowclosed", (P_DoShutdown) =>
            {
                if (Convert.ToInt32(P_DoShutdown) == 1)
                {
                    _DoShutdown();
                }
            });

            G_Srv_MessageBus.RegisterEvent("exception", (P_Expection) =>
            {
                Exception L_Ex = (P_Expection as Exception);
                System.Windows.MessageBox.Show(L_Ex.Message 
                    + Environment.NewLine
                    + Environment.NewLine
                    +L_Ex.StackTrace);

                _DoShutdown();
            });

            G_Srv_MessageBus.RegisterEvent("showmessage", (P_MessageString) =>
            {
                System.Windows.MessageBox.Show(P_MessageString.ToString());
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

        private int _Get_port()
        {
            string L_PortFile = Path.Combine(G_BaseDir, "port.txt");
            if(File.Exists(L_PortFile) == true)
            {
                string[] lines = File.ReadAllLines(L_PortFile);
                try
                {
                    return Convert.ToInt32(lines[0].Trim());
                }
                catch
                {

                }
            }

            return 5555;// Default
        }

        public static string _GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return "http://" + ip.ToString() + ":" + App.G_Port.ToString();
                }
            }

            return "NA";
        }

    }
}
