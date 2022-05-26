using System;
using UnityEngine;
using UnityEngine.Events;

namespace Kitsuma.Utils
{
    public class OnCollide : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Transform> onCollide;
        [SerializeField] private string targetTag = "Player";
        [SerializeField] private bool destroyOnCollide;
        [SerializeField] private GameObject objectToDestroy;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag(targetTag)) return;
            onCollide?.Invoke(col.transform);
            if (!destroyOnCollide) return;
            Destroy(objectToDestroy);
        }
    }
}