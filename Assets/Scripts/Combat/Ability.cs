using System;
using System.Collections;
using UnityEngine;

namespace Kitsuma.Combat
{
    public abstract class Ability : MonoBehaviour
    {
        private const float DamageUpgradeIncrement = 1.2f;
        private const float SpeedUpgradeIncrement = 1.2f;
        private const float CooldownUpgradeDecrement = -1.2f;
        
        [SerializeField] protected float damage = 2f;
        [SerializeField] protected float speed = 10f;
        [SerializeField] protected float cooldownTime = 0.25f;
        [SerializeField] protected int level = 1;

        protected Transform T;
        protected string Owner;

        private bool _onCooldown;
        private WaitForSeconds _wait;

        private void Awake()
        {
            T = transform;
            Owner = gameObject.tag;
        }

        public void Use(Vector2 target)
        {
            if (_onCooldown) return;
            OnUseAbility(target);
            StartCoroutine(AbilityCooldownCoroutine());
        }

        private IEnumerator AbilityCooldownCoroutine()
        {
            _wait ??= new WaitForSeconds(cooldownTime);
            _onCooldown = true;
            yield return _wait;
            _onCooldown = false;
        }

        public virtual void Upgrade()
        {
            damage *= DamageUpgradeIncrement;
            speed *= SpeedUpgradeIncrement;
            cooldownTime *= CooldownUpgradeDecrement;
            _wait = new WaitForSeconds(cooldownTime);
            _onCooldown = false;
            level++;
        }

        protected virtual void Downgrade()
        {
            damage *= -DamageUpgradeIncrement;
            speed *= -SpeedUpgradeIncrement;
            cooldownTime *= -CooldownUpgradeDecrement;
            _wait = new WaitForSeconds(cooldownTime);
            _onCooldown = false;
            level--;
        }

        public void ResetLevels()
        {
            for (int i = level; i >= 0; i--)
            {
                Downgrade();
            }
        }

        public void ResetCooldown()
        {
            _onCooldown = false;
        }
        
        protected abstract void OnUseAbility(Vector2 target);
    }
}