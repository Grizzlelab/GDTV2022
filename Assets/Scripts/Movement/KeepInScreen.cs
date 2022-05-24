using UnityEngine;

namespace Kitsuma.Movement
{
    public class KeepInScreen : MonoBehaviour
    {
        private Transform _t;
        private Vector2 _screenBounds;
        private Camera _camera;

        private void Awake()
        {
            _t = transform;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            _screenBounds = _camera.ScreenToWorldPoint(new Vector3(
                Screen.width, 
                Screen.height, 
                _camera.transform.position.z));
            
            Vector3 pos = _t.position;
            pos.x = Mathf.Clamp(pos.x, -_screenBounds.x, _screenBounds.x);
            pos.y = Mathf.Clamp(pos.y, -_screenBounds.y, _screenBounds.y);
            _t.position = pos;
        }
    }
}