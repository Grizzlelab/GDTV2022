using System.Collections;
using Kitsuma.Entities.Shared;
using UnityEngine;

namespace Kitsuma.Entities.Enemies
{
    public class HurtInRadius : MonoBehaviour
    {
        [SerializeField] private float damage = 2f;
        [SerializeField] private string tagToHurt;
        [SerializeField] private float radius;
        [SerializeField] private float checkTime = 0.5f;

        private Transform _t;
        private WaitForSeconds _wait;

        private void Awake()
        {
            _t = transform;
            _wait = new WaitForSeconds(checkTime);
        }

        private void Start()
        {
            StartCoroutine(HurtCoroutine());
        }

        private IEnumerator HurtCoroutine()
        {
            while (true)
            {
                yield return _wait;
                DamageAround();
            }
        }

        private void DamageAround()
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(_t.position, radius);
            foreach (Collider2D c in cols)
            {
                if (c.CompareTag(gameObject.tag)) continue;
                if (!c.gameObject.TryGetComponent(out Health health)) return;
                health.Damage(damage, tagToHurt);
            }
        }
    }
}
