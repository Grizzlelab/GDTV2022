using Kitsuma.Entities.Shared;
using UnityEngine;

namespace Kitsuma.Combat.Ranged.Projectiles
{
    public class ReturningProjectile : Projectile
    {
        private bool _isReturning;
        
        protected override void Move()
        {
            if (IsAtTarget() && !_isReturning)
            {
                _isReturning = true;
                SetReturnDirection();
            }

            T.position += Direction * (Speed * Time.deltaTime);
        }

        private void SetReturnDirection()
        {
            Direction = GetDirection(Owner.position);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_isReturning)
            {
                if (!col.CompareTag(OwnerTag)) return;
                Destroy(gameObject);
                return;
            }

            if (col.CompareTag(OwnerTag)) return;
            if (!col.gameObject.TryGetComponent(out Health health)) return;
            health.Damage(Damage, OwnerTag);
        }
    }
}