
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using Services;

namespace MixerFixerV1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string G_URL_Localhost = "http://localhost:" + App.G_Port.ToString();
        private string G_URL_127 = "http://127.0.0.1:" + App.G_Port.ToString();

        private Srv_Server G_Srv_Server;
        private Srv_MessageBus G_Srv_MessageBus;

        WebView2 WV2_Viewer = null;
        DispatcherTimer G_WV_RunTime_Waiter = null;
        bool G_HasWVRuntime = false;

        public string G_ServerStatus { get; set; }

        //#region ServerStatus
        //public string G_ServerStatus
        //{
        //    get
        //    {
        //        return (string)GetValue(ServerStatusProperty);
        //    }
        //    set
        //    {
        //        SetValue(ServerStatusProperty, value);
        //    }
        //}

        //public readonly DependencyProperty ServerStatusProperty = DependencyProperty.Register(
        //                                                                "ServerStatusProperty",
        //                                                                typeof(string),
        //                                                                typeof(MainWindow), new UIPropertyMetadata("started")
        //                                                            );

        //#endregion ServerStatus

        public MainWindow()
        {
            G_Srv_Server = App.ServiceProvider.GetService(typeof(Srv_Server)) as Srv_Server;
            G_Srv_MessageBus = App.ServiceProvider.GetService(typeof(Srv_MessageBus)) as Srv_MessageBus;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.G_hwnd = new WindowInteropHelper(this).Handle;

            G_ServerStatus = G_Srv_Server.GetServerStatus();
            //if (G_ServerStatus.Contains("Running") == true)
            //{
            //    _InitUI();
            //}
            
            _Check_WV_Runtime();


            if (G_HasWVRuntime == false)
            {
                _ShowWebViewRuntimeDownload();
            }
            else
            {
                _LoadWebView2();
            }
            
            
        }

        private void _Check_WV_Runtime()
        {
            try
            {
                CoreWebView2Environment.GetAvailableBrowserVersionString();
                G_HasWVRuntime = true;
            }
            catch (WebView2RuntimeNotFoundException ex)
            {
                //MessageBox.Show(ex.Message);
                G_HasWVRuntime = false;
            }
        }

        private void _ShowWebViewRuntimeDownload()
        {
            Grid_NoRuntime.Visibility = Visibility.Visible;

            G_WV_RunTime_Waiter = new DispatcherTimer();
            G_WV_RunTime_Waiter.Interval = TimeSpan.FromMilliseconds(1000);
            G_WV_RunTime_Waiter.Tick += G_WV_RunTime_Waiter_Tick;
            G_WV_RunTime_Waiter.Start();
        }

        private void G_WV_RunTime_Waiter_Tick(object? sender, EventArgs e)
        {
            _Check_WV_Runtime();


            if (G_HasWVRuntime == true)
            {
                G_WV_RunTime_Waiter.Stop();
                _LoadWebView2();
            }
            
        }

        private void _LoadWebView2()
        {
            Grid_NoRuntime.Visibility = Visibility.Collapsed;

            WV2_Viewer = new WebView2();
            WV2_Viewer.NavigationCompleted += WV2_Viewer_NavigationCompleted;
            WV2_Viewer.CreationProperties = new CoreWebView2CreationProperties 
            { 
                AdditionalBrowserArguments = "--enable-smooth-scrolling" 
            };


            WV2_Viewer.CoreWebView2InitializationCompleted += WV2_Viewer_CoreWebView2InitializationCompleted;

            Grid.SetRow(WV2_Viewer, 0);

            Grid_Main.Children.Add(WV2_Viewer);

            Txt_ServerOutput.Text = G_ServerStatus;
            if(G_ServerStatus.Contains("Running") == true && WV2_Viewer.Source == null)
            {
                WV2_Viewer.Source = new Uri(G_URL_Localhost);
            }
        }

        int G_Eventtriggercount = 0;
        int G_RetryCount = 0;
        private void WV2_Viewer_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            G_Eventtriggercount++;
            if (e.IsSuccess == false && G_Eventtriggercount < 2 && G_RetryCount < 1)
            {
                G_RetryCount++;
                G_Srv_MessageBus.Emit("showmessage", "Using '" + G_URL_Localhost + "' failed, trying '" + G_URL_127 + "'...");
                WV2_Viewer.Source = new Uri(G_URL_127);
            }

            if (e.IsSuccess == false && G_Eventtriggercount < 2 && G_RetryCount < 2)
            {
                G_RetryCount++;
                G_Srv_MessageBus.Emit("showmessage", "Using '" + G_URL_127 + "' failed, trying '" + App._GetLocalIPAddress() + "'...");
                WV2_Viewer.Source = new Uri(App._GetLocalIPAddress());
            }

            if (G_Eventtriggercount > 1)
            {
                G_Eventtriggercount = 0;
            }
        }

        private void WV2_Viewer_CoreWebView2InitializationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            //smooth-scrolling

            
            //WV2_Viewer.CoreWebView2.OpenDevToolsWindow();
        }

        private void ClearBrowserCache()
        {
            if (WV2_Viewer != null)
            {
                if (WV2_Viewer.CoreWebView2 != null)
                {
                    WV2_Viewer.CoreWebView2.Profile.ClearBrowsingDataAsync();
                }

                WV2_Viewer.Dispose();
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Win_ConfirmShutdown L_Win_ConfirmShutdown = new Win_ConfirmShutdown();
            L_Win_ConfirmShutdown.ShowDialog();

            if(L_Win_ConfirmShutdown.G_DialogResult == 0)
            {
                e.Cancel = true;
                return;
            }

            _DisposeOfWebviewStuffs();

            G_Srv_MessageBus.Emit("windowclosed", L_Win_ConfirmShutdown.G_DialogResult);
        }

        public void _DisposeOfWebviewStuffs()
        {
            if (G_WV_RunTime_Waiter != null && G_WV_RunTime_Waiter.IsEnabled == true)
            {
                G_WV_RunTime_Waiter.Stop();
            }
            
            ClearBrowserCache();
        }

    }
}
