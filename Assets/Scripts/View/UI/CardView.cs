using System;
using Model.Cards;
using Model.Upgrades;
using TMPro;
using UnityEngine;
using R3;

namespace View.UI
{
    public class CardView: MonoBehaviour
    {
        [SerializeField] private Card _card;
        [SerializeField] private TextMeshProUGUI _label;

        private IDisposable _disposable;
        
        private void OnEnable()
        {
            _disposable = _card.Config.Subscribe(SetLabel);
        }
        
        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private void SetLabel(UpgradeConfig config)
        {
            if (config == null) return;
            
            _label.text = $"{config.Name}+";
        }
    }
}