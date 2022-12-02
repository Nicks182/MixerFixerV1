using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
    }
}
