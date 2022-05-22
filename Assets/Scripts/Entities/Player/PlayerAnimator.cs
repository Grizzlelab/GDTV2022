using Kitsuma.Entities.Shared;
using UnityEngine;

namespace Kitsuma.Entities.Player
{
    public class PlayerAnimator : AnimatedBehaviour
    {
        private const string Idle = "Player_Idle";
        private const string WalkDown = "Player_WalkDown";

        public void OnIdle()
        {
            SetAnimationState(Idle);
        }

        public void OnWalkDown()
        {
            SetAnimationState(WalkDown);
        }
    }
}
