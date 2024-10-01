using System.Collections.Generic;
using UnityEngine;

namespace Model.Leveling
{
    [CreateAssetMenu(menuName = "ScriptableObjects/" + nameof(ExperienceConfig))]
    public class ExperienceConfig : ScriptableObject
    {
        [SerializeField] private List<int> _experienceRequiredPerLevel = new();

        public int GetExperienceRequiredToLevelUp(int levelIndex)
        {
            return levelIndex >= _experienceRequiredPerLevel.Count ? _experienceRequiredPerLevel[^1] :
                _experienceRequiredPerLevel[levelIndex];
        }
    }
}