using System.Collections;
using UnityEngine;

namespace Kitsuma.Combat.Ranged
{
    public class RangedAbility : Ability
    {
        private const int ProjectileCountUpgradeIncrement = 1;
        private const float SpawnWaitUpgradeDecrement = -1.1f;
        
        [SerializeField] private int projectileCount = 1;
        [SerializeField] private float spawnWait = 0.05f;
        [SerializeField] private Projectile projectilePrefab;

        private WaitForSeconds _wait;

        protected override void OnUseAbility(Vector2 target)
        {
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
            Projectile p = Instantiate(
                projectilePrefab, 
                transform.position, 
                Quaternion.identity);
            p.Initialize(target, damage, speed);
        }

        public override void Upgrade()
        {
            base.Upgrade();
            projectileCount += ProjectileCountUpgradeIncrement;
            spawnWait *= SpawnWaitUpgradeDecrement;
            _wait = new WaitForSeconds(spawnWait);
        }
    }
}