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
    }
}
