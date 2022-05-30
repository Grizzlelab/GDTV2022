using Kitsuma.Managers;
using UnityEngine;

namespace Kitsuma.Movement
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private float speed = 7f;
        private Transform _player;

        private Transform _t;

        private void Awake()
        {
            _t = transform;
        }

        private void Start()
        {
            _player = GameManager.Instance.GetPlayer().transform;
        }

        private void Update()
        {
            if (IsAtPlayer()) return;
            Vector3 dir = GetDirectionToPlayer();
            _t.position += dir * (speed * Time.deltaTime);
        }

        private Vector3 GetDirectionToPlayer()
        {
            return (_player.position - _t.position).normalized;
        }

        private bool IsAtPlayer()
        {
            return Vector3.Distance(_player.position, _t.position) <= 0.5f;
        }
    }
}