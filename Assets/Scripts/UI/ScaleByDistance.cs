using UnityEngine;

namespace Kitsuma.UI
{
    public class ScaleByDistance : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform player;
        [SerializeField] private Vector3 min = new(0.75f, 0.75f, 1f);
        [SerializeField] private Vector3 small = new(0.8f, 0.8f, 1f);
        [SerializeField] private Vector3 normal = Vector3.one;
        [SerializeField] private Vector3 max = new(1.25f, 1.25f, 1f);
        [SerializeField] private float scaleSpeed = 5f;

        private RectTransform _t;

        private void Awake()
        {
            _t = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (GetScale() == _t.localScale) return;
            _t.localScale = Vector3.Slerp(
                _t.localScale,
                GetScale(),
                scaleSpeed * Time.deltaTime);
        }

        private Vector3 GetScale()
        {
            float dist = Vector3.Distance(target.position, player.position);

            Vector3 scale = dist switch
            {
                >= 100 => min,
                >= 50 and < 100 => small,
                >= 25 and < 50 => normal,
                _ => max
            };

            return scale;
        }
    }
}