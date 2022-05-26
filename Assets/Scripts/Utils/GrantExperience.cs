using Kitsuma.Entities.Player;
using UnityEngine;

namespace Kitsuma.Utils
{
    public class GrantExperience : MonoBehaviour
    {
        [SerializeField] private int expToGrant = 35;

        public void Grant(Transform target)
        {
            if (!target.TryGetComponent(out LevelSystem level)) return;
            level.AddExperience(expToGrant);
        }
    }
}