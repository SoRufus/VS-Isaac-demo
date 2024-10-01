using System;
using Model.Leveling;
using UnityEngine;
using Zenject;
using R3;
using TMPro;
using UnityEngine.UI;

namespace View.UI
{
    public class ExperienceLabel : MonoBehaviour
    {
        [Inject] private readonly ExperienceManager _experienceManager;

        [SerializeField] private TextMeshProUGUI _levelLabel;
        [SerializeField] private Image _expFillBar;
        
        private IDisposable _disposable;
        private void OnEnable()
        {
            var builder = Disposable.CreateBuilder();
            
            _experienceManager.CurrentTotalExperience.Subscribe(OnExperienceModified).AddTo(ref builder);
            _experienceManager.Level.Subscribe(OnLevelModified).AddTo(ref builder);

            _disposable = builder.Build();
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private void OnExperienceModified(Vector2 currentTotalValue)
        {
            _expFillBar.fillAmount = currentTotalValue.x / currentTotalValue.y;
        }
        
        private void OnLevelModified(int value)
        {
            _levelLabel.text = value.ToString();
        }
    }
}
