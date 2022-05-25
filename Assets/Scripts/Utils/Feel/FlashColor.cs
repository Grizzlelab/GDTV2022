using System;
using System.Collections;
using UnityEngine;

namespace Kitsuma.Utils.Feel
{
    public class FlashColor : MonoBehaviour
    {
        private static readonly int HighlightColor = Shader.PropertyToID("_HighlightColor");
        
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Color color = Color.white;
        [SerializeField] private float flashTime = 0.1f;

        private Color _originalColor;
        private WaitForSeconds _wait;
        private bool _flashing;

        private void Start()
        {
            _originalColor = GetMaterialColor();
        }

        private void OnDisable()
        {
            Reset();
        }

        public void Flash()
        {
            if (_flashing) return;
            SetMaterialColor();
            StartCoroutine(WaitCoroutine());
        }

        public void Reset()
        {
            RevertMaterialColor();
        }

        private IEnumerator WaitCoroutine()
        {
            _wait ??= new WaitForSeconds(flashTime);
            _flashing = true;
            yield return _wait;
            _flashing = false;
            RevertMaterialColor();
        }

        private void SetMaterialColor()
        {
            sprite.material.SetColor(HighlightColor, color);
        }

        private void RevertMaterialColor()
        {
            sprite.material.SetColor(HighlightColor, _originalColor);
        }

        private Color GetMaterialColor()
        {
            return sprite.material.GetColor(HighlightColor);
        }
    }
}
