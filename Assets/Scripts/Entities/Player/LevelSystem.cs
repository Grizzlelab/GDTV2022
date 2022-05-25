using UnityEngine;
using UnityEngine.Events;

namespace Kitsuma.Entities.Player
{
    public class LevelSystem : MonoBehaviour
    {
        [SerializeField] private int maxLevel = 99;
        [SerializeField] private int currentLevel = 1;
        [SerializeField] private int exp;
        // current exp, exp to next level
        [SerializeField] private UnityEvent<int, int> onExpGained;
        // level, current exp, exp to next level
        [SerializeField] private UnityEvent<int, int, int> onLevelGained;

        public void AddExperience(int expToAdd)
        {
            if (currentLevel == maxLevel) return;
            exp += expToAdd;
            int expToNext = GetExpToNextLevel();
            onExpGained?.Invoke(exp, expToNext);
            if (expToNext > exp) return;
            currentLevel += 1;
            onLevelGained?.Invoke(currentLevel, exp, expToNext);
        }

        private int GetExpToNextLevel()
        {
            if (currentLevel == maxLevel) return int.MaxValue;
            return GetExpAtLevel(currentLevel + 1) - exp;
        }

        private static int GetExpAtLevel(int level)
        {
            var total = 0f;

            for (var i = 1; i < level; i++)
            {
                total += Mathf.FloorToInt(i + 300 * Mathf.Pow(2, i / 7.0f));
            }
            
            return Mathf.FloorToInt(total / 4f);
        }
    }
}