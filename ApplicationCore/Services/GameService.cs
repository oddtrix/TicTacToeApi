using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using Domain.DTOs.Game;
using Domain.Entities;

namespace ApplicationCore.Services
{
    public class GameService : IGameService
    {
        private readonly IEntityService<Game> gameRepository;
        private readonly IEntityService<Chat> chatRepository;
        private readonly IEntityService<Field> fieldRepository;
        private readonly IEntityService<Player> playerRepository;
        private readonly IEntityService<GamePlayerJunction> gamePlayerRepository;

        public GameService(IEntityService<Game> gameRepository,
            IEntityService<Chat> chatRepository,
            IEntityService<Field> fieldRepository,
            IEntityService<Player> playerRepository,
            IEntityService<GamePlayerJunction> gamePlayerRepository
            )
        {
            this.gameRepository = gameRepository;
            this.chatRepository = chatRepository;
            this.fieldRepository = fieldRepository;
            this.playerRepository = playerRepository;
            this.gamePlayerRepository = gamePlayerRepository;
        }

        public Game CreateGame(Game game)
        {
            game.ChatId = CreateChat();
            game.FieldId = CreateField();
            game.GameStatus = GameStatus.Pending;
            game.isPrivate = false;
            gameRepository.Create(game);
            return game;
        }

        public Guid CreateChat()
        {
            var chat = new Chat() { Id = Guid.NewGuid() };
            this.chatRepository.Create(chat);
            return chat.Id;
        }

        public Guid CreateField()
        {
            var field = new Field() { Id = Guid.NewGuid() };
            this.fieldRepository.Create(field);
            return field.Id;
        }

        public void CreateGamePlayer(Guid gameId, Guid playerId)
        {
            var player = this.playerRepository.GetById(playerId);
            var gamePlayer = new GamePlayerJunction() { PlayerId = playerId, Player = player, GameId = gameId };
            gamePlayerRepository.Create(gamePlayer);
        }

        public Game JoinToGame(Guid playerId, Game game)
        {
            var playerToAdd = this.playerRepository.GetById(playerId);
            var gamePlayer = new GamePlayerJunction() { PlayerId = playerId, Player = playerToAdd, GameId = game.Id };
            if (game.GamesPlayers.Count() < 2)
            {
                game.GamesPlayers.Add(gamePlayer);
                game.GameStatus = GameStatus.Started;
                this.gamePlayerRepository.Create(gamePlayer);
            }
            return game;
        }

        public void UpdateGameState(GameUpdateDTO updateDTO, Guid gameId, GameStatus gameStatus)
        {
            /*updateDTO.Id = gameId;
            updateDTO.GameStatus = gameStatus;
            this.gameRepository.Update(updateDTO);*/
        }

        public Game FindGameById(Guid gameId)
        {
            var game = this.gameRepository.GetByIdWithInclude(gameId, g => g.Chat, g => g.Field, g => g.GamesPlayers);
            return game;
        }

        public Game SetWinner(Guid winnerId, Guid loserId, Guid gameId)
        {
            var game = this.gameRepository.GetById(gameId);
            var winner = this.playerRepository.GetById(winnerId);
            var loser = this.playerRepository.GetById(loserId);

            game.Winner = winner;
            game.GameStatus = GameStatus.Completed;
            winner.Rating += 10;
            loser.Rating -= 10;

            this.playerRepository.Update(winner);
            this.playerRepository.Update(loser);
            this.gameRepository.Update(game);

            return game;
        }

        public Game CancelGame(Guid gameId)
        {
            var game = this.gameRepository.GetById(gameId);
            game.GameStatus = GameStatus.Canceled;
            this.gameRepository.Update(game);
            return game;
        }

        public IEnumerable<Game> GetOpenGames()
        {
            var games = this.gameRepository.GetAll().Where(g => g.GameStatus == GameStatus.Pending);
            return games;
        }
    }
}
