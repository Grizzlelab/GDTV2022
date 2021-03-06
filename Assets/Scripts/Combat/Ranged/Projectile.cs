using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Kitsuma.Combat.Ranged
{
    public abstract class Projectile : MonoBehaviour
    {
        // Vector3
        [SerializeField] protected UnityEvent<Vector3> onProjectileFired;
        [SerializeField] protected UnityEvent onTargetReached;
        protected float Damage;
        protected Vector3 Direction;
        protected bool Initialized;
        private bool IsReleased;
        protected Action<Projectile> OnRelease;
        protected Transform Owner;
        protected string OwnerTag;
        protected bool Pierces;
        protected float Speed;

        protected Transform T;
        protected Vector3 Target;

        private void Update()
        {
            if (!Initialized) return;
            Move();
        }

        private void OnEnable()
        {
            IsReleased = false;
        }

        public void Initialize(string ownerTag, Transform owner, Vector3 target, float damage, float speed,
            bool pierces)
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

        public void SetOnRelease(Action<Projectile> onRelease)
        {
            OnRelease = onRelease;
        }

        public void Release()
        {
            if (IsReleased) return;
            IsReleased = true;
            OnRelease(this);
        }

        protected abstract void Move();
    }
}