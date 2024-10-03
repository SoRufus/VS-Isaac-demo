using UnityEngine;

namespace Model.Entities.Waves
{
    [CreateAssetMenu(menuName = "ScriptableObjects/" + nameof(WaveConfig))]
    public class WaveConfig: ScriptableObject
    {
        [field: SerializeField] public Entity EntityPrefab { get; private set; }
        [field: SerializeField] public float Amount { get; private set; }
        [field: SerializeField] public float Frequency { get; private set; }
    }
}