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
            _inactiveAbilities.Remove(defaultAbility);
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

        public void ResetAllAbilities()
        {
            _activeAbilities = hasAllAbilities ? _abilities : new List<Ability> { defaultAbility };
            _inactiveAbilities = _abilities;
            
            foreach (Ability ability in _abilities)
            {
                ability.ResetLevels();
            }
            
            ResetAllCooldowns();
        }
        
        public void ResetAllCooldowns()
        {
            foreach (Ability ability in _abilities)
            {
                ability.ResetCooldown();
            }
        }

        public void UnlockRandomAbility()
        {
            Ability a = _inactiveAbilities[Random.Range(0, _inactiveAbilities.Count)];
            if (a == null) return;
            _inactiveAbilities.Remove(a);
            _activeAbilities.Add(a);
        }
        
        public bool HasNewAbilities() => _inactiveAbilities.Count > 0;
        public List<Ability> GetAbilities() => _abilities;
    }
}
