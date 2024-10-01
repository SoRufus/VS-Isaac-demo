using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public class WeightedValue
    {
        public static T GetWeightedValue<T>(List<float> weights, List<T> options)
        {
            float total = weights.Sum();
            float random = UnityEngine.Random.Range(0f, total);
            float current = 0f;
            for (int i = 0; i < weights.Count; i++)
            {
                current += weights[i];
                if (random < current)
                {
                    return options[i];
                }
            }
    
            Debug.LogError("No index found");
            return default;
        }
    }
}