using System;
using UnityEngine;
using UnityEngine.Events;

namespace Kitsuma.Entities.Shared
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 10f;
        [SerializeField] private float currentHealth = 10f;
        [SerializeField] private UnityEvent onDeath;
        [SerializeField] private UnityEvent onHit;

        public void Heal(float amount)
        {
            Math.Clamp(currentHealth + amount, 0f, maxHealth);
        }

        public void Damage(float amount)
        {
            if (currentHealth == 0) return;
            Math.Clamp(currentHealth - amount, 0f, maxHealth);
            onHit?.Invoke();
            if (currentHealth == 0) onDeath?.Invoke();
        }
    }
}
