using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace ApplicationCore.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IChatService chatService;

        private readonly IFieldService fieldService;

        public GameService(IUnitOfWork unitOfWork, IChatService chatService, IFieldService fieldService)
        {
            this.unitOfWork = unitOfWork;
            this.chatService = chatService;
            this.fieldService = fieldService;
        }

        public Game CreateGame(Game game, Guid playerId)
        {
            game.GameCreatorId = playerId;
            game.ChatId = this.chatService.CreateChat();

            game.FieldId = this.fieldService.CreateField();
            var field = this.unitOfWork.FieldRepository.GetById(game.FieldId);
            game.Field = field;
            var fieldMovesId = this.fieldService.CreateFieldMoves(game.FieldId);
            var fieldMoves = this.unitOfWork.FieldMovesRepository.GetById(fieldMovesId);
            game.Field.FieldMoves = fieldMoves;
            fieldMoves.FieldId = game.FieldId;

            game.GameStatus = GameStatus.Pending;
            game.IsPrivate = false;
            this.unitOfWork.GameRepository.Create(game);
            this.unitOfWork.Save();

            return game;
        }

        public void CreateGamePlayer(Guid gameId, Guid playerId)
        {
            var player = this.unitOfWork.PlayerRepository.GetById(playerId);
            var gamePlayer = new GamePlayerJunction() { PlayerId = playerId, Player = player, GameId = gameId };
            this.unitOfWork.GamePlayerRepository.Create(gamePlayer);
            this.unitOfWork.Save();
        }

        public Game JoinToGame(Guid playerId, Game game)
        {
            var playerToAdd = this.unitOfWork.PlayerRepository.GetById(playerId);
            var gamePlayer = new GamePlayerJunction() { PlayerId = playerId, Player = playerToAdd, GameId = game.Id }; 
            
            if (game.GamesPlayers.Count == 1)
            {
                game.PlayerQueueId = game.GamesPlayers.First().PlayerId;
            }

            if (game.GamesPlayers.Count < 2)
            {
                game.GamesPlayers.Add(gamePlayer);
                game.GameStatus = GameStatus.Started;
                
                this.unitOfWork.GamePlayerRepository.Create(gamePlayer);
            }

            this.unitOfWork.Save();
            return game;
        }

        public Game MakeMove(Guid gameId, Guid fieldId, Guid fieldMovesId, Guid playerId, int index)
        {
            this.fieldService.CreateCell(gameId, fieldId, fieldMovesId, playerId, index);
            var game = this.FindGameByIdWithInclude(gameId);
            this.unitOfWork.Save();
            return game;
        }

        public Game FindGameByIdWithInclude(Guid gameId)
        {
            var game = this.unitOfWork.GameRepository.GetByIdWithInclude(
                gameId, 
                g => g.Chat.Messages, 
                g => g.Field.FieldMoves, 
                g => g.GamesPlayers);

            foreach (var gamesPlayer in game.GamesPlayers)
            {
                gamesPlayer.Player = this.unitOfWork.PlayerRepository.GetById(gamesPlayer.PlayerId);
            }

            var fieldMoves = this.unitOfWork.FieldMovesRepository.GetByIdWithInclude(game.Field.FieldMoves.Id, f => f.Cells);
            game.Field.FieldMoves = fieldMoves;

            return game;
        }

        public Game SetDraw(Guid gameId)
        {
            var game = this.FindGameByIdWithInclude(gameId);
            game.GameStatus = GameStatus.Completed;
            this.unitOfWork.Save();
            return game;
        }

        public Game SetWinner(Guid winnerId, Guid loserId, Guid gameId)
        {
            var game = this.FindGameByIdWithInclude(gameId);
            game.Winner = this.CalculateRating(winnerId, loserId);
            game.GameStatus = GameStatus.Completed;
            this.unitOfWork.Save();
            return game;
        }

        public Player CalculateRating(Guid winnerId, Guid loserId)
        {
            var winner = this.unitOfWork.PlayerRepository.GetById(winnerId);
            var loser = this.unitOfWork.PlayerRepository.GetById(loserId);
            winner.Rating += 10;
            if (loser.Rating >= 10)
            {
                loser.Rating -= 10;
            }

            return winner;
        }

        public Game CancelGame(Guid gameId)
        {
            var game = this.unitOfWork.GameRepository.GetById(gameId);
            game.GameStatus = GameStatus.Canceled;
            this.unitOfWork.Save();
            return game;
        }

        public IEnumerable<Game> GetOpenGames()
        {
            var games = this.unitOfWork.GameRepository.GetAll().Where(g => g.GameStatus == GameStatus.Pending).ToList();
            return games;
        }

        public Game SendMessage(Guid gameId, string messageBody, Guid chatId, Guid playerId)
        {
            this.chatService.CreateMessage(messageBody, chatId, playerId);
            var game = this.FindGameByIdWithInclude(gameId);
            game.Chat.Messages = game.Chat.Messages.OrderBy(m => m.DateTime).ToList();
            return game;
        }
    }
}
