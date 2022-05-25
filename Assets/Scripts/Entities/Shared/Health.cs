using System;
using UnityEngine;
using UnityEngine.Events;

namespace Kitsuma.Entities.Shared
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 10f;
        [SerializeField] private float currentHealth = 10f;
        [SerializeField] private UnityEvent<string> onDeath;
        [SerializeField] private UnityEvent onHit;
        // onHealthChanged should return a normalized version of current health
        [SerializeField] private UnityEvent<float> onHealthChanged;

        public void Heal(float amount)
        {
            currentHealth = Math.Clamp(currentHealth + amount, 0f, maxHealth);
            OnHealthChanged();
        }

        public void Damage(float amount, string attacker)
        {
            if (currentHealth == 0) return;
            currentHealth = Math.Clamp(currentHealth - amount, 0f, maxHealth);
            onHit?.Invoke();
            OnHealthChanged();
            if (currentHealth == 0) onDeath?.Invoke(attacker);
        }
        
        private void OnHealthChanged()
        {
            onHealthChanged?.Invoke(GetHealthNormalized());
        }

        private float GetHealthNormalized() => currentHealth / maxHealth;
    }
}
