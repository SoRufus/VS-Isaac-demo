using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace View.Entities
{
    public class EntityRandomColor : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void OnEnable()
        {
            SetRandomColor();
        }

        private void SetRandomColor()
        {
            var hue = Random.Range(0f, 1f);
            var saturation = Random.Range(0.5f, 1f);
            var value = Random.Range(0.5f, 1f);

            Color randomColor = Color.HSVToRGB(hue, saturation, value);

            _spriteRenderer.color = randomColor;
        }
    }
}
