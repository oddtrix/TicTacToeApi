using Domain.Entities;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IGameRepository GameRepository { get; set; }

        ICellRepository CellRepository { get; set; }

        IChatRepository ChatRepository { get; set; }

        IFieldRepository FieldRepository { get; set; }

        IPlayerRepository PlayerRepository { get; set; }

        IMessageRepository MessageRepository { get; set; }

        IFieldMovesRepository FieldMovesRepository { get; set; }

        IGamePlayerJunctionRepository GamePlayerRepository { get; set; }

        void Save();
    }
}
