using UnityEngine;

namespace Kitsuma.Utils
{
    public class DestroyOn : MonoBehaviour
    {
        public void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}