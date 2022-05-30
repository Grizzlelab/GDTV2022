using UnityEngine;

namespace Kitsuma.Utils
{
    public class PlaySfx : MonoBehaviour
    {
        [SerializeField] private float minPitch = 0.9f;
        [SerializeField] private float maxPitch = 1.1f;

        private AudioSource _audio;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

        public void Play()
        {
            if (_audio.isPlaying) return;
            _audio.pitch = Random.Range(minPitch, maxPitch);
            _audio.Play();
        }
    }
}