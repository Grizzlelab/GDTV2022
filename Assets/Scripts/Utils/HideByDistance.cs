using UnityEngine;

namespace Kitsuma.Utils
{
    public class HideByDistance : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private GameObject objectToHide;
        [SerializeField] private float dist = 5f;

        private Transform _t;

        private void Awake()
        {
            _t = transform;
        }

        private void Update()
        {
            objectToHide.SetActive(!IsInRange());
        }

        private bool IsInRange()
        {
            return Vector3.Distance(target.position, _t.position) <= dist;
        }
    }
}