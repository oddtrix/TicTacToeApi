namespace ApplicationCore.Interfaces
{
    public interface IFieldService
    {
        Guid CreateField();

        Guid CreateFieldMoves(Guid fieldId);

        (int, int) DefineCellCoordinates(int index);

        Guid CreateCell(Guid gameid, Guid fieldId, Guid fieldMovesId, Guid playerId, int index);
    }
}
