using System;
using System.Collections.Generic;
using Utils.Pool;

namespace Model.Entities.Spawner
{
    //wip
    public class EntityPoolManager
    {
        private readonly Dictionary<Type, EntityPool> _pools = new();
        private readonly GameObjectFactory _entityFactory;

        public EntityPoolManager(GameObjectFactory factory)
        {
            _entityFactory = factory;
        }

        public T Get<T>(EntitySpawnData data) where T : Entity
        {
            var type = data.Entity.GetType();
            if (!_pools.ContainsKey(type))
            {
                _pools[type] = new EntityPool(_entityFactory);
            }
            return (T)_pools[type].Get(data);
        }

        public void Return(Entity entity)
        {
            if (_pools.TryGetValue(entity.GetType(), out var pool))
            {
                pool.Return(entity);
            }
        }

    }
}