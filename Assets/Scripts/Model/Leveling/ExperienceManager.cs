using Model.Entities.Settings;
using R3;
using UnityEngine;
using Zenject;

namespace Model.Leveling
{
    public class ExperienceManager
    {
        private readonly ExperienceConfig _config;
        private readonly ReactiveProperty<int> _level = new();
        private readonly ReactiveProperty<Vector2> _currentTotalExperience = new();
        
        [Inject]
        public ExperienceManager(GameSettings gameSettings)
        {
            _config = gameSettings.ExperienceConfig;
            _currentTotalExperience.Value = new Vector2(0, _config.GetExperienceRequiredToLevelUp(Level.CurrentValue));
        }

        public void ModifyExperience(int value)
        {
            _currentTotalExperience.Value += new Vector2(value, 0);
            
            if (_currentTotalExperience.CurrentValue.x >= _config.GetExperienceRequiredToLevelUp(Level.CurrentValue))
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _level.Value++;
            _currentTotalExperience.Value = new Vector2(0, _config.GetExperienceRequiredToLevelUp(Level.CurrentValue));
        }

        public ReadOnlyReactiveProperty<int> Level => _level;
        public ReadOnlyReactiveProperty<Vector2> CurrentTotalExperience => _currentTotalExperience;
    }
}