using Kitsuma.Combat;
using Kitsuma.Entities.Shared;
using Kitsuma.Movement;
using Kitsuma.UI;
using UnityEngine;

namespace Kitsuma.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                }
                
                return _instance;
            }
        }

        [SerializeField] private GameObject player;
        [SerializeField] private GameObject necromancer;
        [SerializeField] private GameOverScreen gameOverScreen;
        [SerializeField] private ExperienceBar expBar;
        [SerializeField] private GameObject levelUpScreen;
        [SerializeField] private string wonHeader = "";
        [SerializeField] private string wonSubheader = "";
        [SerializeField] private string lostHeader = "";
        [SerializeField] private string lostSubheader = "";

        private bool _isPaused;
        
        public void OnLevelUp()
        {
            levelUpScreen.SetActive(true);
            Pause();
        }
        
        public void OnUpgradeHealth()
        {
            var playerHealth = player.GetComponent<Health>();
            playerHealth.Upgrade();
            levelUpScreen.SetActive(false);
            Unpause();
        }

        public void UpgradeAbilities()
        {
            var playerAbilities = player.GetComponent<AbilityManager>();
            playerAbilities.UpgradeAllAbilities();
            levelUpScreen.SetActive(false);
            Unpause();
        }
        
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
            playerHealth.ResetMaxHealth(10f);
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

        public void Pause()
        {
            _isPaused = true;
            Time.timeScale = 0;
            player.GetComponent<PlayerAbilityController>().Pause();
            necromancer.GetComponent<EnemyAbilityController>().Pause();
        }

        public void Unpause()
        {
            _isPaused = false;
            Time.timeScale = 1f;
            player.GetComponent<PlayerAbilityController>().Unpause();
            necromancer.GetComponent<EnemyAbilityController>().Unpause();
        }

        public bool GetIsPaused() => _isPaused;
        public GameObject GetPlayer() => player;
        public GameObject GetNecro() => necromancer;
    }
}
