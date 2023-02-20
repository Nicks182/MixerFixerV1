using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using MixerFixerV1;

namespace Services
{
    public class Srv_Server
    {
        private Srv_MessageBus G_Srv_MessageBus;

        public bool G_Enabled = false;
        private string G_ServerStatus;
        private IWebHost G_Server = null;

        public Srv_Server()
        {
            G_Srv_MessageBus = App.ServiceProvider.GetService(typeof(Srv_MessageBus)) as Srv_MessageBus;
        }

        public void StartServer()
        {
            try
            {
                if (G_Enabled == true)
                {
                    return;
                }

                G_Server = WebHost.CreateDefaultBuilder()
                    .UseKestrel(x =>
                    {
                        x.ListenAnyIP(App.G_Port);
                        x.ListenLocalhost(App.G_Port);

                    })
                    .UseStartup<Startup>()
                    .UseUrls("http://*:" + App.G_Port.ToString())
                    .Build();

                G_ServerStatus = "Starting";

                G_Srv_MessageBus.Emit("serverstatuschanged", G_ServerStatus);

                Task.Run(() =>
                {
                    try
                    {
                        Thread.Sleep(500);
                        G_Server.RunAsync();
                        G_ServerStatus = "Running...";
                        G_Srv_MessageBus.Emit("serverstatuschanged", G_ServerStatus);
                    }
                    catch (Exception ex)
                    {
                        G_Srv_MessageBus.Emit("exception", ex);
                    }
                });

            }
            catch (Exception ex)
            {
                G_Srv_MessageBus.Emit("exception", ex);
            }
        }



        public void StopServer()
        {
            if (G_Server != null)
            {
                G_ServerStatus = "Shutting down";
                G_Srv_MessageBus.Emit("serverstatuschanged", G_ServerStatus);
                G_Server.StopAsync().Wait();

            }
            Thread.Sleep(3000);
            G_ServerStatus = "Down";
            G_Srv_MessageBus.Emit("serverstatuschanged", G_ServerStatus);

        }

        public string GetServerStatus()
        {
            return G_ServerStatus;
        }


        

    }
}
