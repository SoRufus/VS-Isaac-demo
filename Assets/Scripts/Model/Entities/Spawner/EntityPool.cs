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

        public Entity Get(EntitySpawnData data)
        {
            if (_pool.Count <= 0)
            {
                _pool.Enqueue(CreateNewObject(data.Entity));
            }
            
            var entity = _pool.Dequeue();
            
            entity.Spawn(data);
            entity.gameObject.SetActive(true);

            return entity;
        }

        public void Return(Entity entity)
        {
            if (!entity.gameObject.activeSelf) return;

            entity.gameObject.SetActive(false);
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