using TMPro;
using UnityEngine;

namespace Kitsuma.Utils.Debug
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmp;

        private Animator _anim;
        private string _state;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        public void ShowAnimation()
        {
            if (string.Equals(_state, tmp.text)) return;
            // We have to remove the trailing whitespace from the input field
            string cleaned = tmp.text.Replace("\u200B", "");
            _state = cleaned;
            _anim.Play(cleaned);
        }
    }
}