using UnityEngine;

namespace Kitsuma.UI
{
    public class PointerBehaviour : MonoBehaviour
    {
        [SerializeField] protected Transform target;

        protected RectTransform T;
        protected Camera Cam;

        private void Awake()
        {
            T = GetComponent<RectTransform>();
            Cam = Camera.main;
        }
    }
}