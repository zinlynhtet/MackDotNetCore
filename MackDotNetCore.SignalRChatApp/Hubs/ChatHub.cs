using Microsoft.AspNetCore.SignalR;

namespace MackDotNetCore.SignalRChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SeverReceiveMessage(string user, string message)
        {
            await Clients.All.SendAsync("ClientReceiveMessage", user, message);
        }
    }
}
