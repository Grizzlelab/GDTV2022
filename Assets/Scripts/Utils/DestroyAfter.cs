using UnityEngine;

namespace Kitsuma.Utils
{
    public class DestroyAfter : MonoBehaviour
    {
        [SerializeField] private float destroyAfterTime = 2f;

        private void Start()
        {
            Destroy(gameObject, destroyAfterTime);
        }
    }
}