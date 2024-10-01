using R3;
using UnityEngine;

namespace Model.Leveling
{
    public class ExperienceManager
    {
        private readonly ReactiveProperty<int> _level = new();
        private readonly ReactiveProperty<Vector2> _currentTotalExperience = new();
        
        private float _expRequiredMultiplier = 3; //TEMP
        
        public ExperienceManager()
        {

            _currentTotalExperience.Value =
                new Vector2(0, _level.Value * 10);
        }

        public void ModifyExperience(int value)
        {
            _currentTotalExperience.Value += new Vector2(value, 0);

            if (_currentTotalExperience.CurrentValue.x >= _level.Value * _expRequiredMultiplier)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _level.Value++;
            _currentTotalExperience.Value = new Vector2(0, _level.Value * _expRequiredMultiplier);
        }

        public ReadOnlyReactiveProperty<int> Level => _level;
        public ReadOnlyReactiveProperty<Vector2> CurrentTotalExperience => _currentTotalExperience;
    }
}