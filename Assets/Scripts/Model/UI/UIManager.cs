using System.Collections.Generic;
using UnityEngine;
using Utils.Pool;
using Zenject;

namespace Model.UI
{
    public class UIManager
    {
        private readonly GameObjectFactory _gameObjectFactory;

        private readonly Stack<GameObject> _uiWindows = new();

        [Inject]
        public UIManager(GameObjectFactory gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
        }
        
        public void ShowWindow<T>(GameObject obj, T data)
        {
            var window = _gameObjectFactory.Create(obj).GetComponent<UIWindow<T>>();
            window.Setup(data);
            _uiWindows.Push(window.gameObject);
            window.OnHidden += () => _uiWindows.Pop();
        }
    }
}