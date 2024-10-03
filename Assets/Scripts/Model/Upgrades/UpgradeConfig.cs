using Model.Entities.Statistics;
using UnityEngine;

namespace Model.Upgrades
{
    [CreateAssetMenu(menuName = "ScriptableObjects/" + nameof(UpgradeConfig))]
    public class UpgradeConfig: ScriptableObject
    {
        [field: SerializeField] public StatisticData Statistic { get; private set; }
        [field: SerializeField, Range(0, 100)] public float Chance { get; private set; }
    }
}