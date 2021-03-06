using System.Collections;
using Kitsuma.Movement;
using Kitsuma.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace Kitsuma.Managers
{
    public class PooledRandomSpawner : MonoBehaviour
    {
        [SerializeField] protected GameObject[] prefabs;
        [SerializeField] protected int minCapacity = 10;
        [SerializeField] protected int maxCapacity = 20;
        [SerializeField] protected float spawnWait = 1f;
        [SerializeField] protected int batchSpawn = 10;
        private bool _onCooldown;

        private WaitForSeconds _wait;
        protected Transform Player;

        protected ObjectPool<GameObject> Pool;

        private void Awake()
        {
            _wait = new WaitForSeconds(spawnWait);
        }

        private void Start()
        {
            Player = GameManager.Instance.GetPlayer().transform;

            Pool = new ObjectPool<GameObject>(
                () => Instantiate(prefabs[Random.Range(0, prefabs.Length)]),
                OnCreate,
                obj => obj.SetActive(false),
                Destroy, true, minCapacity, maxCapacity);
        }

        private void Update()
        {
            if (_onCooldown || Pool.CountActive >= maxCapacity) return;
            StartCoroutine(Spawn());
        }

        protected virtual void OnCreate(GameObject obj)
        {
            obj.GetComponent<PlaceRandomlyFrom>().PlaceRandomly(Player.position);
            obj.GetComponent<Release>().SetOnRelease(OnRelease);
            obj.SetActive(true);
        }

        private IEnumerator Spawn()
        {
            if (batchSpawn + Pool.CountActive >= maxCapacity)
                Pool.Get();
            else
                for (var i = 0; i < batchSpawn; i++)
                    Pool.Get();

            _onCooldown = true;
            yield return _wait;
            _onCooldown = false;
        }

        private void OnRelease(GameObject obj)
        {
            Pool.Release(obj);
        }
    }
}