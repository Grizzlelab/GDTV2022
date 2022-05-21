using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kitsuma.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField, Min(0f)]
        private float movementSpeed = 10f;
        
        private Rigidbody2D _rb;
        private PlayerInputActions _input;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _input = new PlayerInputActions();
            _input.Enable();
        }

        private void Update()
        {
            var vec = _input.Player.Movement.ReadValue<Vector2>();
            _rb.velocity = vec * (movementSpeed * Time.deltaTime);
        }
        
    }
}
