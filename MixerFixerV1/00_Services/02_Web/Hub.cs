
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using MixerFixerV1;

namespace Services
{
    public class CommandHub : Hub
    {
        private Srv_UI G_UIService;
        private Srv_AudioCore G_Srv_AudioCore;

        public CommandHub(Srv_UI P_UIService)
        {
            G_UIService = P_UIService;
        }

        public override Task OnConnectedAsync()
        {
            G_Srv_AudioCore = App.ServiceProvider.GetService(typeof(Srv_AudioCore)) as Srv_AudioCore;

            G_UIService._Init(G_Srv_AudioCore);

            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnID", Context.ConnectionId);
            HubUserHandler.ConnectedIds.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? P_Ex)
        {
            HubUserHandler.ConnectedIds.Remove(Context.ConnectionId);
            if (HubUserHandler.ConnectedIds.Count < 1)
            {
                G_UIService._StopDataPush();
            }
            return base.OnDisconnectedAsync(P_Ex);
        }

        public async Task SendMessage(string P_CommObject)
        {
            Web_InterCommMessage L_Web_InterCommMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<Web_InterCommMessage>(P_CommObject);

            try
            {
                G_UIService._ProcessComms(L_Web_InterCommMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            await Clients.All.SendAsync("ReceiveMessage", L_Web_InterCommMessage);
        }

        
    }

    public static class HubUserHandler
    {
        public static HashSet<string> ConnectedIds = new HashSet<string>();
    }
}
