using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities.Statistics;
using UnityEngine;
using Utils;

namespace Model.Upgrades
{
    [Serializable]
    public class UpgradesConfig
    {
        [SerializeField] private List<UpgradeConfig> _upgrades = new();

        [ContextMenu("Load")]
        public void LoadUpgrades()
        {
            _upgrades = GetEveryAssetOfType.Get<UpgradeConfig>().ToList();
        }

        public StatisticData GetUpgrade()
        {
            return GetWeightedValue.Get(_upgrades.Select(x => x.Chance).ToList(), _upgrades).Statistic;
        }
    }
}