using System;
using UnityEngine;

namespace Kitsuma.Utils
{
    public class FaceTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private Transform _t;

        private void Awake()
        {
            _t = transform;
        }

        public void Rotate(Transform t)
        {
            target = t;
            Rotate();
        }

        public void Rotate()
        {
            Rotate(target.position);
        }

        public void Rotate(Vector3 t)
        {
            Vector3 dir = GetDirection(t, _t.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;
            _t.localEulerAngles = new Vector3(0f, 0f, angle);
        }

        private static Vector3 GetDirection(Vector3 a, Vector3 b)
        {
            return (a - b).normalized;
        }
    }
}