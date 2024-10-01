using UnityEngine;

namespace Model.Entities.Projectiles
{
    [CreateAssetMenu(menuName = "ScriptableObjects/" + nameof(WeaponConfig))]
    public class WeaponConfig: ScriptableObject
    {
        [field: SerializeField] public Entity ProjectilePrefab { get; private set; }
    }
}