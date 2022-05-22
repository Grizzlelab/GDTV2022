using UnityEngine;

namespace Kitsuma.Movement
{
    public class PlayerMovement : MovementBehaviour
    {
        [SerializeField, Min(0f)]
        private float moveSpeed = 10f;
        
        private Rigidbody2D _rb;
        private PlayerInputActions _input;
        private Vector2 _movement;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _input = new PlayerInputActions();
            _input.Enable();
        }

        private void Update()
        {
            _movement = _input.Player.Movement.ReadValue<Vector2>();
            SetAnimationByMovement(_movement);
        }

        private void FixedUpdate()
        {
            if (_movement == Vector2.zero) return;
            _rb.MovePosition(GetMovePosition());
        }

        private Vector2 GetMovePosition()
        {
            return _rb.position + _movement * (moveSpeed * Time.fixedDeltaTime);
        }
    }
}
