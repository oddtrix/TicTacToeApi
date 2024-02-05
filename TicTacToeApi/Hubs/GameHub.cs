using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TicTacToeApi.Hubs
{
    public class GameHub : Hub
    {
/*        public async Task GetConnectionId(string gameId)
        {
            string connectionId = Context.ConnectionId;

            await Clients.Group(gameId).SendAsync("GetConnectionId", connectionId);
        }
*/
        public async Task JoinGameGroup(string gameId, string name)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            await Clients.Group(gameId).SendAsync("JoinedPlayer", name);
/*
            string connectionId = Context.ConnectionId;
            await Clients.Group(gameId).SendAsync("GetConnectionId", connectionId);*/
        }

        public async Task LeaveGameGroup(string gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
        }

        public async Task UpdateGameState(string gameId, Game newState)
        {
            var serializedGameState = JsonConvert.SerializeObject(newState, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            await Clients.Group(gameId).SendAsync("ReceiveGameState", serializedGameState);
        }
    }
}
