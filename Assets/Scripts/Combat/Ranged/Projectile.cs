using Kitsuma.Entities.Shared;
using UnityEngine;

namespace Kitsuma.Combat.Ranged
{
    public abstract class Projectile : MonoBehaviour
    {
        protected Transform T;
        protected Vector3 Target;
        protected string OwnerTag;
        protected float Damage;
        protected float Speed;
        protected bool Pierces;
        protected bool Initialized;
        protected Vector3 Direction;

        private void Update()
        {
            if (!Initialized) return;
            Move();
        }

        public void Initialize(string ownerTag, Vector3 target, float damage, float speed, bool pierces)
        {
            T = transform;
            OwnerTag = ownerTag;
            Target = target;
            Damage = damage;
            Speed = speed;
            Pierces = pierces;
            Direction = GetDirection(Target);
            Initialized = true;
        }

        protected Vector3 GetDirection(Vector3 target)
        {
            return (target - T.position).normalized;
        }

        protected bool IsAtTarget()
        {
            return Vector3.Distance(Target, T.position) < 1f;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag(OwnerTag)) return;
            if (!col.gameObject.TryGetComponent(out Health health)) return;
            health.Damage(Damage, OwnerTag);
            if (Pierces) return;
            Destroy(gameObject);
        }

        protected abstract void Move();
    }
}