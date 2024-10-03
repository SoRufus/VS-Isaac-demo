using Model.Cards;
using Model.Settings;
using Model.UI;
using R3;
using UnityEngine;
using Zenject;

namespace Model.Leveling
{
    public class ExperienceManager
    {
        private readonly ExperienceConfig _experienceConfig;
        private readonly CardsConfig _cardsConfig;
        private readonly UIManager _uiManager;
        private readonly ReactiveProperty<int> _level = new();
        private readonly ReactiveProperty<Vector2> _currentTotalExperience = new();
        
        [Inject]
        public ExperienceManager(GameSettings gameSettings, UIManager uiManager)
        {
            _experienceConfig = gameSettings.ExperienceConfig;
            _cardsConfig = gameSettings.CardsConfig;
            _uiManager = uiManager;
            _currentTotalExperience.Value = new Vector2(0, _experienceConfig.GetExperienceRequiredToLevelUp(Level.CurrentValue));
            
        }

        public void ModifyExperience(int value)
        {
            _currentTotalExperience.Value += new Vector2(value, 0);
            
            if (_currentTotalExperience.CurrentValue.x >= _experienceConfig.GetExperienceRequiredToLevelUp(Level.CurrentValue))
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _level.Value++;
            _currentTotalExperience.Value = new Vector2(0, _experienceConfig.GetExperienceRequiredToLevelUp(Level.CurrentValue));
            _uiManager.ShowWindow(_cardsConfig.CardWindowPrefab, _cardsConfig.GetCardsData());
        }

        public ReadOnlyReactiveProperty<int> Level => _level;
        public ReadOnlyReactiveProperty<Vector2> CurrentTotalExperience => _currentTotalExperience;
    }
}