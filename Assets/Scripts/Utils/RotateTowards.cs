using UnityEngine;

namespace Kitsuma.Utils
{
    public class RotateTowards : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed = 5f;

        private Transform _t;

        private void Awake()
        {
            _t = transform;
        }

        private void Update()
        {
            transform.rotation = Quaternion.Slerp(
                _t.rotation, 
                GetRotation(), 
                speed * Time.deltaTime);
        }

        private Quaternion GetRotation()
        {
            return Quaternion.AngleAxis(GetAngle(), Vector3.forward);
        }

        private float GetAngle()
        {
            Vector2 dir = GetDirection();
            return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        }

        private Vector2 GetDirection()
        {
            return (target.position - _t.position).normalized;
        }
    }
}