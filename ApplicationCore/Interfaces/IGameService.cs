using ApplicationCore.Enums;
using Domain.DTOs.Game;
using Domain.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IGameService
    {
        Guid CreateChat();

        Guid CreateField();

        Game CancelGame(Guid gameId);

        Game FindGameById(Guid gameId);

        Game JoinToGame(Guid playerId, Game game);

        Game SetWinner(Guid winnerId, Guid loserId, Guid gameId);

        void CreateGamePlayer(Guid gameId, Guid playerId);

        IEnumerable<Game> GetOpenGames();

        Game CreateGame(Game game);

        void UpdateGameState(GameUpdateDTO updateDTO, Guid gameId, GameStatus gameStatus);
    }
}
