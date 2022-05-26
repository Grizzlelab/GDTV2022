using UnityEngine;

namespace Kitsuma.Utils
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private Transform _transform;
        private bool _rotate = true;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (!_rotate) return;
            _transform.Rotate(new Vector3(0, 0, speed));
        }

        public void StartRotating() => _rotate = true;
        public void StopRotating() => _rotate = false;
    }
}