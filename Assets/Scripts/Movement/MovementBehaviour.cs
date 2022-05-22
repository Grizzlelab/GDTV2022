using UnityEngine;
using UnityEngine.Events;

namespace Kitsuma.Movement
{
    public class MovementBehaviour : MonoBehaviour
    {
        [SerializeField] protected UnityEvent onWalkDown;
        [SerializeField] protected UnityEvent onWalkUp;
        [SerializeField] protected UnityEvent onWalkLeft;
        [SerializeField] protected UnityEvent onWalkRight;
        [SerializeField] protected UnityEvent onIdle;
    }
}