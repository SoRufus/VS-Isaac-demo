using Model.Entities.Player;
using Model.Upgrades;
using R3;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEngine;
using Zenject;

namespace Model.Cards
{
    public class Card: MonoBehaviour
    {
        [Inject] private readonly Player _player;

        private readonly ReactiveProperty<UpgradeConfig> _config = new();

        public event Action OnClicked;
        
        public void Setup(UpgradeConfig config)
        {
            _config.Value = config;
        }
        
        public void OnClick()
        {
            _player.GetStatisticData(_config.Value.Statistic).ModifyValue(_config.Value.OperationType,
                _config.Value.Value);
            OnClicked?.Invoke();
        }

        public ReadOnlyReactiveProperty<UpgradeConfig> Config => _config;
    }
}