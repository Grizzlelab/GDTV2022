using Kitsuma.Entities.Shared;
using UnityEngine;

namespace Kitsuma.Managers
{
    public class MobManager : PooledRandomSpawner
    {
        protected override void OnCreate(GameObject obj)
        {
            obj.GetComponent<Health>().Heal(float.MaxValue);
            base.OnCreate(obj);
        }
    }
}