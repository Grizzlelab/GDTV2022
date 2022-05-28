using Kitsuma.Entities.Shared;
using Kitsuma.Managers;
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

            if (_isReturning)
            {
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
            if (GameManager.Instance.GetIsPaused()) return;
            if (_isReturning)
            {
                if (col.CompareTag(OwnerTag))
                {
                    Release();
                    return;
                }
            }

            if (col.CompareTag(OwnerTag)) return;
            DamageCollider(col);
        }

        private void DamageCollider(Collider2D col)
        {
            if (!col.gameObject.TryGetComponent(out Health health)) return;
            health.Damage(Damage, OwnerTag);
        }
    }
}