using UnityEngine;

namespace Kitsuma.Movement
{
    public class FlipByDirection : MonoBehaviour
    {
        [SerializeField] private Transform transformToFlip;

        public void Flip(Vector3 dir)
        {
            transformToFlip.localScale = dir.x switch
            {
                < 0 => FlipX(1),
                > 0 => FlipX(-1),
                _ => transformToFlip.localScale
            };
        }

        private Vector3 FlipX(float x)
        {
            Vector3 scale = transformToFlip.localScale;
            return new Vector3(x, scale.y, scale.z);
        }
    }
}