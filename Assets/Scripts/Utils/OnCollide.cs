using System;
using UnityEngine;
using UnityEngine.Events;

namespace Kitsuma.Utils
{
    public class OnCollide : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Transform> onCollide;
        [SerializeField] private string targetTag = "Player";

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag(targetTag)) return;
            onCollide?.Invoke(col.transform);
        }
    }
}