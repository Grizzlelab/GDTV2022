using System;
using UnityEngine;

namespace Kitsuma.UI
{
    public class RotateTowards : PointerBehaviour
    {
        private void Update()
        {
            Vector3 to = target.position;
            Vector3 from = Cam.transform.position;
            from.z = 0f;
            Vector3 dir = GetDirection(to, from);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;
            T.localEulerAngles = new Vector3(0f, 0f, angle);
        }
        
        private static Vector3 GetDirection(Vector3 a, Vector3 b)
        {
            return (a - b).normalized;
        }
    }
}