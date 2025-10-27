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

    public Button lvl01ContinueButton;
    public Button lvl01ReplayButton;
    public Button lvl02ContinueButton;
    public Button lvl02ReplayButton;
    public Button lvl03ContinueButton;
    public Button lvl03ReplayButton;

    public GameObject restartPanel;
    public GameObject lvl01YesButton;
    public GameObject lvl02YesButton;
    public GameObject lvl03YesButton;

    void Start()
    {
        if (!SaveManager.HasSaveFile())
        {
            continueButton.interactable = false;
            lvl01ContinueButton.interactable = false;
            lvl01ReplayButton.interactable = false;
            lvl02ContinueButton.interactable = false;
            lvl02ReplayButton.interactable = false;
            lvl03ContinueButton.interactable = false;
            lvl03ReplayButton.interactable = false;
        }

        else
        {
            SaveManager.SaveData saveData = SaveManager.LoadGame();
            lvl01ContinueButton.interactable = false;
            lvl01ReplayButton.interactable = false;
            lvl02ContinueButton.interactable = false;
            lvl02ReplayButton.interactable = false;
            lvl03ContinueButton.interactable = false;
            lvl03ReplayButton.interactable = false;


            if (saveData.levelName == "Level01")
            {
                if (!saveData.IsSofaTVCompleted) lvl01ContinueButton.interactable = true;
            }
            else if (saveData.levelName == "Level02")
            {
                if (!saveData.IsWasherCarpetTowelCompleted) lvl02ContinueButton.interactable = true;
            }
            else if (saveData.levelName == "Level03")
            {
                if (!saveData.IsGlass03Glass04Glass05Completed) lvl03ContinueButton.interactable = true;
            }

            if (saveData.IsSofaTVCompleted) lvl01ReplayButton.interactable = true;
            if (saveData.IsWasherCarpetTowelCompleted) lvl02ReplayButton.interactable = true;
            if (saveData.IsGlass03Glass04Glass05Completed) lvl03ReplayButton.interactable = true;

        }

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

    public void StartLvl02()
    {
        SceneManager.LoadScene("Level02");
    }

    public void StartLvl03()
    {
        SceneManager.LoadScene("Level03");
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

    public void InitiateRestartLvl01()
    {
        roomsPanel.SetActive(false);
        restartPanel.SetActive(true);

        lvl01YesButton.SetActive(true);
        lvl02YesButton.SetActive(false);
        lvl03YesButton.SetActive(false);
    }

    public void InitiateRestartLvl02()
    {
        roomsPanel.SetActive(false);
        restartPanel.SetActive(true);

        lvl01YesButton.SetActive(false);
        lvl02YesButton.SetActive(true);
        lvl03YesButton.SetActive(false);
    }

    public void InitiateRestartLvl03()
    {
        roomsPanel.SetActive(false);
        restartPanel.SetActive(true);

        lvl01YesButton.SetActive(false);
        lvl02YesButton.SetActive(false);
        lvl03YesButton.SetActive(true);
    }

    public void RestartNo()
    {
        roomsPanel.SetActive(true);
        restartPanel.SetActive(false);
    }

    public void PlayButtonSound()
    {
        AudioManager.Instance.PlaySoundEffect(1);
    }

    public void PlayButtonClose()
    {
        AudioManager.Instance.PlaySoundEffect(3);
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