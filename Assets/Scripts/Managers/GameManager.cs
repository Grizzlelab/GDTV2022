using Kitsuma.Combat;
using Kitsuma.Entities.Shared;
using Kitsuma.Movement;
using Kitsuma.UI;
using UnityEngine;

namespace Kitsuma.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject necromancer;
        [SerializeField] private GameOverScreen gameOverScreen;
        [SerializeField] private ExperienceBar expBar;
        [SerializeField] private string wonHeader = "";
        [SerializeField] private string wonSubheader = "";
        [SerializeField] private string lostHeader = "";
        [SerializeField] private string lostSubheader = "";

        public void OnPlayerDeath(string killerTag)
        {
            if (necromancer.CompareTag(killerTag))
            {
                OnGameLost();
            }
            else
            {
                HealPlayer();
                ResetPlayerPosition();
                HealNecromancer();
            }
        }

        public void OnNecromancerDeath()
        {
            OnGameWon();
        }

        private void OnGameWon()
        {
            DisableEntities();
            expBar.gameObject.SetActive(false);
            gameOverScreen.gameObject.SetActive(true);
            gameOverScreen.SetHeader(wonHeader);
            gameOverScreen.SetSubHeader(wonSubheader);
        }

        private void OnGameLost()
        {
            DisableEntities();
            expBar.gameObject.SetActive(false);
            gameOverScreen.gameObject.SetActive(true);
            gameOverScreen.SetHeader(lostHeader);
            gameOverScreen.SetSubHeader(lostSubheader);
        }

        public void OnNewGame()
        {
            HealPlayer();
            ResetPlayerAbilities();
            ResetPlayerPosition();
            HealNecromancer();
            ResetNecromancerAbilities();
            ResetNecromancerPosition();
            EnableEntities();
            gameOverScreen.gameObject.SetActive(false);
            expBar.gameObject.SetActive(true);
        }

        private void HealPlayer()
        {
            var playerHealth = player.GetComponent<Health>();
            playerHealth.Heal(float.MaxValue);
        }

        private void ResetPlayerAbilities()
        {
            var abilities = player.GetComponent<AbilityManager>();
            abilities.ResetAllCooldowns();
        }

        private void ResetPlayerPosition()
        {
            player.transform.position = Vector3.zero;
        }

        private void HealNecromancer()
        {
            var necroHealth = necromancer.GetComponent<Health>();
            necroHealth.Heal(float.MaxValue);
        }

        private void ResetNecromancerAbilities()
        {
            var abilities = necromancer.GetComponent<AbilityManager>();
            abilities.ResetAllCooldowns();
            var controller = necromancer.GetComponent<EnemyAbilityController>();
            controller.Reset();
        }

        private void ResetNecromancerPosition()
        {
            var necroPos = necromancer.GetComponent<PlaceRandomly>();
            necroPos.SetNewRandomPosition();
        }

        private void EnableEntities()
        {
            player.SetActive(true);
            necromancer.SetActive(true);
        }
        
        private void DisableEntities()
        {
            player.SetActive(false);
            necromancer.SetActive(false);
        }
    }
}
