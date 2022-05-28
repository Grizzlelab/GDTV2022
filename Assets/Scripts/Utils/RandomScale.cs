using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kitsuma.Utils
{
    public class RandomScale : MonoBehaviour
    {
        [SerializeField] private Vector3 minScale = Vector3.one;
        [SerializeField] private Vector3 maxScale = Vector3.one * 3;

        private Transform _t;

        private void Awake()
        {
            _t = transform;
        }

        private void Start()
        {
            SetRandomScale();
        }

        private void OnDisable()
        {
            SetRandomScale();
        }
        
        private void SetRandomScale()
        {
            _t.localScale = new Vector3(
                Random.Range(minScale.x, maxScale.x),
                Random.Range(minScale.y, maxScale.y),
                Random.Range(minScale.z, maxScale.z));
        }
    }
}