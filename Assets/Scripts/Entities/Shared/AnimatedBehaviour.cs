using System;
using UnityEngine;

namespace Kitsuma.Entities.Shared
{
    public class AnimatedBehaviour : MonoBehaviour
    {
        private Animator _anim;
        private string _state;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        protected void SetAnimationState(string state)
        {
            if (string.Equals(_state, state)) return;
            _state = state;
            _anim.Play(state);
        }
    }
}