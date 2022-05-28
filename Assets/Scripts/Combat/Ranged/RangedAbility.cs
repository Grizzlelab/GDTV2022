using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Kitsuma.Combat.Ranged
{
    public class RangedAbility : Ability
    {
        private const int ProjectileCountUpgradeIncrement = 1;
        private const float SpawnWaitUpgradeDecrement = -1.1f;

        [SerializeField] private int minPool = 5;
        [SerializeField] private int maxPool = 15;
        [SerializeField] private bool pierces;
        [SerializeField] private int projectileCount = 1;
        [SerializeField] private float spawnWait = 0.05f;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private bool randomPlacement;

        private WaitForSeconds _wait;
        private ObjectPool<Projectile> _pool;

        private void Start()
        {
            _pool = new ObjectPool<Projectile>(
                () => Instantiate(projectilePrefab, transform.position, Quaternion.identity), 
                p => p.gameObject.SetActive(true),
                p => p.gameObject.SetActive(false), 
                p => Destroy(p.gameObject), 
                true, minPool, maxPool);
        }

        protected override void OnUseAbility(Vector2 target)
        {
            if (_pool.CountActive >= maxPool) return;
            StartCoroutine(SpawnProjectilesCoroutine(target));
        }

        private IEnumerator SpawnProjectilesCoroutine(Vector2 target)
        {
            _wait ??= new WaitForSeconds(spawnWait);

            for (var i = 0; i < projectileCount; i++)
            {
                CreateProjectile(target);
                yield return _wait;
            }
        }

        private void CreateProjectile(Vector2 target)
        {
            Projectile p = _pool.Get();

            p.transform.position = transform.position;
            
            if (randomPlacement)
            {
                Transform t = p.transform;
                Vector2 rand = Random.insideUnitCircle;
                t.position += new Vector3(rand.x, rand.y, 0f);
            }
            
            p.SetOnRelease(OnRelease);
            p.Initialize(Owner, T, target, damage, speed, pierces);
        }

        public override void Upgrade()
        {
            base.Upgrade();
            projectileCount += ProjectileCountUpgradeIncrement;
            spawnWait *= SpawnWaitUpgradeDecrement;
            _wait = new WaitForSeconds(spawnWait);
        }

        protected override void Downgrade()
        {
            base.Downgrade();
            projectileCount -= ProjectileCountUpgradeIncrement;
            spawnWait *= -SpawnWaitUpgradeDecrement;
            _wait = new WaitForSeconds(spawnWait);
        }

        private void OnRelease(Projectile p)
        {
            _pool.Release(p);
        }
    }
}