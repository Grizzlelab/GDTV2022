using TMPro;
using UnityEngine;

namespace Kitsuma.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text header;
        [SerializeField] private TMP_Text subheader;

        public void SetHeader(string text) => header.text = text;
        public void SetSubHeader(string text) => subheader.text = text;
    }
}