using UnityEngine;

namespace Kitsuma.Entities.Shared
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Transform bar;

        public void SetFill(float fillNormalized)
        {
            bar.localScale = new Vector3(fillNormalized, 1f);
        }
    }
}
