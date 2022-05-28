using System;
using System.Collections;
using Kitsuma.Entities.Shared;
using UnityEngine;

namespace Kitsuma.Combat.Ranged.Projectiles
{
    public class EruptingProjectile : Projectile
    {
        [SerializeField] private string eruptAnimName = "Tentacles_Erupt";
        [SerializeField] private float damageRadius = 1f;
        [SerializeField] private float hiddenWaitTime = 0.5f;
        [SerializeField] private float visibleWaitTime = 0.5f;
        [SerializeField] private float damagePulseTime = 0.25f;
        
        private Animator _anim;
        private WaitForSeconds _hiddenWait;
        private WaitForSeconds _visibleWait;
        private WaitForSeconds _pulseTime;
        private bool _shouldMove = true;
        
        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _hiddenWait = new WaitForSeconds(hiddenWaitTime);
            _visibleWait = new WaitForSeconds(visibleWaitTime);
            _pulseTime = new WaitForSeconds(damagePulseTime);
        }

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
                StartCoroutine(HideCoroutine());
                return;
            }
            
            MoveToTarget();
        }

        private IEnumerator HideCoroutine()
        {
            yield return _hiddenWait;
            _anim.Play(eruptAnimName);
            StartCoroutine(EruptCoroutine());
            StartCoroutine(PulseCoroutine());
        }

        private IEnumerator EruptCoroutine()
        {
            yield return _visibleWait;
            Release();
        }

        private IEnumerator PulseCoroutine()
        {
            while (true)
            {
                DamageAllNearby();
                yield return _pulseTime;
            }
        }
        
        private void DamageAllNearby()
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(
                T.position, 
                damageRadius);

            foreach (Collider2D c in cols)
            {
                if (c.CompareTag(OwnerTag)) continue;
                if (!c.gameObject.TryGetComponent(out Health health)) return;
                health.Damage(Damage, OwnerTag);
            }
        }
    }
}
