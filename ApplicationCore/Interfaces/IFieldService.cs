namespace ApplicationCore.Interfaces
{
    public interface IFieldService
    {
        Guid CreateField();

        Guid CreateFieldMoves(Guid fieldId);

        Guid CreateCell(Guid gameid, Guid fieldId, Guid fieldMovesId, Guid playerId, int index);
    }
}
