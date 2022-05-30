using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kitsuma.Utils
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        public void Load()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}