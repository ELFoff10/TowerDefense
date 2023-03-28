using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_Warning, m_ContinueText, m_ContinueText2;
        [SerializeField] private Button m_ContinueButton;

        private void Start()
        {
            m_ContinueButton.interactable = FileHandler.HasFile(MapCompletion.Filename);
            if (FileHandler.HasFile(MapCompletion.Filename) == true)
            {
                m_ContinueText.SetActive(true);
                m_ContinueText2.SetActive(false);
            }
            if (FileHandler.HasFile(MapCompletion.Filename) == false)
            {
                m_ContinueText2.SetActive(true);
                m_ContinueText.SetActive(false);
            }
        }

        public void NewGame()
        {
            if (FileHandler.HasFile(MapCompletion.Filename) == true)
            {
                gameObject.SetActive(false);
                m_Warning.SetActive(true);
            }
            else
            {
                FileHandler.Reset(MapCompletion.Filename);
                FileHandler.Reset(Upgrades.filename);
                SceneManager.LoadScene(1);
            }
        }

        public void NewGameAndReset()
        {
            FileHandler.Reset(MapCompletion.Filename);
            FileHandler.Reset(Upgrades.filename);
            SceneManager.LoadScene(1);
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
