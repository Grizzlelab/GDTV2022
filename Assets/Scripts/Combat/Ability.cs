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
        [SerializeField] protected float maxSpeed = 15f;
        [SerializeField] protected float cooldownTime = 0.25f;
        [SerializeField] protected int level = 1;

        protected Transform T;
        protected string Owner;

        private bool _onCooldown;
        private WaitForSeconds _wait;
        private float _originalDamage;
        private float _originalSpeed;
        private float _originalCooldown;

        private void Awake()
        {
            T = transform;
            Owner = gameObject.tag;
            _originalDamage = damage;
            _originalSpeed = speed;
            _originalCooldown = cooldownTime;
        }

        private void OnDisable()
        {
            _onCooldown = false;
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
            speed = Mathf.Clamp(speed * SpeedUpgradeIncrement, 0f, maxSpeed);
            cooldownTime = Mathf.Clamp(
                cooldownTime * CooldownUpgradeDecrement, 
                0.05f, 
                cooldownTime);
            _wait = new WaitForSeconds(cooldownTime);
            _onCooldown = false;
            level += 1;
        }

        public virtual void ResetLevels()
        {
            if (level <= 1) return;
            damage = _originalDamage;
            speed = _originalSpeed;
            cooldownTime = _originalCooldown;
            _wait = new WaitForSeconds(cooldownTime);
            level = 1;
            ResetCooldown();
        }

        public void ResetCooldown()
        {
            _onCooldown = false;
        }
        
        protected abstract void OnUseAbility(Vector2 target);
    }
}