using UnityEngine;
using Random = UnityEngine.Random;

namespace Kitsuma.Movement
{
    public class PlaceRandomly : MonoBehaviour
    {
        [SerializeField] private float distFromCenter = 100f;

        private void Start()
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