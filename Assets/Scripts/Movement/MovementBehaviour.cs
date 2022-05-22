using System;
using Kitsuma.Entities.Shared;
using UnityEngine;
using UnityEngine.Events;

namespace Kitsuma.Movement
{
    public class MovementBehaviour : MonoBehaviour
    { 
        private AnimatedBehaviour _anim;
        private Direction _dir = Direction.Down;
        
        private void Start()
        {
            _anim = GetComponent<AnimatedBehaviour>();
        }

        protected void SetAnimationByMovement(Vector2 vec)
        {
            if (vec == Vector2.zero)
            {
                if (_dir == Direction.Down) _anim.OnIdleDown();
                if (_dir == Direction.Up) _anim.OnIdleUp();
                return;
            }

            if (vec == new Vector2(0, 1))
            {
                _dir = Direction.Up;
                _anim.OnWalkUp();
            }

            if (vec == new Vector2(0, -1))
            {
                _dir = Direction.Down;
                _anim.OnWalkDown();
            }
        }
    }
}