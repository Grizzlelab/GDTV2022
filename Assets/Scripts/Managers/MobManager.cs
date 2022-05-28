using System.Collections;
using Kitsuma.Entities.Shared;
using Kitsuma.Utils;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Kitsuma.Managers
{
    public class MobManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] mobPrefabs;
        [SerializeField] private float radiusAroundPlayer = 10f;
        [SerializeField] private int startCapacity = 50;
        [SerializeField] private int maxCapacity = 100;
        [SerializeField] private float spawnWait = 1f;

        private ObjectPool<GameObject> _pool;
        private Transform _player;
        private WaitForSeconds _wait;
        private bool _onCooldown;

        private void Awake()
        {
            _wait = new WaitForSeconds(spawnWait);
        }

        private void Start()
        {
            _player = GameManager.Instance.GetPlayer().transform;
            
            _pool = new ObjectPool<GameObject>(
                () => Instantiate(mobPrefabs[Random.Range(0, mobPrefabs.Length)]),
                mob =>
                {
                    mob.transform.position = _player.position + new Vector3(
                        Random.Range(-radiusAroundPlayer, radiusAroundPlayer) + radiusAroundPlayer, 
                        Random.Range(-radiusAroundPlayer, radiusAroundPlayer) + radiusAroundPlayer, 
                        0f);
                    mob.GetComponent<Release>().SetOnRelease(OnRelease);
                    mob.GetComponent<Health>().Heal(float.MaxValue);
                    mob.SetActive(true);
                },
                mob => mob.SetActive(false),
                Destroy, true, startCapacity, maxCapacity);
        }

        private void Update()
        {
            if (_onCooldown || _pool.CountActive >= maxCapacity) return;
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            _pool.Get();
            _onCooldown = true;
            yield return _wait;
            _onCooldown = false;
        }

        private void OnRelease(GameObject obj) => _pool.Release(obj);
    }
}
