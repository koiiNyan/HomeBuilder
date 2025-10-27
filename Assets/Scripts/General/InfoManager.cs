using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    public class InfoManager : MonoBehaviour
    {
        public GameObject instructionsPanel;
        public GameObject nextLvlPanel;

        public List<GameObject> uiElements;

        private string levelName;

        void Start()
        {
            levelName = SceneManager.GetActiveScene().name;
        }

        public void CloseInstructions()
        {
            instructionsPanel.SetActive(false);

            for (int i = 0; i < uiElements.Count; i++)
            {
                uiElements[i].SetActive(true);
            }
        }

        public void OpenInstructions()
        {
            instructionsPanel.SetActive(true);
            for (int i = 0; i < uiElements.Count; i++)
            {
                uiElements[i].SetActive(false);
            }

        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            Debug.Log("Attempting to stop play mode in Editor");
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void Next()
        {
            if (levelName == "Level01") SceneManager.LoadScene("Level02");
            else if (levelName == "Level02") SceneManager.LoadScene("Level03");
        }

        public void Menu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void ShowPanel()
        {
            for (int i = 0; i < uiElements.Count; i++)
            {
                uiElements[i].SetActive(false);
            }


            nextLvlPanel.SetActive(true);
        }

        ///sounds
        public void PlayButtonSound()
        {
            AudioManager.Instance.PlaySoundEffect(1);
        }

        public void PlayButtonClose()
        {
            AudioManager.Instance.PlaySoundEffect(3);
        }

        public void PlayArrow()
        {
            AudioManager.Instance.PlaySoundEffect(4);
        }

    }
}