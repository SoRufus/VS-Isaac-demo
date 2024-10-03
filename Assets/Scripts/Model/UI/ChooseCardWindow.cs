using System.Collections.Generic;
using Model.Cards;
using Model.Settings;
using UnityEngine;
using Utils.Pool;
using Zenject;

namespace Model.UI
{
    public class ChooseCardWindow: UIWindow<ChooseCardData>
    {
        [Inject] private readonly GameObjectFactory _gameObjectFactory;
        [Inject] private readonly GameSettings _gameSettings;
        
        [SerializeField] private GameObject _container;

        private List<Card> _cards = new();

        public override void OnShow()
        {
            for (int i = 0; i < Data.CardsAmount; i++)
            {
               var card = _gameObjectFactory.Create(Data.CardPrefab, _container.transform).GetComponent<Card>();
               card.Setup(_gameSettings.UpgradesConfig.GetUpgrade());
               _cards.Add(card);
               card.OnClicked += Dispose;
            }
        }

        public override void Dispose()
        {
            foreach (var card in _cards)
            {
                card.OnClicked -= Dispose;
                Destroy(card.gameObject);
            }
            _cards.Clear();
            base.Dispose();
        }
    }
}