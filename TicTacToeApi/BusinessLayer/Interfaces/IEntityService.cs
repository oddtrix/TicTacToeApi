namespace TicTacToeApi.BusinessLayer.Interfaces
{
    public interface IEntityService<TEntity, TCreateDTO, TUpdateDTO>
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(Guid id);

        TEntity Create(TCreateDTO createDTO);

        TEntity Update(TUpdateDTO updateDTO);

        void Delete(Guid id);
    }
}
