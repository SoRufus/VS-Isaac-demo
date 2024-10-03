using Model.Cards;
using Model.Entities.Spawner;
using Model.Leveling;
using Model.Upgrades;
using UnityEngine;

namespace Model.Settings
{
    [CreateAssetMenu(menuName = "ScriptableObjects/" + nameof(GameSettings))]
    public class GameSettings: ScriptableObject
    {
        [field: SerializeField] public ExperienceConfig ExperienceConfig { get; private set; }
        [field: SerializeField] public EnemySpawnerConfig EnemySpawnerConfig { get; private set; }
        [field: SerializeField] public CardsConfig CardsConfig { get; private set; }
        [field: SerializeField] public UpgradesConfig UpgradesConfig { get; private set; }

    }
}