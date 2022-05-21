using System;
using UnityEngine;

namespace Kitsuma.Entities.Shared
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 10f;
        [SerializeField] private float currentHealth = 10f;

        public void Heal(float amount)
        {
            Math.Clamp(currentHealth + amount, 0f, maxHealth);
        }

        public void Damage(float amount)
        {
            Math.Clamp(currentHealth - amount, 0f, maxHealth);
        }
    }
}
