using UnityEngine;

namespace Model.Entities
{
    public abstract class EntityComponent: MonoBehaviour
    {
        protected Entity Entity { get; private set; }
        
        protected virtual void Awake()
        {
            Entity = GetComponent<Entity>();
        }
    }
}