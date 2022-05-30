using UnityEngine;

namespace Kitsuma.Movement
{
    public class PlaceRandomly : MonoBehaviour
    {
        [SerializeField] private float distFromCenter = 100f;

        private void Start()
        {
            SetNewRandomPosition();
        }

        public void SetNewRandomPosition()
        {
            transform.position = GetRandomPosition();
        }

        private Vector3 GetRandomPosition()
        {
            return new Vector3(
                Random.Range(-distFromCenter, distFromCenter),
                Random.Range(-distFromCenter, distFromCenter),
                transform.position.z);
        }
    }
}