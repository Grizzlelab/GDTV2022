using System.Collections;
using UnityEngine;

namespace Kitsuma.Combat
{
    public class EnemyAbilityController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float minDistFromTarget = 4f;
        [SerializeField] private float cooldownTime = 0.5f;

        private AbilityManager _abilities;
        private bool _isOnCooldown;
        private bool _paused;
        private Transform _t;
        private WaitForSeconds _wait;

        private void Awake()
        {
            _abilities = GetComponent<AbilityManager>();
            _t = transform;
            _wait = new WaitForSeconds(cooldownTime);
        }

        public void Reset()
        {
            _isOnCooldown = false;
        }

        private void Update()
        {
            if (!IsNearTarget() || _isOnCooldown || _paused) return;
            _abilities.UseAbilities(target.position);
            StartCoroutine(WaitCoroutine());
        }

        private IEnumerator WaitCoroutine()
        {
            _isOnCooldown = true;
            yield return _wait;
            _isOnCooldown = false;
        }

        private bool IsNearTarget()
        {
            float dist = Vector3.Distance(target.position, _t.position);
            return dist <= minDistFromTarget;
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Unpause()
        {
            _paused = false;
        }
    }
}