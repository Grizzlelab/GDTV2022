using UnityEngine;

namespace Kitsuma.Combat.Ranged.Projectiles
{
    public class SimpleProjectile : Projectile
    {
        protected override void Move()
        {
            T.position += Direction * (Speed * Time.deltaTime);
        }
    }
}