using Domain.Entities;

namespace TicTacToeApi.Hubs
{
    public class UserConnection
    {
        public string Name { get; set; }

        public Guid GameId { get; set; }
    }
}
