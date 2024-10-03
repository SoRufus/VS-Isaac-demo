using System;
using Model.Cards;
using UnityEngine;
using Utils.Pool;
using Zenject;

namespace Model.UI
{
    public class ChooseCardWindow: UIWindow<ChooseCardData>
    {
        [Inject] private readonly GameObjectFactory _gameObjectFactory;
        
        [SerializeField] private GameObject _container;

        [Inject]
        public ChooseCardWindow(GameObjectFactory gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
        }

        private void OnEnable()
        {
            for (int i = 0; i < Data.CardsAmount; i++)
            {
                _gameObjectFactory.Create(Data.CardPrefab, _container.transform);
            }
        }
    }
}