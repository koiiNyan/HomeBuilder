using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject title;
    public Button continueButton;
    public GameObject buttons;
    public GameObject roomsPanel;
    public GameObject instructionsPanel;

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            continueButton.interactable = false;
            exitButton.interactable = false;
#else
        if (!SaveManager.HasSaveFile())
        {
            continueButton.interactable = false;
        }
#endif
    }

    public void ContinueGame()
    {
        SaveManager.SaveData saveData = SaveManager.LoadGame();
        if (saveData != null)
        {
            PlayerPrefs.SetInt("LoadSavedGame", 1);
            PlayerPrefs.Save(); // Ensure the PlayerPrefs are saved
            SceneManager.LoadScene(saveData.levelName);
        }
        else
        {
            Debug.LogWarning("No save data found. Starting a new game.");
            NewGame();
        }
    }

    public void NewGame()
    {
        // Start a new game
        SceneManager.LoadScene("Level01");
    }

    public void ShowRooms()
    {
        roomsPanel.SetActive(true);
        ButtonsController(false);
    }

    public void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
        ButtonsController(false);
    }

    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
        ButtonsController(true);
    }

    public void ButtonsController(bool action)
    {
        title.SetActive(action);
        buttons.SetActive(action);
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
}