using UnityEngine;
using Zenject;

namespace Utils.Pool
{
    public class GameObjectFactory
    {
        private readonly DiContainer _container;

        public GameObjectFactory(DiContainer container)
        {
            _container = container;
        }

        public GameObject Create(GameObject prefab, Transform parentTransform = null)
        {
            var newObject = _container.InstantiatePrefab(prefab, parentTransform);
            return newObject;
        }
    }
}