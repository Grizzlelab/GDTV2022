using System.Collections;
using UnityEngine;

namespace Kitsuma.Movement
{
    public class WanderMovement : MovementBehaviour
    {
        [SerializeField] private float movementSpeed = 10f;
        [SerializeField] private Vector2 wanderDistance = Vector2.one * 10;
        [SerializeField] private float wanderWaitTime = 5f;
        [SerializeField] private float waitRand = 1f;
        [SerializeField] private float distanceCheck = 1f;

        private bool _canWander = true;
        private Vector3 _startPosition;
        private Vector3 _target;
        private Transform _trans;
        private WaitForSeconds _wait;

        private void Awake()
        {
            _trans = transform;
            _wait = new WaitForSeconds(Random.Range(
                wanderWaitTime - waitRand,
                wanderWaitTime + waitRand));
        }

        private void Update()
        {
            if (!_canWander) return;

            if (_startPosition == Vector3.zero)
            {
                _startPosition = _trans.position;
                _target = GetRandomWanderTarget();
            }

            Vector3 vec = GetMovementDelta();
            SetAnimationByMovement(vec);
            _trans.position += vec;
            if (!IsAtTarget()) return;
            StartCoroutine(WaitCoroutine());
        }

        private Vector3 GetMovementDelta()
        {
            return GetTargetDirection() * (movementSpeed * Time.deltaTime);
        }

        private IEnumerator WaitCoroutine()
        {
            _canWander = false;
            SetAnimationByMovement(Vector2.zero);
            yield return _wait;
            _target = GetRandomWanderTarget();
            _canWander = true;
        }

        private Vector3 GetRandomWanderTarget()
        {
            return new Vector3(
                GetRandX(),
                GetRandY(),
                _startPosition.z);
        }

        private float GetRandX()
        {
            return Random.Range(
                _startPosition.x - wanderDistance.x,
                _startPosition.x + wanderDistance.x);
        }

        private float GetRandY()
        {
            return Random.Range(
                _startPosition.y - wanderDistance.y,
                _startPosition.y + wanderDistance.y);
        }

        private Vector3 GetTargetDirection()
        {
            return (_target - _trans.position).normalized;
        }

        private bool IsAtTarget()
        {
            return Vector3.Distance(_trans.position, _target) <= distanceCheck;
        }
    }
}