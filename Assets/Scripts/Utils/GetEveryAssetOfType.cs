using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Utils
{
    public class GetEveryAssetOfType
    {
#if UNITY_EDITOR
        public static IEnumerable<T> Get<T>() where T : ScriptableObject
        {
            var assets = AssetDatabase.FindAssets("t:" + typeof(T).Name);

            foreach (var asset in assets)
            {
                var path = AssetDatabase.GUIDToAssetPath(asset);
                var scriptableObject = AssetDatabase.LoadAssetAtPath<T>(path);

                if (scriptableObject != null)
                {
                    yield return scriptableObject;
                }
            }
        }
#endif
    }
}