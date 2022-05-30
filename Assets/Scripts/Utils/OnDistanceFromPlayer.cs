using Kitsuma.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Kitsuma.Utils
{
    public class OnDistanceFromPlayer : MonoBehaviour
    {
        [SerializeField] private float distance = 20f;
        [SerializeField] private UnityEvent onTooFar;
        private bool _hasSentEvent;
        private Transform _player;

        private Transform _t;

        private void Awake()
        {
            _t = transform;
        }

        private void Start()
        {
            _player = GameManager.Instance.GetPlayer().transform;
        }

        private void Update()
        {
            if (_hasSentEvent || !GetTooFar()) return;
            onTooFar?.Invoke();
            _hasSentEvent = true;
        }

        private void OnEnable()
        {
            _hasSentEvent = false;
        }

        private bool GetTooFar()
        {
            return Mathf.Abs(Vector3.Distance(_player.position, _t.position)) >= distance;
        }
    }
}