using UnityEngine;

namespace Kitsuma.UI
{
    public class DeathUpgradeScreen : MonoBehaviour
    {
        [SerializeField] private GameObject newAbilitiesButton;
        [SerializeField] private GameObject upgradeAbilitiesButton;

        public void Show(bool hasNewAbilities)
        { 
            newAbilitiesButton.SetActive(hasNewAbilities);
            upgradeAbilitiesButton.SetActive(!hasNewAbilities);
        }
    }
}