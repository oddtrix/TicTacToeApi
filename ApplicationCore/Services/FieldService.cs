using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace ApplicationCore.Services
{
    public class FieldService : IFieldService
    {
        private readonly IUnitOfWork unitOfWork;

        public FieldService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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

            var (x, y) = this.DefineCellCoordinates(index);
            cell.X = x; cell.Y = y;

            var players = this.unitOfWork.GamePlayerRepository.GetAllByIdWithInclude(gameid, "GameId", p => p.Player).Select(p => p.Player).ToList();
            game.PlayerQueueId = players.Single(p => p.Id != playerId).Id;

            if (game.StrokeNumber % 2 == 0)
            {
                cell.Value = CellState.X;
            }
            else
            {
                cell.Value = CellState.O;
            }

            game.StrokeNumber++;
            this.unitOfWork.CellRepository.Create(cell);
            this.unitOfWork.Save();
            return cell.FieldId;
        }

        public (int, int) DefineCellCoordinates(int index)
        {
            var x = (index - 1) / 3;
            var y = (index - 1) % 3;

            return (x, y);
        }
    }
}
