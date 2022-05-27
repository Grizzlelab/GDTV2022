using UnityEngine;
using UnityEngine.Events;

namespace Kitsuma.Combat.Ranged
{
    public abstract class Projectile : MonoBehaviour
    {
        // Vector3
        [SerializeField] protected UnityEvent<Vector3> onProjectileFired;
        [SerializeField] protected UnityEvent onTargetReached;

        protected Transform T;
        protected Transform Owner;
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

        public void Initialize(string ownerTag, Transform owner, Vector3 target, float damage, float speed, bool pierces)
        {
            T = transform;
            Owner = owner;
            OwnerTag = ownerTag;
            Target = target;
            Damage = damage;
            Speed = speed;
            Pierces = pierces;
            Direction = GetDirection(Target);
            Initialized = true;
            onProjectileFired?.Invoke(target);
        }

        protected void MoveToTarget()
        {
            T.position += Direction * (Speed * Time.deltaTime);
        }
        
        protected Vector3 GetDirection(Vector3 target)
        {
            return (target - T.position).normalized;
        }

        protected bool IsAtTarget()
        {
            return Vector3.Distance(Target, T.position) < Random.Range(0.1f, 1f);
        }

        protected abstract void Move();
    }
}