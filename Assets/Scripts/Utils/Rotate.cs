using UnityEngine;

namespace Kitsuma.Utils
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            _transform.Rotate(new Vector3(0, 0, speed));
        }
    }
}