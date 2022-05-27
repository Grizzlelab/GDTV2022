using Kitsuma.Entities.Shared;
using UnityEngine;

namespace Kitsuma.Combat.Ranged.Projectiles
{
    public class SimpleProjectile : Projectile
    {
        protected override void Move()
        {
            MoveToTarget();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(OwnerTag)) return;
            if (!col.gameObject.TryGetComponent(out Health health)) return;
            health.Damage(Damage, OwnerTag);
            if (Pierces) return;
            Destroy(gameObject);
        }
    }
}