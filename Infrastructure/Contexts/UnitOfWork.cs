using Domain.Entities;
using Infrastructure.Interfaces;
using TicTacToeApi.Contexts;

namespace Infrastructure.Contexts
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDomainContext context;

        public UnitOfWork(AppDomainContext context,
            IGameRepository gameRepository,
            ICellRepository cellRepository,
            IChatRepository chatRepository,
            IFieldRepository fieldRepository,
            IPlayerRepository playerRepository,
            IMessageRepository messageRepository,
            IFieldMovesRepository fieldMovesRepository,
            IGamePlayerJunctionRepository gamePlayerJunctionRepository)
        {
            this.context = context;
            this.GameRepository = gameRepository;
            this.CellRepository = cellRepository;
            this.ChatRepository = chatRepository;
            this.FieldRepository = fieldRepository;
            this.PlayerRepository = playerRepository;
            this.MessageRepository = messageRepository;
            this.FieldMovesRepository = fieldMovesRepository;
            this.GamePlayerRepository = gamePlayerJunctionRepository;
        }

        public IGameRepository GameRepository { get; set; }

        public ICellRepository CellRepository { get; set; }

        public IChatRepository ChatRepository { get; set; }

        public IFieldRepository FieldRepository { get; set; }

        public IPlayerRepository PlayerRepository { get; set; }

        public IMessageRepository MessageRepository { get; set; }

        public IFieldMovesRepository FieldMovesRepository { get; set; }

        public IGamePlayerJunctionRepository GamePlayerRepository { get; set; }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
