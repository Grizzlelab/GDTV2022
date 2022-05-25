using UnityEngine;

namespace Kitsuma.Utils.Feel
{
    public class ScreenShake : MonoBehaviour
    {
        [SerializeField] private float intensity = 1f;
        [SerializeField] private float time = 0.10f;

        public void Shake()
        {
            ScreenShakeController.Instance.Shake(intensity, time);
        }
    }
}