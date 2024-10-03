using System;
using UnityEngine;

namespace Model.Cards
{
    [Serializable]
    public class CardsConfig
    {
        [field: SerializeField] public int CardsAmount { get; private set; }
        [field: SerializeField] public GameObject CardPrefab { get; private set; }
        [field: SerializeField] public GameObject CardWindowPrefab { get; private set; }

        public ChooseCardData GetCardsData()
        {
            return new ChooseCardData(CardPrefab, CardsAmount);
        }
    }
}