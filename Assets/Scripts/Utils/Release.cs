using System;
using UnityEngine;

namespace Kitsuma.Utils
{
    public class Release : MonoBehaviour
    {
        private Action<GameObject> _onRelease;

        public void SetOnRelease(Action<GameObject> onRelease)
        {
            _onRelease = onRelease;
        }

        public void OnRelease() => _onRelease(gameObject);
    }
}