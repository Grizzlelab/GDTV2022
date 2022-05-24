using UnityEngine;

namespace Kitsuma.Utils
{
    public class NecroTracker : MonoBehaviour
    {
        [SerializeField] private Camera uiCamera;
        [SerializeField] private Transform target;
        [SerializeField] private float buffer = 100f;

        private RectTransform _t;
        private Camera _cam;

        private void Awake()
        {
            _t = GetComponent<RectTransform>();
            _cam = Camera.main;
        }

        private void Update()
        {
            RotateTowardsTarget();
            
            if (!IsTargetOffScreen()) return;
            Vector3 targetScreenPoint = _cam.WorldToScreenPoint(target.position);
            Vector3 pos = targetScreenPoint;
            if (pos.x <= buffer) pos.x = buffer;
            if (pos.x >= Screen.width - buffer) pos.x = Screen.width - buffer;
            if (pos.y <= buffer) pos.x = buffer;
            if (pos.y >= Screen.height - buffer) pos.y = Screen.height - buffer;
            Vector3 worldPos = uiCamera.ScreenToWorldPoint(pos);
            _t.position = worldPos;
            Vector3 localPosition = _t.localPosition;
            localPosition = new Vector3(localPosition.x, localPosition.y, 0f);
            _t.localPosition = localPosition;
        }

        private void RotateTowardsTarget()
        {
            Vector3 to = target.position;
            Vector3 from = _cam.transform.position;
            from.z = 0f;
            Vector3 dir = GetDirection(to, from);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;
            _t.localEulerAngles = new Vector3(0f, 0f, angle);
        }

        private Vector3 GetDirection(Vector3 a, Vector3 b)
        {
            return (a - b).normalized;
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
