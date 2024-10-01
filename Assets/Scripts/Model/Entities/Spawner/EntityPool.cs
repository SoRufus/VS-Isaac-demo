using System.Collections.Generic;
using Utils.Pool;

namespace Model.Entities.Spawner
{
    public class EntityPool
    {
        private readonly GameObjectFactory _entityFactory;
        private readonly Queue<Entity> _pool = new();
        
        public EntityPool(GameObjectFactory factory)
        {
            _entityFactory = factory;
        }
        
        public EntityPool(GameObjectFactory factory, EntitySpawnData data, int initialSize = 0)
        {
            _entityFactory = factory;
            
            for (int i = 0; i < initialSize; i++)
            { 
                CreateNewObject(data.Entity);
            }
        }

        public Entity Get(EntitySpawnData data)
        {
            if (_pool.Count == 0)
            {
                _pool.Enqueue(CreateNewObject(data.Entity));
            }
            
            var entity = _pool.Dequeue();
            
            entity.gameObject.SetActive(true);
            entity.OnSpawned(data);

            return entity;
        }

        public void Return(Entity entity)
        {
            entity.gameObject.SetActive(false);
            entity.OnDespawned();
            _pool.Enqueue(entity);
        }
        
        private Entity CreateNewObject(Entity entity)
        {
            var obj = _entityFactory.Create(entity.gameObject);
            var newEntity = obj.GetComponent<Entity>();
            obj.SetActive(false);
            
            return newEntity;
        }
    }
}