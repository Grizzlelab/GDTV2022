using System.Collections;
using Kitsuma.Entities.Shared;
using Kitsuma.Utils;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

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
