using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Kitsuma.Utils
{
    public class ScreenShake : MonoBehaviour
    {
        private static ScreenShake _instance;
        public static ScreenShake Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ScreenShake>();
                }
                
                return _instance;
            }
        }

        private CinemachineBasicMultiChannelPerlin _perlin;
        private WaitForSeconds _wait;
        private bool _shaking;

        private void Start()
        {
            var cam = GetComponent<CinemachineVirtualCamera>();
            _perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void Shake(float intensity, float time)
        {
            if (_shaking) return;
            _perlin.m_AmplitudeGain = intensity;
            StartCoroutine(WaitCoroutine(time));
        }

        private IEnumerator WaitCoroutine(float time)
        {
            _wait ??= new WaitForSeconds(time);
            _shaking = true;
            yield return _wait;
            _shaking = false;
            _perlin.m_AmplitudeGain = 0f;
        }
    }
}
