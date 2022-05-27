using UnityEngine;

namespace Kitsuma.Utils
{
    public class ScaleOverTime : MonoBehaviour
    {
        [SerializeField] private Vector3 targetScale = Vector3.one * 2;
        [SerializeField] private float scaleSpeed = 3f;

        private Transform _t;

        private void Awake()
        {
            _t = transform;
        }

        private void Update()
        {
            _t.localScale = Vector3.Slerp(
                _t.localScale, 
                targetScale, 
                Time.deltaTime * scaleSpeed);
        }
    }
}