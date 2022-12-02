
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Services;

namespace MixerFixerV1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Srv_Server G_Srv_Server;
        private Srv_MessageBus G_Srv_MessageBus;
        public string G_ServerStatus
        {
            get
            {
                return (string)GetValue(ServerStatusProperty);
            }
            set
            {
                SetValue(ServerStatusProperty, value);
            }
        }

        public readonly DependencyProperty ServerStatusProperty = DependencyProperty.Register(
                                                                        "ServerStatusProperty",
                                                                        typeof(string),
                                                                        typeof(MainWindow), new UIPropertyMetadata("started")
                                                                    );

        public MainWindow()
        {
            this.G_Srv_Server = App.ServiceProvider.GetService(typeof(Srv_Server)) as Srv_Server;
            this.G_Srv_MessageBus = App.ServiceProvider.GetService(typeof(Srv_MessageBus)) as Srv_MessageBus;


            this.G_Srv_MessageBus.RegisterEvent("serverstatuschanged", (status) =>
            {
                G_ServerStatus = this.G_Srv_Server.GetServerStatus();
                Txt_ServerOutput.Text = "Status: " + G_ServerStatus;

                if (G_ServerStatus.Contains("Running") == true)
                {
                    _InitUI();
                }
            });

            
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WV2_Viewer.CoreWebView2InitializationCompleted += WV2_Viewer_CoreWebView2InitializationCompleted;
            Task.Run(() =>
            {
                G_Srv_Server.StartServer();
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void _InitUI()
        {

            WV2_Viewer.Source = new Uri("http://127.0.0.1:5000");
            
            
        }

        private void WV2_Viewer_CoreWebView2InitializationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            //WV2_Viewer.CoreWebView2.OpenDevToolsWindow();
        }
    }
}
