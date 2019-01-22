using Onion.Core.Domain;
using Onion.DomainServices;
using System;
using System.Collections.Generic;

namespace Onion.Infrastructure.Fake
{
    public class FakeRepository<T> : IRepository<T>
        where T : IEntity
    {
        private Dictionary<Guid, T> store = new Dictionary<Guid, T>();

        public T Create(T entity)
        {
            entity.Id = Guid.NewGuid();
            store[entity.Id] = entity;
            return entity;
        }

        public void Delete(T entity)
        {
            store.Remove(entity.Id);
        }

        public T Find(Guid id)
        {
            if (store.TryGetValue(id, out T value))
            {
                return value;
            }
            return default(T);
        }

        public T Update(T entity)
        {
            store[entity.Id] = entity;
            return entity;
        }
    }
}
