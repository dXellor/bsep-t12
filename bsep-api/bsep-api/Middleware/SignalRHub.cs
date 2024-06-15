using Microsoft.AspNetCore.SignalR;

namespace bsep_api.Middleware;

public class SignalRHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        //await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} joined");
    }

    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}