using System;
using System.Collections;
using Kitsuma.Utils;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Kitsuma.Managers
{
    public class PooledRandomSpawner : MonoBehaviour
    {
        [SerializeField] protected GameObject[] prefabs;
        [SerializeField] protected int minCapacity = 10;
        [SerializeField] protected int maxCapacity = 20;
        [SerializeField] protected float radiusAroundPlayer = 10f;
        [SerializeField] protected float spawnWait = 1f;
        [SerializeField] protected int batchSpawn = 10;

        protected ObjectPool<GameObject> Pool;
        protected Transform Player;
        
        private WaitForSeconds _wait;
        private bool _onCooldown;

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
        
        protected virtual void OnCreate(GameObject obj)
        {
            obj.transform.position = Player.position + new Vector3(
                Random.Range(-radiusAroundPlayer, radiusAroundPlayer) * radiusAroundPlayer / 2,
                Random.Range(-radiusAroundPlayer, radiusAroundPlayer) * radiusAroundPlayer / 2,
                0f);
            obj.GetComponent<Release>().SetOnRelease(OnRelease);
            obj.SetActive(true);
        }

        private void Update()
        {
            if (_onCooldown || Pool.CountActive >= maxCapacity) return;
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            if (batchSpawn + Pool.CountActive >= maxCapacity)
            {
                Pool.Get();
            }
            else
            {
                for (var i = 0; i < batchSpawn; i++)
                {
                    Pool.Get();
                }
            }
            
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