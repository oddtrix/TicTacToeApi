using AutoMapper;
using TicTacToeApi.BusinessLayer.Interfaces;
using TicTacToeApi.Models.Repositories.PlayerRepos;

namespace TicTacToeApi.BusinessLayer.Services
{
    public class EntityService<TEntity, TCreateDTO, TUpdateDTO> : IEntityService<TEntity, TCreateDTO, TUpdateDTO>
/*        where TEntity : class
        where TCreateDTO : class
        where TUpdateDTO : class*/
    {
        private readonly IEntityRepository<TEntity> repository;
        private readonly IMapper mapper;

        public EntityService(IEntityRepository<TEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public TEntity Create(TCreateDTO createDTO)
        {
            var entity = this.mapper.Map<TEntity>(createDTO);
            this.repository.Create(entity);
            return entity;
        }

        public void Delete(Guid id)
        {
            this.repository.Delete(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            var entities = this.repository.GetAll();
            return entities;
        }

        public TEntity GetById(Guid id)
        {
            var entity = this.repository.GetById(id);
            return entity;
        }

        public TEntity Update(TUpdateDTO updateDTO)
        {
            var entity = mapper.Map<TEntity>(updateDTO);
            this.repository.Update(entity);
            return entity;
        }
    }
}
