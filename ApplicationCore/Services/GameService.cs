using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using Domain.Entities;

namespace ApplicationCore.Services
{
    public class GameService : IGameService
    {
        private readonly IEntityService<Game> gameRepository;
        private readonly IEntityService<Cell> cellRepository;
        private readonly IEntityService<Chat> chatRepository;
        private readonly IEntityService<Field> fieldRepository;
        private readonly IEntityService<Player> playerRepository;
        private readonly IEntityService<FieldMoves> fieldMovesRepository;
        private readonly IEntityService<GamePlayerJunction> gamePlayerRepository;

        public GameService(
            IEntityService<Game> gameRepository,
            IEntityService<Cell> cellRepository,
            IEntityService<Chat> chatRepository,
            IEntityService<Field> fieldRepository,
            IEntityService<Player> playerRepository,
            IEntityService<FieldMoves> fieldMovesRepository,
            IEntityService<GamePlayerJunction> gamePlayerRepository)
        {
            this.gameRepository = gameRepository;
            this.cellRepository = cellRepository;
            this.chatRepository = chatRepository;
            this.fieldRepository = fieldRepository;
            this.playerRepository = playerRepository;
            this.fieldMovesRepository = fieldMovesRepository;
            this.gamePlayerRepository = gamePlayerRepository;
        }

        public Game CreateGame(Game game)
        {
            game.ChatId = this.CreateChat();

            game.FieldId = this.CreateField();
            var field = this.fieldRepository.GetById(game.FieldId);
            game.Field = field;
            var fieldMovesId = this.CreateFieldMoves(game.FieldId);
            var fieldMoves = this.fieldMovesRepository.GetById(fieldMovesId);
            game.Field.FieldMoves = fieldMoves;
            fieldMoves.FieldId = game.FieldId;

            game.GameStatus = GameStatus.Pending;
            game.IsPrivate = false;
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

        public Guid CreateFieldMoves(Guid fieldId)
        {
            var fieldMoves = new FieldMoves() { Id = Guid.NewGuid() };
            fieldMoves.FieldId = fieldId;
            this.fieldMovesRepository.Create(fieldMoves);
            return fieldMoves.Id;
        }

        public Guid CreateCell(Guid gameid, Guid fieldId, Guid fieldMovesId, Guid playerId, int index)
        {
            var game = this.gameRepository.GetById(gameid);
            var cell = new Cell() { Id = Guid.NewGuid(), FieldId = fieldId, PlayerId = playerId };
            var fieldMoves = this.fieldMovesRepository.GetByIdWithInclude(fieldMovesId, f => f.Cells);
            fieldMoves.Cells.Add(cell);

            var x = (index - 1) / 3;
            var y = (index - 1) % 3;
            cell.X = x; cell.Y = y;

            var players = this.gamePlayerRepository.GetAll().Where(p => p.GameId == gameid).ToArray();
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

            this.cellRepository.Create(cell);
            return cell.FieldId;
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
            
            if (game.GamesPlayers.Count == 1)
            {
                game.PlayerQueueId = game.GamesPlayers.First().PlayerId;
            }

            if (game.GamesPlayers.Count < 2)
            {
                game.GamesPlayers.Add(gamePlayer);
                game.GameStatus = GameStatus.Started;
                
                this.gamePlayerRepository.Create(gamePlayer);
            }

            return game;
        }

        public Game MakeMove(Guid gameId, Guid fieldId, Guid fieldMovesId, Guid playerId, int index)
        {
            this.CreateCell(gameId, fieldId, fieldMovesId, playerId, index);
            var game = this.FindGameById(gameId);
            var fieldMoves = this.fieldMovesRepository.GetByIdWithInclude(fieldMovesId, f => f.Cells);
            game.Field.FieldMoves = fieldMoves;
            return game;
        }

        public Game FindGameById(Guid gameId)
        {
            var game = this.gameRepository.GetByIdWithInclude(gameId, g => g.Chat, g => g.Field.FieldMoves, g => g.GamesPlayers);

            foreach (var gamesPlayer in game.GamesPlayers)
            {
                gamesPlayer.Player = this.playerRepository.GetById(gamesPlayer.PlayerId);
            }

            return game;
        }

        public Game SetDraw(Guid gameId)
        {
            var game = this.FindGameById(gameId);
            var fieldMoves = this.fieldMovesRepository.GetByIdWithInclude(game.Field.FieldMoves.Id, f => f.Cells);

            game.Field.FieldMoves = fieldMoves;
            game.GameStatus = GameStatus.Completed;

            this.gameRepository.Update(game);

            return game;
        }

        public Game SetWinner(Guid winnerId, Guid loserId, Guid gameId)
        {
            var game = this.FindGameById(gameId);
            var winner = this.playerRepository.GetById(winnerId);
            var loser = this.playerRepository.GetById(loserId);

            var fieldMoves = this.fieldMovesRepository.GetByIdWithInclude(game.Field.FieldMoves.Id, f => f.Cells);

            game.Field.FieldMoves = fieldMoves;
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
