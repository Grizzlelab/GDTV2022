using UnityEngine;

namespace Kitsuma.Movement
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float minDist = 1f;

        private Transform _t;

        private void Awake()
        {
            _t = transform;
        }

        private void Update()
        {
            if (IsInRange()) return;
            _t.position += GetDirectionToTarget() * (speed * Time.deltaTime);
        }

        private bool IsInRange()
        {
            return Vector3.Distance(_t.position, target.position) <= minDist;
        }
        
        private Vector3 GetDirectionToTarget()
        {
            return (target.position - _t.position).normalized;
        }
    }
}