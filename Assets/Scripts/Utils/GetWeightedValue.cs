using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public class GetWeightedValue
    {
        public static T Get<T>(List<float> weights, List<T> options)
        {
            var total = weights.Sum();
            var random = Random.Range(0f, total);
            var current = 0f;
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