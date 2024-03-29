using Microsoft.AspNetCore.SignalR;

namespace TicTacToeApi.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinChatGroup(string gameId, string name)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Group(gameId).SendAsync("JoinedChatMember", name);
        }

        public async Task SendMessage(string gameId, string message)
        {
            await Clients.Group(gameId).SendAsync("GetMessage", message);
        }
    }
}
