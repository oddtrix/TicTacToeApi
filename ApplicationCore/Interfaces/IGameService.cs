using Domain.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IGameService
    {
        void SetDraw(Game game);

        Game CancelGame(Guid gameId);

        IEnumerable<Game> GetOpenGames();

        Game FindGameByIdWithInclude(Guid gameId);

        Game JoinToGame(Guid playerId, Game game);

        Game CreateGame(Game game, Guid playerId);

        void CreateGamePlayer(Guid gameId, Guid playerId);

        void SetWinner(Guid winnerId, Guid loserId, Game game);

        Game SendMessage(Guid gameId, string messageBody, Guid chatId, Guid playerId);

        Game MakeMove(Guid gameId, Guid fieldId, Guid fieldMovesId, Guid playerId, int index);
    }
}
