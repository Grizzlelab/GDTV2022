using System.Collections;
using Kitsuma.Combat.Ranged;
using UnityEngine;

namespace Kitsuma.Utils
{
    public class ReleaseAfter : MonoBehaviour
    {
        [SerializeField] private float time;

        private WaitForSeconds _wait;
        private Projectile _p;
        
        private void Awake()
        {
            _wait = new WaitForSeconds(time);
            _p = GetComponent<Projectile>();
        }

        private void OnEnable()
        {
            StartCoroutine(Release());
        }

        private IEnumerator Release()
        {
            yield return _wait;
            _p.Release();
        }
    }
}