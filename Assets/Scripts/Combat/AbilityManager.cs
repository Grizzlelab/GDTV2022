using System.Collections.Generic;
using UnityEngine;

namespace Kitsuma.Combat
{
    public class AbilityManager : MonoBehaviour
    {
        private List<Ability> _abilities;

        private void Awake()
        {
            _abilities = new List<Ability>();
            _abilities.AddRange(GetComponents<Ability>());
        }

        public void UseAbilities(Vector2 target)
        {
            foreach (Ability ability in _abilities)
            {
                ability.Use(target);
            }
        }

        public void ResetAllCooldowns()
        {
            foreach (Ability ability in _abilities)
            {
                ability.ResetCooldown();
            }
        }

        public List<Ability> GetAbilities() => _abilities;
    }
}
