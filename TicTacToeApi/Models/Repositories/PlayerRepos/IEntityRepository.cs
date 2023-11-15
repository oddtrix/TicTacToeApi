namespace TicTacToeApi.Models.Repositories.PlayerRepos
{
    public interface IEntityRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(Guid id);

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(Guid id);
    }
}
