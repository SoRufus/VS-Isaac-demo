using UnityEngine;

namespace Model.Entities.Projectiles
{
    public struct ProjectileData
    {
        public float ShootingSpeed { get; private set; }
        public float Damage { get; private set; }
        public Vector2 Direction { get; private set; }
        public float LifeTime { get; private set; }
        public float KnockBackStrength { get; private set; }
        public float KnockBackDuration { get; private set; }

        public ProjectileData(float shootingSpeed, float damage, float lifeTime, Vector2 direction,
            float knockBackStrength, float knockBackDuration)

        {
            ShootingSpeed = shootingSpeed;
            Damage = damage;
            Direction = direction;
            LifeTime = lifeTime;
            KnockBackStrength = knockBackStrength;
            KnockBackDuration = knockBackDuration;
        }
    }
}