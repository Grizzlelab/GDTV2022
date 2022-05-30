using UnityEngine;

namespace Kitsuma.Entities.Shared
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Transform bar;
        [SerializeField] private Transform background;
        [SerializeField] private Transform border;

        [SerializeField] private bool hideOnFull;

        private void Start()
        {
            if (hideOnFull) Hide();
        }

        public void SetFill(float fillNormalized)
        {
            bar.localScale = new Vector3(fillNormalized, 1f);
            if (hideOnFull) Hide();
        }

        private void Hide()
        {
            Vector3 localScale = bar.localScale;
            bar.gameObject.SetActive(localScale.x < 1f);
            background.gameObject.SetActive(localScale.x < 1f);
            border.gameObject.SetActive(localScale.x < 1f);
        }
    }
}