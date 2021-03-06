using System.Collections;
using Kitsuma.Entities.Shared;
using UnityEngine;
using UnityEngine.Events;

namespace Kitsuma.Combat.Ranged.Projectiles
{
    public class ExplosiveProjectile : Projectile
    {
        [SerializeField] private UnityEvent onExploded;
        [SerializeField] private UnityEvent onPulse;
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private float detonateAfter = 0.25f;
        [SerializeField] private float pulseTime = 0.1f;
        [SerializeField] private float radius = 2f;
        private WaitForSeconds _pulseWait;

        private bool _shouldMove = true;

        private void OnDisable()
        {
            _shouldMove = true;
        }

        protected override void Move()
        {
            if (!_shouldMove) return;

            if (IsAtTarget())
            {
                _shouldMove = false;
                onTargetReached?.Invoke();
                StartCoroutine(PulseCoroutine());
                StartCoroutine(DetonateCoroutine());
                return;
            }

            MoveToTarget();
        }

        private IEnumerator PulseCoroutine()
        {
            _pulseWait ??= new WaitForSeconds(pulseTime);
            while (true)
            {
                yield return _pulseWait;
                onPulse?.Invoke();
            }
        }

        private IEnumerator DetonateCoroutine()
        {
            yield return new WaitForSeconds(detonateAfter);
            onExploded?.Invoke();
            DamageAllNearby();
            SpawnExplosion();
            Release();
        }

        private void SpawnExplosion()
        {
            Instantiate(explosionPrefab, T.position, Quaternion.identity);
        }

        private void DamageAllNearby()
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(T.position, radius);

            foreach (Collider2D c in cols)
            {
                if (c.CompareTag(OwnerTag)) continue;
                if (!c.gameObject.TryGetComponent(out Health health)) return;
                health.Damage(Damage, OwnerTag);
            }
        }
    }
}