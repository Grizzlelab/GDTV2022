using System.Collections;
using UnityEngine;

namespace Kitsuma.Combat
{
    public class RandomAbilityController : MonoBehaviour
    {
        [SerializeField] private float minRandTime = 5f;
        [SerializeField] private float maxRandTime = 10f;
        private AbilityManager _abilities;
        private bool _isOnCooldown;

        private Transform _t;

        private void Awake()
        {
            _t = transform;
            _abilities = GetComponent<AbilityManager>();
        }

        private void Update()
        {
            if (_isOnCooldown) return;
            _abilities.UseAbilities(_t.position + (Vector3)Random.insideUnitCircle);
            StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            _isOnCooldown = true;
            yield return new WaitForSeconds(Random.Range(minRandTime, maxRandTime));
            _isOnCooldown = false;
        }
    }
}