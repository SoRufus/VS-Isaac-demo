using Model.Cards;
using Model.Entities.Player;
using UnityEngine;
using Zenject;

namespace View.UI
{
    public class CardView: MonoBehaviour
    {
        private readonly Player _player;
        
        [Inject]
        private CardView(Player player, CardsConfig cardsConfig)
        {
            _player = player;
        }
    }
}