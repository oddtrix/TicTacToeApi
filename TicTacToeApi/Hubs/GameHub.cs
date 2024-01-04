using ApplicationCore.Interfaces;
using Domain.DTOs.Game;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace TicTacToeApi.Hubs
{
    public class GameHub : Hub
    {
        public async Task JoinGameGroup(string gameId, string name)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Group(gameId).SendAsync("JoinedPlayer", name);
        }

        public async Task LeaveGameGroup(string gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
        }

        public async Task UpdateGameState(string gameId, Game newState)
        {
            var serializedGameState = JsonSerializer.Serialize(newState);
            await Clients.Group(gameId).SendAsync("ReceiveGameState", serializedGameState);
        }
    }
}
