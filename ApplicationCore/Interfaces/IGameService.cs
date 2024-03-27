using Domain.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IGameService
    {
        Guid CreateChat();

        Guid CreateField();

        Game SetDraw(Guid gameId);

        Game CreateGame(Game game);

        Game CancelGame(Guid gameId);

        Game FindGameById(Guid gameId);

        IEnumerable<Game> GetOpenGames();

        Game JoinToGame(Guid playerId, Game game);

        void CreateGamePlayer(Guid gameId, Guid playerId);

        Game SetWinner(Guid winnerId, Guid loserId, Guid gameId);

        Game MakeMove(Guid gameId, Guid fieldId, Guid fieldMovesId, Guid playerId, int index);
    }
}
