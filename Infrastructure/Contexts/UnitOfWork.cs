using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using TicTacToeApi.Contexts;

namespace Infrastructure.Contexts
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDomainContext context;

        private readonly AppIdentityContext identityContext;

        private GenericRepository<Game> gameRepository;

        private GenericRepository<Cell> cellRepository;

        private GenericRepository<Chat> chatRepository;

        private GenericRepository<Field> fieldRepository;

        private GenericRepository<Player> playerRepository;

        private GenericRepository<Message> messageRepository;

        private GenericRepository<FieldMoves> fieldMovesRepository;

        private GamePlayerJunctionRepository gamePlayerRepository;

        public UnitOfWork(AppDomainContext context, AppIdentityContext identityContext)
        {
            this.context = context;
            this.identityContext = identityContext;
        }

        public GenericRepository<Game> GameRepository
        {
            get
            {

                if (this.gameRepository == null)
                {
                    this.gameRepository = new GenericRepository<Game>(this.context);
                }

                return gameRepository;
            }
        }

        public GenericRepository<Cell> CellRepository
        {
            get
            {

                if (this.cellRepository == null)
                {
                    this.cellRepository = new GenericRepository<Cell>(this.context);
                }

                return cellRepository;
            }
        }

        public GenericRepository<Chat> ChatRepository
        {
            get
            {

                if (this.chatRepository == null)
                {
                    this.chatRepository = new GenericRepository<Chat>(this.context);
                }

                return chatRepository;
            }
        }

        public GenericRepository<Field> FieldRepository
        {
            get
            {

                if (this.fieldRepository == null)
                {
                    this.fieldRepository = new GenericRepository<Field>(this.context);
                }

                return fieldRepository;
            }
        }

        public GenericRepository<Player> PlayerRepository
        {
            get
            {

                if (this.playerRepository == null)
                {
                    this.playerRepository = new GenericRepository<Player>(this.context, this.identityContext);
                }

                return playerRepository;
            }
        }

        public GenericRepository<Message> MessageRepository
        {
            get
            {

                if (this.messageRepository == null)
                {
                    this.messageRepository = new GenericRepository<Message>(this.context);
                }

                return messageRepository;
            }
        }

        public GenericRepository<FieldMoves> FieldMovesRepository
        {
            get
            {

                if (this.fieldMovesRepository == null)
                {
                    this.fieldMovesRepository = new GenericRepository<FieldMoves>(this.context);
                }

                return fieldMovesRepository;
            }
        }

        public GamePlayerJunctionRepository GamePlayerJunctionRepository
        {
            get
            {

                if (this.gamePlayerRepository == null)
                {
                    this.gamePlayerRepository = new GamePlayerJunctionRepository(this.context);
                }

                return gamePlayerRepository;
            }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
