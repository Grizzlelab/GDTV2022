using UnityEngine;

namespace Kitsuma.Utils.Debug
{
    public class Scaler : MonoBehaviour
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void SetScale(float scale)
        {
            Vector3 origScale = _transform.localScale;
            _transform.localScale = new Vector3(scale, scale, origScale.z);
        }
    }
}
