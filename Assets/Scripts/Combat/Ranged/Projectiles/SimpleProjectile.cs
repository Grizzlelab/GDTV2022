using Kitsuma.Entities.Shared;
using Kitsuma.Managers;
using UnityEngine;

namespace Kitsuma.Combat.Ranged.Projectiles
{
    public class SimpleProjectile : Projectile
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (GameManager.Instance.GetIsPaused()) return;
            if (col.gameObject.CompareTag(OwnerTag)) return;
            if (!col.gameObject.TryGetComponent(out Health health)) return;
            health.Damage(Damage, OwnerTag);
            if (Pierces) return;
            Release();
        }

        protected override void Move()
        {
            MoveToTarget();
        }
    }
}