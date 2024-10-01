using System;
using Model.Entities.Statistics;
using UnityEngine;

namespace Model.Entities.Components
{
    public class ContactDamageComponent: EntityComponent
    {
        [SerializeField] private Statistic _contactDamageStatistic;

        public event Action<Entity> OnHit;
        
        private StatisticData _contactDamageData;
        private void OnEnable()
        {
           _contactDamageData = Entity.GetStatisticData(_contactDamageStatistic);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var entity = other.GetComponentInParent<Entity>();
            if (entity == null) return;
            
            var healthComponent = entity.GetComponent<HealthComponent>();
            if (healthComponent == null) return;

            OnHit?.Invoke(entity);
            healthComponent.ModifyHealth(-_contactDamageData.Value);
        }

        public void SetContactDamage(float value)
        {
            _contactDamageData.SetValue(value);
        }
    }
}