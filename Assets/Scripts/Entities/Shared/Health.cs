using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Kitsuma.Entities.Shared
{
    public class Health : MonoBehaviour
    {
        private const float HealthUpgradeAmount = 10f;

        [SerializeField] private float maxHealth = 10f;
        [SerializeField] private float currentHealth = 10f;
        [SerializeField] private bool hasInvuln;
        [SerializeField] private float invulnTime = 0.25f;
        [SerializeField] private UnityEvent<string> onDeath;

        [SerializeField] private UnityEvent onHit;

        // onHealthChanged should return a normalized version of current health
        [SerializeField] private UnityEvent<float> onHealthChanged;

        private WaitForSeconds _invulnWait;
        private bool _isInvuln;

        private void Awake()
        {
            _invulnWait = new WaitForSeconds(invulnTime);
        }

        private void OnDisable()
        {
            _isInvuln = false;
        }

        public void Heal(float amount)
        {
            currentHealth = Math.Clamp(currentHealth + amount, 0f, maxHealth);
            OnHealthChanged();
        }

        public void Damage(float amount, string attackerTag)
        {
            if (_isInvuln) return;

            if (hasInvuln) StartCoroutine(InvulnCoroutine());

            if (currentHealth == 0) return;
            currentHealth = Math.Clamp(currentHealth - amount, 0f, maxHealth);
            onHit?.Invoke();
            OnHealthChanged();
            if (currentHealth == 0) onDeath?.Invoke(attackerTag);
        }

        private IEnumerator InvulnCoroutine()
        {
            _isInvuln = true;
            yield return _invulnWait;
            _isInvuln = false;
        }

        public void ResetMaxHealth(float amount)
        {
            maxHealth = amount;
        }

        public void Upgrade()
        {
            maxHealth += HealthUpgradeAmount;
            Heal(float.MaxValue);
        }

        private void OnHealthChanged()
        {
            onHealthChanged?.Invoke(GetHealthNormalized());
        }

        private float GetHealthNormalized()
        {
            return currentHealth / maxHealth;
        }
    }
}