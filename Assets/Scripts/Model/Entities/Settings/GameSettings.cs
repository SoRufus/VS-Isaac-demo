using Model.Entities.Spawner;
using Model.Leveling;
using UnityEngine;

namespace Model.Entities.Settings
{
    [CreateAssetMenu(menuName = "ScriptableObjects/" + nameof(GameSettings))]
    public class GameSettings: ScriptableObject
    {
        [field: SerializeField] public ExperienceConfig ExperienceConfig { get; private set; }
        [field: SerializeField] public EnemySpawnerConfig EnemySpawnerConfig { get; private set; }
    }
}