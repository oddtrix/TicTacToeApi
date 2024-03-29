using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace ApplicationCore.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Game CreateGame(Game game)
        {
            game.ChatId = this.CreateChat();

            game.FieldId = this.CreateField();
            var field = this.unitOfWork.FieldRepository.GetById(game.FieldId);
            game.Field = field;
            var fieldMovesId = this.CreateFieldMoves(game.FieldId);
            var fieldMoves = this.unitOfWork.FieldMovesRepository.GetById(fieldMovesId);
            game.Field.FieldMoves = fieldMoves;
            fieldMoves.FieldId = game.FieldId;

            game.GameStatus = GameStatus.Pending;
            game.IsPrivate = false;
            this.unitOfWork.GameRepository.Create(game);

            this.unitOfWork.Save();
            return game;
        }

        public Guid CreateChat()
        {
            var chat = new Chat() { Id = Guid.NewGuid() };
            this.unitOfWork.ChatRepository.Create(chat);
            this.unitOfWork.Save();
            return chat.Id;
        }

        public Guid CreateField()
        {
            var field = new Field() { Id = Guid.NewGuid() };
            this.unitOfWork.FieldRepository.Create(field);
            this.unitOfWork.Save();
            return field.Id;
        }

        public Guid CreateFieldMoves(Guid fieldId)
        {
            var fieldMoves = new FieldMoves() { Id = Guid.NewGuid() };
            fieldMoves.FieldId = fieldId;
            this.unitOfWork.FieldMovesRepository.Create(fieldMoves);
            this.unitOfWork.Save();
            return fieldMoves.Id;
        }

        public Guid CreateCell(Guid gameid, Guid fieldId, Guid fieldMovesId, Guid playerId, int index)
        {
            var game = this.unitOfWork.GameRepository.GetById(gameid);
            var cell = new Cell() { Id = Guid.NewGuid(), FieldId = fieldId, PlayerId = playerId };
            var fieldMoves = this.unitOfWork.FieldMovesRepository.GetByIdWithInclude(fieldMovesId, f => f.Cells);
            fieldMoves.Cells.Add(cell);

            var x = (index - 1) / 3;
            var y = (index - 1) % 3;
            cell.X = x; cell.Y = y;

            var players = this.unitOfWork.GamePlayerRepository.GetAll().Where(p => p.GameId == gameid).ToArray();
            if (game.StrokeNumber % 2 == 0)
            {
                game.PlayerQueueId = players[1].PlayerId;
                cell.Value = CellState.X;
            }
            else 
            {
                game.PlayerQueueId = players[0].PlayerId;
                cell.Value = CellState.O;
            } 

            game.StrokeNumber++;

            this.unitOfWork.CellRepository.Create(cell);
            this.unitOfWork.Save();
            return cell.FieldId;
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
            this.CreateCell(gameId, fieldId, fieldMovesId, playerId, index);
            var game = this.FindGameById(gameId);
            var fieldMoves = this.unitOfWork.FieldMovesRepository.GetByIdWithInclude(fieldMovesId, f => f.Cells);
            game.Field.FieldMoves = fieldMoves;
            this.unitOfWork.Save();
            return game;
        }

        public Game FindGameById(Guid gameId)
        {
            var game = this.unitOfWork.GameRepository.GetByIdWithInclude(gameId, g => g.Chat, g => g.Field.FieldMoves, g => g.GamesPlayers);

            foreach (var gamesPlayer in game.GamesPlayers)
            {
                gamesPlayer.Player = this.unitOfWork.PlayerRepository.GetById(gamesPlayer.PlayerId);
            }

            return game;
        }

        public Game SetDraw(Guid gameId)
        {
            var game = this.FindGameById(gameId);
            var fieldMoves = this.unitOfWork.FieldMovesRepository.GetByIdWithInclude(game.Field.FieldMoves.Id, f => f.Cells);

            game.Field.FieldMoves = fieldMoves;
            game.GameStatus = GameStatus.Completed;

            this.unitOfWork.GameRepository.Update(game);

            this.unitOfWork.Save();
            return game;
        }

        public Game SetWinner(Guid winnerId, Guid loserId, Guid gameId)
        {
            var game = this.FindGameById(gameId);
            var winner = this.unitOfWork.PlayerRepository.GetById(winnerId);
            var loser = this.unitOfWork.PlayerRepository.GetById(loserId);

            var fieldMoves = this.unitOfWork.FieldMovesRepository.GetByIdWithInclude(game.Field.FieldMoves.Id, f => f.Cells);

            game.Field.FieldMoves = fieldMoves;
            game.Winner = winner;
            game.GameStatus = GameStatus.Completed;
            winner.Rating += 10;
            loser.Rating -= 10;

            this.unitOfWork.PlayerRepository.Update(winner);
            this.unitOfWork.PlayerRepository.Update(loser);
            this.unitOfWork.GameRepository.Update(game);

            this.unitOfWork.Save();
            return game;
        }

        public Game CancelGame(Guid gameId)
        {
            var game = this.unitOfWork.GameRepository.GetById(gameId);
            game.GameStatus = GameStatus.Canceled;
            this.unitOfWork.GameRepository.Update(game);
            this.unitOfWork.Save();
            return game;
        }

        public IEnumerable<Game> GetOpenGames()
        {
            var games = this.unitOfWork.GameRepository.GetAll().Where(g => g.GameStatus == GameStatus.Pending);
            return games;
        }
    }
}
