using System;
using Kitsuma.Movement;
using UnityEngine;

namespace Kitsuma.Utils
{
    public class FollowWithinRange : MonoBehaviour
    {
        [SerializeField] private FollowTarget follow;
        [SerializeField] private string targetTag = "Player";

        private bool _hasSetFollow;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag(targetTag) || _hasSetFollow) return;
            follow.Follow(col.transform);
            _hasSetFollow = true;
        }
    }
}