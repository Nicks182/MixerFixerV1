﻿
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
        private Srv_MessageBus G_Srv_MessageBus;
        private Srv_UI G_UIService;
        private Srv_AudioCore G_Srv_AudioCore;

        public CommandHub(Srv_UI P_UIService)
        {
            G_Srv_MessageBus = App.ServiceProvider.GetService(typeof(Srv_MessageBus)) as Srv_MessageBus;

            G_Srv_AudioCore = new Srv_AudioCore();
            G_Srv_AudioCore.Init();
            G_UIService = P_UIService;
            G_UIService._Init(G_Srv_AudioCore);
        }

        //public CommandHub()
        //{
        //    G_UIService = App.ServiceProvider.GetService(typeof(Srv_UI)) as Srv_UI; ;
        //}

        private Task G_UIService_UpdateData(Web_InterCommMessage P_Web_InterCommMessage)
        {
            Clients.All.SendAsync("ReceiveMessage", P_Web_InterCommMessage);
            return Task.CompletedTask;
        }


        public override Task OnConnectedAsync()
        {
            Console.WriteLine("--> Connection Opened: " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnID", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string P_CommObject)
        {
            //Web_InterCommMessage L_Web_InterCommMessage = JsonSerializer.Deserialize<Web_InterCommMessage>(P_CommObject);
            Web_InterCommMessage L_Web_InterCommMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<Web_InterCommMessage>(P_CommObject);
            //Console.WriteLine("MessageType: " + L_CommObject.CommType.ToString());
            Console.WriteLine("Message Recieved on: " + Context.ConnectionId);

            try
            {
                //HubMessage?.Invoke(L_CommObject);
                G_UIService._ProcessComms(L_Web_InterCommMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", L_CommObject);
            await Clients.All.SendAsync("ReceiveMessage", L_Web_InterCommMessage);
            //await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", new CommObject { CommType = CommObject_Type._DoNothing});
        }

        
    }
}
