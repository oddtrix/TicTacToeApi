using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace TicTacToeApi.Hubs
{
    public class GameHub : Hub
    {
//create new named group and throw players
        public async Task JoinGameGroup(string gameId, string name)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Group(gameId).SendAsync("JoinedPlayer", name);
        }

        public async Task LeaveGameGroup(string gameId, string name)
        {
            await Clients.Group(gameId).SendAsync("LeaveGroup", name);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
        }

        public async Task UpdateGameState(string gameId, Game newState)
        {
            var serializedGameState = JsonConvert.SerializeObject(newState);
            await Clients.OthersInGroup(gameId).SendAsync("ReceiveGameState", serializedGameState);
        }
    }
}