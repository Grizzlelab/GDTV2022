using System;
using UnityEngine;

namespace Kitsuma.Entities.Shared
{
    public class AnimatedBehaviour : MonoBehaviour
    {
        private const string IdleDown = "_IdleDown";
        private const string IdleUp = "_IdleUp";
        private const string WalkDown = "_WalkDown";
        private const string WalkUp = "_WalkUp";

        [SerializeField] private string animationPrefix = "Player";
        
        private Animator _anim;
        private string _state;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        public void OnIdleDown()
        {
            SetAnimationState(ToAnimationString(IdleDown));
        }

        public void OnIdleUp()
        {
            SetAnimationState(ToAnimationString(IdleUp));
        }

        public void OnWalkDown()
        {
            SetAnimationState(ToAnimationString(WalkDown));
        }

        public void OnWalkUp()
        {
            SetAnimationState(ToAnimationString(WalkUp));
        }
        
        private string ToAnimationString(string s) => animationPrefix + s;
        
        private void SetAnimationState(string state)
        {
            if (string.Equals(_state, state)) return;
            _state = state;
            _anim.Play(state);
        }
    }
}