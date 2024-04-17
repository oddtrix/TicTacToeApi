using Domain.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IGameService
    {
        Game SetDraw(Guid gameId);

        Game CancelGame(Guid gameId);

        IEnumerable<Game> GetOpenGames();

        Game FindGameByIdWithInclude(Guid gameId);

        Game JoinToGame(Guid playerId, Game game);

        Game CreateGame(Game game, Guid playerId);

        void CreateGamePlayer(Guid gameId, Guid playerId);

        Game SetWinner(Guid winnerId, Guid loserId, Guid gameId);

        Game SendMessage(Guid gameId, string messageBody, Guid chatId, Guid playerId);

        Game MakeMove(Guid gameId, Guid fieldId, Guid fieldMovesId, Guid playerId, int index);
    }
}
