using System.Collections;
using UnityEngine;

namespace Kitsuma.Utils.Feel
{
    public class TimeController : MonoBehaviour
    {
        private static TimeController _instance;
        private bool _timeChanged;

        private WaitForSeconds _wait;

        public static TimeController Instance
        {
            get
            {
                if (_instance == null) _instance = FindObjectOfType<TimeController>();

                return _instance;
            }
        }

        public void ChangeTime(float timeScale, float time)
        {
            if (_timeChanged) return;
            Time.timeScale = timeScale;
            StartCoroutine(WaitCoroutine(time));
        }

        private IEnumerator WaitCoroutine(float time)
        {
            _wait ??= new WaitForSeconds(time);
            _timeChanged = true;
            yield return _wait;
            _timeChanged = false;
            Time.timeScale = 1f;
        }
    }
}