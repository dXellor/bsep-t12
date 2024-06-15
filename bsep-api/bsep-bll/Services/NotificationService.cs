using bsep_bll.Contracts;
using Google.Protobuf;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bsep_bll.Services
{
    public class NotificationService : INotificationService
    {
        private readonly HubConnection hubConnection;
        private readonly IHubProxy hubProxy;

        public NotificationService()
        {
            this.hubConnection = new HubConnection("http://localhost:53353");
            hubProxy = hubConnection.CreateHubProxy("notifications");
        }

        public void Connect()
        {
            hubConnection.Start().Wait();
        }

        public async Task SendMessage(string message)
        {
            await hubProxy.Invoke("SendMessage", message);
        }
    }
}
