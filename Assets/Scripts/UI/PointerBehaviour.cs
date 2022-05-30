using UnityEngine;

namespace Kitsuma.UI
{
    public class PointerBehaviour : MonoBehaviour
    {
        [SerializeField] protected Transform target;
        protected Camera Cam;

        protected RectTransform T;

        private void Awake()
        {
            T = GetComponent<RectTransform>();
            Cam = Camera.main;
        }
    }
}