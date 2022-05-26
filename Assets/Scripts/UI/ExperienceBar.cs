using UnityEngine;

namespace Kitsuma.UI
{
    public class ExperienceBar : MonoBehaviour
    {
        [SerializeField] private GameObject bar;

        private RectTransform _t;

        private void Awake()
        {
            _t = bar.GetComponent<RectTransform>();
        }

        public void ChangeValue(int currentExp, int expToNext)
        {
            _t.localScale = new Vector3(
                Mathf.Clamp((float)currentExp / expToNext, 0f, 1f),
                1f, 
                1f);
        }
    }
}
