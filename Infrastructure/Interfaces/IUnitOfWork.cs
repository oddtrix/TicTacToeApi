using Domain.Entities;
using Infrastructure.Repositories;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        GenericRepository<Game> GameRepository { get; }

        GenericRepository<Cell> CellRepository { get; }

        GenericRepository<Chat> ChatRepository { get; }

        GenericRepository<Field> FieldRepository { get; }

        GenericRepository<Player> PlayerRepository { get; }

        GenericRepository<Message> MessageRepository { get; }

        GenericRepository<FieldMoves> FieldMovesRepository { get; }

        GamePlayerJunctionRepository GamePlayerJunctionRepository { get; }

        void Save();
    }
}
