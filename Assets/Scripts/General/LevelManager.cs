using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [Header("Level01")]
    [SerializeField] private GameObject bedObject;
    [SerializeField] private GameObject bookshelfObject;
    [SerializeField] private GameObject deskObject;
    [SerializeField] private GameObject chairObject;
    [SerializeField] private GameObject rugObject;
    [SerializeField] private GameObject posterObject;
    [SerializeField] private GameObject sofaObject;
    [SerializeField] private GameObject tvObject;

    [SerializeField] private BedController bedController;
    [SerializeField] private BookshelfController bookshelfController;
    [SerializeField] private DeskController deskController;
    [SerializeField] private ChairController chairController;
    [SerializeField] private RugController rugController;
    [SerializeField] private PosterController posterController;
    [SerializeField] private SofaController sofaController;
    [SerializeField] private TVController tvController;

    [SerializeField] private PartManager partManager;
    [SerializeField] private CameraController cameraController;



    [Header("Level02")]
    [SerializeField] private CameraController_Lvl02 cameraController02; // lvl02
    [SerializeField] private PartManager_Lvl02 partManager02;

    [SerializeField] private GameObject bathObject;
    [SerializeField] private GameObject sinkObject;
    [SerializeField] private GameObject toiletObject;

    [SerializeField] private BathController bathController;
    [SerializeField] private SinkController sinkController;
    [SerializeField] private ToiletController toiletController;


    void Start()
    {
        Debug.Log("LevelManager Start called");
        if (PlayerPrefs.HasKey("LoadSavedGame"))
        {
            int loadFlag = PlayerPrefs.GetInt("LoadSavedGame", 0);
            Debug.Log($"LoadSavedGame flag: {loadFlag}");
            if (loadFlag == 1)
            {
                LoadSavedGame();
                PlayerPrefs.DeleteKey("LoadSavedGame");
                PlayerPrefs.Save();
            }
        }
        else
        {
            Debug.Log("LoadSavedGame key not found in PlayerPrefs");
        }
    }

    void LoadSavedGame()
    {
        SaveManager.SaveData saveData = SaveManager.LoadGame();
        if (saveData != null)
        {
            if (saveData.levelName == SceneManager.GetActiveScene().name)
            {
                ApplySaveData(saveData);
            }
            else
            {
                Debug.LogWarning("Loaded level doesn't match current scene");
            }
        }
        else
        {
            Debug.LogWarning("No save data found");
        }
    }

    void ApplySaveData(SaveManager.SaveData saveData)
    {
        Debug.Log("Applying save data");

        if (saveData.levelName == "Level01")

        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager not found in the scene");
                return;
            }

            // Apply the save data to GameManager
            gameManager.SetBedCompleted(saveData.bedCompleted);
            gameManager.SetBookshelfCompleted(saveData.bookshelfCompleted);
            gameManager.SetDeskCompleted(saveData.deskCompleted);
            gameManager.SetChairCompleted(saveData.chairCompleted);
            gameManager.SetRugCompleted(saveData.rugCompleted);
            gameManager.SetPosterCompleted(saveData.posterCompleted);
            gameManager.SetSofaCompleted(saveData.sofaCompleted);
            gameManager.SetTVCompleted(saveData.tvCompleted);
            gameManager.SetIsBedBookCompleted(saveData.IsBedBookCompleted);
            gameManager.SetIsDeskChairCompleted(saveData.IsDeskChairCompleted);
            gameManager.SetIsRugPosterCompleted(saveData.IsRugPosterCompleted);
            gameManager.SetIsSofaTVCompleted(saveData.IsSofaTVCompleted);

            // Set up the scene based on progress
            if (saveData.IsBedBookCompleted && !saveData.IsDeskChairCompleted)
            {
                deskObject.SetActive(true);
                chairObject.SetActive(true);
                partManager.InitializeDeskParts();
                cameraController.defaultTransform = cameraController.deskCameraPosition;
                cameraController.ResetZoom();
            }
            else if (saveData.IsDeskChairCompleted && !saveData.IsRugPosterCompleted)
            {
                rugObject.SetActive(true);
                posterObject.SetActive(true);
                partManager.InitializeRugParts();
                cameraController.defaultTransform = cameraController.rugCameraPosition;
                cameraController.ResetZoom();
            }
            else if (saveData.IsRugPosterCompleted && !saveData.IsSofaTVCompleted)
            {
                sofaObject.SetActive(true);
                tvObject.SetActive(true);
                partManager.InitializeSofaParts();
                cameraController.defaultTransform = cameraController.sofaTVCameraPosition;
                cameraController.ResetZoom();
            }
            else if (saveData.IsSofaTVCompleted)
            {
                cameraController.defaultTransform = cameraController.finalPosition;
                cameraController.defaultZoom = 10f;
                cameraController.ResetZoom();
                gameManager.infoManager.ShowPanel();
            }

            // Update objects based on saved state
            UpdateObjectState(bedObject, bedController, saveData.bedCompleted);
            UpdateObjectState(bookshelfObject, bookshelfController, saveData.bookshelfCompleted);
            UpdateObjectState(deskObject, deskController, saveData.deskCompleted);
            UpdateObjectState(chairObject, chairController, saveData.chairCompleted);
            UpdateObjectState(rugObject, rugController, saveData.rugCompleted);
            UpdateObjectState(posterObject, posterController, saveData.posterCompleted);
            UpdateObjectState(sofaObject, sofaController, saveData.sofaCompleted);
            UpdateObjectState(tvObject, tvController, saveData.tvCompleted);

            Debug.Log("Save data applied successfully");
        }

        else if (saveData.levelName == "Level02")

        {
            GameManager_Lvl02 gameManager = FindObjectOfType<GameManager_Lvl02>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager not found in the scene");
                return;
            }

            // Apply the save data to GameManager
            gameManager.SetBathCompleted(saveData.bathCompleted);
            gameManager.SetSinkCompleted(saveData.sinkCompleted);
            gameManager.SetToiletCompleted(saveData.toiletCompleted);
            gameManager.SetIsBathSinkToiletCompleted(saveData.IsBathSinkToiletCompleted);


            // Set up the scene based on progress
            /*if (saveData.IsBedBookCompleted && !saveData.IsDeskChairCompleted)
            {
                deskObject.SetActive(true);
                chairObject.SetActive(true);
                partManager.InitializeDeskParts();
                cameraController.defaultTransform = cameraController.deskCameraPosition;
                cameraController.ResetZoom();
            }
            else if (saveData.IsDeskChairCompleted && !saveData.IsRugPosterCompleted)
            {
                rugObject.SetActive(true);
                posterObject.SetActive(true);
                partManager.InitializeRugParts();
                cameraController.defaultTransform = cameraController.rugCameraPosition;
                cameraController.ResetZoom();
            }
            else if (saveData.IsRugPosterCompleted && !saveData.IsSofaTVCompleted)
            {
                sofaObject.SetActive(true);
                tvObject.SetActive(true);
                partManager.InitializeSofaParts();
                cameraController.defaultTransform = cameraController.sofaTVCameraPosition;
                cameraController.ResetZoom();
            }
            else if (saveData.IsSofaTVCompleted)
            {
                cameraController.defaultTransform = cameraController.finalPosition;
                cameraController.defaultZoom = 10f;
                cameraController.ResetZoom();
                gameManager.infoManager.ShowPanel();
            } */

            // Update objects based on saved state
            UpdateObjectState(bathObject, bathController, saveData.bathCompleted);
            UpdateObjectState(sinkObject, sinkController, saveData.sinkCompleted);
            UpdateObjectState(toiletObject, toiletController, saveData.toiletCompleted);

            Debug.Log("Save data applied successfully");
        }
    }

    void UpdateObjectState(GameObject obj, MonoBehaviour controller, bool completed)
    {
        if (obj != null && controller != null && completed)
        {
            obj.SetActive(true);
            if (controller is ILoadable loadable)
            {
                loadable.OnLoad();
            }
            Debug.Log($"{obj.name} state updated");
        }
    }


    void OnDisable()
    {
        Debug.Log("LevelManager: OnDisable called, triggering save");
        SaveManager.SaveGame(SceneManager.GetActiveScene().name);
    }

    void OnApplicationQuit()
    {
        Debug.Log("LevelManager: OnApplicationQuit called, triggering save");
        SaveManager.SaveGame(SceneManager.GetActiveScene().name);
    }
}
