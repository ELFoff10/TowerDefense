using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_Warning;
        [SerializeField] private Button m_ContinueButton;
        [SerializeField] private GameObject m_ContinueText;
        [SerializeField] private GameObject m_ContinueText2;

        private void Start()
        {
            m_ContinueButton.interactable = FileHandler.HasFile(MapCompletion.filename);
            if (FileHandler.HasFile(MapCompletion.filename) == true)
            {
                m_ContinueText.SetActive(true);
                m_ContinueText2.SetActive(false);
            }
            if (FileHandler.HasFile(MapCompletion.filename) == false)
            {
                m_ContinueText2.SetActive(true);
                m_ContinueText.SetActive(false);
            }
        }

        public void NewGame()
        {
            if (FileHandler.HasFile(MapCompletion.filename) == true)
            {
                gameObject.SetActive(false);
                m_Warning.SetActive(true);
            }
            else
            {
                FileHandler.Reset(MapCompletion.filename);
                FileHandler.Reset(Upgrades.filename);
                SceneManager.LoadScene(1);
            }
        }

        public void NewGameAndReset()
        {
            FileHandler.Reset(MapCompletion.filename);
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

