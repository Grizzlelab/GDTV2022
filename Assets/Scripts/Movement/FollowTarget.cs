using UnityEngine;

namespace Kitsuma.Movement
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 20f;
        private bool _isFollowing;

        private Transform _t;
        private Transform _target;

        private void Awake()
        {
            _t = transform;
        }

        private void Update()
        {
            if (!_isFollowing) return;
            Vector3 dir = GetDirectionToTarget();
            _t.position += dir * (moveSpeed * Time.deltaTime);
        }

        public void Follow(Transform target)
        {
            _target = target;
            _isFollowing = true;
        }

        private Vector3 GetDirectionToTarget()
        {
            return (_target.position - _t.position).normalized;
        }
    }
}