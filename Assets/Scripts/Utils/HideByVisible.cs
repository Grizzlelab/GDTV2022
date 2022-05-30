using UnityEngine;

namespace Kitsuma.Utils
{
    public class HideByVisible : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private GameObject gameObjectToHide;

        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            gameObjectToHide.SetActive(IsTargetOffScreen());
        }

        private bool IsTargetOffScreen()
        {
            Vector3 pos = _cam.WorldToScreenPoint(target.position);
            return pos.x <= 0 ||
                   pos.x >= Screen.width ||
                   pos.y <= 0 ||
                   pos.y >= Screen.height;
        }
    }
}