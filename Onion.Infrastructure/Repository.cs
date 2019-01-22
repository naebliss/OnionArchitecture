using Onion.Core.Domain;
using Onion.DomainServices;
using System;

namespace Onion.Infrastructure
{
    public class Repository<T> : IRepository<T>
        where T : class, IEntity
    {
        DungeonContext ctx;

        public Repository()
        {
            ctx = new DungeonContext();
        }

        public T Create(T entity)
        {
            ctx.Add<T>(entity);
            ctx.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            ctx.Remove<T>(entity);
            ctx.SaveChanges();
        }

        public T Find(Guid id)
        {
            return ctx.Find<T>(id);
        }

        public T Update(T entity)
        {
            ctx.Update<T>(entity);
            ctx.SaveChanges();
            return entity;
        }
    }
}
