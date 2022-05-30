using UnityEngine;

namespace Kitsuma.Entities.Enemies
{
    public class DropExperience : MonoBehaviour
    {
        [SerializeField] private GameObject expOrb;
        [SerializeField] private int amountToDrop = 1;
        [SerializeField] private bool randomizedPosition = true;
        [SerializeField] private float randomRadius = 1f;

        private Transform _t;

        private void Awake()
        {
            _t = transform;
        }

        public void Drop()
        {
            for (var i = 0; i < amountToDrop; i++)
            {
                Vector3 pos = _t.position;
                if (randomizedPosition) pos += (Vector3)Random.insideUnitCircle * randomRadius;

                Instantiate(expOrb, pos, Quaternion.identity);
            }
        }
    }
}