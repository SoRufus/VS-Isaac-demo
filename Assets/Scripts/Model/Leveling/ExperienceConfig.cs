using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Leveling
{
    [Serializable]
    public class ExperienceConfig
    {
        [SerializeField] private List<int> _experienceRequiredPerLevel = new();

        public int GetExperienceRequiredToLevelUp(int levelIndex)
        {
            return levelIndex >= _experienceRequiredPerLevel.Count ? _experienceRequiredPerLevel[^1] :
                _experienceRequiredPerLevel[levelIndex];
        }
    }
}