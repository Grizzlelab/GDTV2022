using System.Collections;
using Kitsuma.Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Kitsuma.Movement
{
    public class LurchTowardsPlayer : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string restAnim = "Slime_Rest";
        [SerializeField] private string lurchAnim = "Slime_Lurch";
        [SerializeField] private float speed = 7f;
        [SerializeField] private float restTime = 0.25f;
        [SerializeField] private float lurchTime = 0.5f;
        // Direction
        [SerializeField] private UnityEvent<Vector3> onMovement;

        private Transform _t;
        private Transform _player;
        private WaitForSeconds _rest;
        private WaitForSeconds _lurch;
        private bool _canMove;

        private void Awake()
        {
            _t = transform;
        }

        private void Start()
        {
            _player = GameManager.Instance.GetPlayer().transform;
            StartCoroutine(RestCoroutine());
        }

        private void Update()
        {
            if (!_canMove || IsAtPlayer()) return;
            Vector3 dir = GetDirectionToPlayer();
            _t.position += dir * (speed * Time.deltaTime);
            onMovement?.Invoke(dir);
        }

        private Vector3 GetDirectionToPlayer()
        {
            return (_player.position - _t.position).normalized;
        }

        private IEnumerator RestCoroutine()
        {
            _rest ??= new WaitForSeconds(restTime);
            animator.Play(restAnim);
            yield return _rest;
            _canMove = true;
            StartCoroutine(LurchCoroutine());
        }

        private IEnumerator LurchCoroutine()
        {
            _lurch ??= new WaitForSeconds(lurchTime);
            animator.Play(lurchAnim);
            yield return _lurch;
            _canMove = false;
            StartCoroutine(RestCoroutine());
        }

        private bool IsAtPlayer()
        {
            return Vector3.Distance(_t.position, _player.position) <= 0.5f;
        }
    }
}