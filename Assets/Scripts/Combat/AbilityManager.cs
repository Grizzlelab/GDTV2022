using System.Collections.Generic;
using UnityEngine;

namespace Kitsuma.Combat
{
    public class AbilityManager : MonoBehaviour
    {
        [SerializeField] private Ability defaultAbility;
        [SerializeField] private bool hasAllAbilities;
        
        private List<Ability> _abilities;
        private List<Ability> _inactiveAbilities;
        private List<Ability> _activeAbilities;

        private void Awake()
        {
            _abilities = new List<Ability>();
            _abilities.AddRange(GetComponents<Ability>());
            _inactiveAbilities = _abilities;
            _activeAbilities = hasAllAbilities ? _abilities : new List<Ability> { defaultAbility };
        }

        public void UseAbilities(Vector2 target)
        {
            foreach (Ability ability in _activeAbilities)
            {
                ability.Use(target);
            }
        }

        public void UpgradeAllAbilities()
        {
            foreach (Ability ability in _activeAbilities)
            {
                ability.Upgrade();
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
