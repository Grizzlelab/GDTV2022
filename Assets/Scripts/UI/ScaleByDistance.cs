using System;
using UnityEngine;

namespace Kitsuma.UI
{
    public class ScaleByDistance : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 min = new(0.5f, 0.5f, 1f);
        [SerializeField] private Vector3 max = new(1.5f, 1.5f, 1f);

        private RectTransform _t;

        private void Awake()
        { 
            _t = GetComponent<RectTransform>();
        }

        private void Update()
        {
        }
    }
}