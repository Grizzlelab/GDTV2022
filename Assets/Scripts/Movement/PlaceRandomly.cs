using UnityEngine;
using Random = UnityEngine.Random;

namespace Kitsuma.Movement
{
    public class PlaceRandomly : MonoBehaviour
    {
        [SerializeField] private float minDistFromCenter = 100f;
        [SerializeField] private float maxDistFromCenter = 150f;

        private void Start()
        {
            transform.position = GetRandomPosition();
        }
        
        private Vector3 GetRandomPosition()
        {
            return new Vector3(
                Random.Range(minDistFromCenter, maxDistFromCenter), 
                Random.Range(minDistFromCenter, maxDistFromCenter),
                transform.position.z);
        }
    }
}