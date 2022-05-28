using UnityEngine;
using UnityEngine.Events;

namespace Kitsuma.Entities.Player
{
    public class LevelSystem : MonoBehaviour
    {
        [SerializeField] private int maxLevel = 99;
        [SerializeField] private int currentLevel = 1;
        // Current Experience, experience required for the next level
        [SerializeField] private UnityEvent<int, int> onExpGained;
        // Current Experience, experience required for the next level
        [SerializeField] private UnityEvent<int, int> onLevelGained;

        private int _exp;

        public void AddExperience(int exp)
        {
            if (currentLevel >= maxLevel) return;
            _exp += exp;
            onExpGained?.Invoke(_exp, GetExperienceForLevel(currentLevel + 1));
            if (GetExperienceForLevel(currentLevel + 1) > _exp) return;
            _exp = 0;
            currentLevel += 1;
            onLevelGained?.Invoke(_exp, GetExperienceForLevel(currentLevel + 1));
        }

        private int GetExperienceForLevel(int level)
        {
            if (level == maxLevel) return int.MaxValue;
            return (level + 1) * 150;
        }
    }
}