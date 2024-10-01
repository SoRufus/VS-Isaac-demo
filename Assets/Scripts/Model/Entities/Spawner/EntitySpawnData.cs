using UnityEngine;

namespace Model.Entities.Spawner
{
    public struct EntitySpawnData
    {
        public Entity Entity { get; private set; }
        public Vector2 Position { get; private set; }
        public Transform Parent { get; private set; }
        
        public EntitySpawnData(Entity entity, Vector2 position, Transform parent = null)
        {
            Entity = entity;
            Position = position;
            Parent = parent;
        }
    }
}