using UnityEngine;
using Random = UnityEngine.Random;

namespace Kitsuma.Movement
{
    public class PlaceRandomlyFrom : MonoBehaviour
    {
        [SerializeField] private float minDist = 15f;
        [SerializeField] private float maxDist = 25f;

        private Transform _t;

        private void Awake()
        {
            _t = transform;
        }
        
        public void PlaceRandomly(Vector3 t)
        {
            Vector3 p = new Vector3(
                            Random.value - 0.5f, 
                            Random.value - 0.5f, 
                            0f).normalized * Random.Range(minDist, maxDist);
            _t.position = t + p;
        }
    }
}