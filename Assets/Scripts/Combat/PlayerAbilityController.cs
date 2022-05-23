using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kitsuma.Combat
{
    public class PlayerAbilityController : MonoBehaviour
    {
        private AbilityManager _abilities;
        private Camera _camera;
        private PlayerInputActions _input;

        private void Awake()
        {
            _abilities = GetComponent<AbilityManager>();
            _camera = Camera.main;
            _input = new PlayerInputActions();
            _input.Enable();
        }

        private void Update()
        {
            if (!_input.Player.Attack.triggered) return;
            Vector2 mousePos = Mouse.current.position.ReadValue(); 
            mousePos = _camera.ScreenToWorldPoint(mousePos);
            _abilities.UseAbilities(mousePos);
        }
    }
}