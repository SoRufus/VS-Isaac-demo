using UnityEngine;

namespace Model.Cards
{
    public struct ChooseCardData
    {
        public int CardsAmount { get; private set; }
        public GameObject CardPrefab { get; private set; }

        public ChooseCardData(GameObject cardPrefab, int cardsAmount)
        {
            CardsAmount = cardsAmount;
            CardPrefab = cardPrefab;
        }
    }
}