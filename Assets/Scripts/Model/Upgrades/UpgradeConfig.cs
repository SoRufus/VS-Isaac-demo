using Model.Entities.Statistics;
using UnityEngine;
using Utils;

namespace Model.Upgrades
{
    [CreateAssetMenu(menuName = "ScriptableObjects/" + nameof(UpgradeConfig))]
    public class UpgradeConfig: ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Statistic Statistic { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
        [field: SerializeField] public OperationType OperationType { get; private set; }
        [field: SerializeField, Range(0, 100)] public float Chance { get; private set; }
    }
}