using Onion.Core.Domain;
using System;

namespace Onion.DomainServices
{
    public interface IRepository<TEntity>
        where TEntity: IEntity
    {
        TEntity Find(Guid id);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
