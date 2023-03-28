using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public class MainMenuSceneChange : MonoBehaviour
    {
        public void Change()
        {
            SceneManager.LoadScene(0);
        }
    }
}
