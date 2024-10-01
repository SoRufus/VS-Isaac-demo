using System.Collections.Generic;
using UnityEngine;

namespace Model.Leveling
{
    [CreateAssetMenu(menuName = "ScriptableObjects/" + nameof(LevelingConfig))]
    public class LevelingConfig : ScriptableObject
    {
        [SerializeField] private List<int> _experienceRequiredPerLevel;

        public int GetExperienceRequiredToLevelUp(int levelIndex)
        {
            return levelIndex >= _experienceRequiredPerLevel.Count ? _experienceRequiredPerLevel[^1] :
                _experienceRequiredPerLevel[levelIndex];
        }
    }
}