using UnityEngine;

namespace Kitsuma.Utils.Feel
{
    public class ChangeTime : MonoBehaviour
    {
        [SerializeField] private float timeScale = 0.25f;
        [SerializeField] private float duration = 0.1f;

        public void Change()
        {
            TimeController.Instance.ChangeTime(timeScale, duration);
        }
    }
}