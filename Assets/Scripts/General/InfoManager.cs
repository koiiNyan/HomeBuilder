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

        public void CloseInstructions()
        {
            instructionsPanel.SetActive(false);
        }

        public void ControlInstructions()
        {
            instructionsPanel.SetActive(!instructionsPanel.activeSelf);
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
            SceneManager.LoadScene("Level02");
        }

        public void Menu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void ShowPanel()
        {
            nextLvlPanel.SetActive(true);
        }
    }
}