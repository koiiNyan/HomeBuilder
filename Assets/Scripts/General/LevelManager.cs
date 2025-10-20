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

    [SerializeField] private GameObject washerObject;
    [SerializeField] private GameObject carpetObject;
    [SerializeField] private GameObject towelObject;

    [SerializeField] private WasherController washerController;
    [SerializeField] private CarpetController carpetController;
    [SerializeField] private TowelController towelController;


    [Header("Level03")]
    [SerializeField] private CameraController_Lvl03 cameraController03; // lvl03
    [SerializeField] private PartManager_Lvl03 partManager03;

    [SerializeField] private GameObject kitchenObject;
    [SerializeField] private GameObject cookerObject;
    [SerializeField] private GameObject fridgeObject;

    [SerializeField] private KitchenController kitchenController;
    [SerializeField] private CookerController cookerController;
    [SerializeField] private FridgeController fridgeController;

    [SerializeField] private GameObject chair01Object;
    [SerializeField] private GameObject chair02Object;
    [SerializeField] private GameObject chair03Object;

    [SerializeField] private Chair01Controller chair01Controller;
    [SerializeField] private Chair02Controller chair02Controller;
    [SerializeField] private Chair03Controller chair03Controller;

    [SerializeField] private GameObject chair04Object;
    [SerializeField] private GameObject tableObject;
    [SerializeField] private GameObject tableItemsObject;

    [SerializeField] private Chair04Controller chair04Controller;
    [SerializeField] private TableController tableController;
    [SerializeField] private TableItemsController tableItemsController;

    [SerializeField] private GameObject vaseObject;
    [SerializeField] private GameObject microwaveObject;
    [SerializeField] private GameObject panObject;

    [SerializeField] private VaseController vaseController;
    [SerializeField] private MicrowaveController microwaveController;
    [SerializeField] private PanController panController;

    [SerializeField] private GameObject dishesObject;
    [SerializeField] private GameObject glass01Object;
    [SerializeField] private GameObject glass02Object;

    [SerializeField] private DishesController dishesController;
    [SerializeField] private Glass01Controller glass01Controller;
    [SerializeField] private Glass02Controller glass02Controller;

    [SerializeField] private GameObject glass03Object;
    [SerializeField] private GameObject glass04Object;
    [SerializeField] private GameObject glass05Object;

    [SerializeField] private Glass03Controller glass03Controller;
    [SerializeField] private Glass04Controller glass04Controller;
    [SerializeField] private Glass05Controller glass05Controller;



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

            gameManager.SetWasherCompleted(saveData.washerCompleted);
            gameManager.SetCarpetCompleted(saveData.carpetCompleted);
            gameManager.SetTowelCompleted(saveData.towelCompleted);
            gameManager.SetIsWasherCarpetTowelCompleted(saveData.IsWasherCarpetTowelCompleted);


            // Set up the scene based on progress
            if (saveData.IsBathSinkToiletCompleted && !saveData.IsWasherCarpetTowelCompleted)
            {
                washerObject.SetActive(true);
                carpetObject.SetActive(true);
                towelObject.SetActive(true);
                partManager02.InitializeWasherCarpetTowelParts();
                cameraController02.defaultTransform = cameraController02.washerCameraPosition;
                cameraController02.ResetZoom();
            }

            else if (saveData.IsWasherCarpetTowelCompleted)
            {
                cameraController02.defaultTransform = cameraController02.finalPosition;
                cameraController02.defaultZoom = 10f;
                cameraController02.ResetZoom();
                gameManager.infoManager.ShowPanel();
            }

            // Update objects based on saved state
            UpdateObjectState(bathObject, bathController, saveData.bathCompleted);
            UpdateObjectState(sinkObject, sinkController, saveData.sinkCompleted);
            UpdateObjectState(toiletObject, toiletController, saveData.toiletCompleted);

            UpdateObjectState(washerObject, washerController, saveData.washerCompleted);
            UpdateObjectState(carpetObject, carpetController, saveData.carpetCompleted);
            UpdateObjectState(towelObject, towelController, saveData.towelCompleted);

            Debug.Log("Save data applied successfully");
        }

        else if (saveData.levelName == "Level03")

        {
            GameManager_Lvl03 gameManager = FindObjectOfType<GameManager_Lvl03>();
            if (gameManager == null)
            {
                Debug.LogError("GameManager not found in the scene");
                return;
            }

            // Apply the save data to GameManager
            gameManager.SetKitchenCompleted(saveData.kitchenCompleted);
            gameManager.SetCookerCompleted(saveData.cookerCompleted);
            gameManager.SetFridgeCompleted(saveData.fridgeCompleted);
            gameManager.SetIsKitchenCookerFridgeCompleted(saveData.IsKitchenCookerFridgeCompleted);

            gameManager.SetChair01Completed(saveData.chair01Completed);
            gameManager.SetChair02Completed(saveData.chair02Completed);
            gameManager.SetChair03Completed(saveData.chair03Completed);
            gameManager.SetIsChair01Chair02Chair03Completed(saveData.IsChair01Chair02Chair03Completed);

            gameManager.SetChair04Completed(saveData.chair04Completed);
            gameManager.SetTableCompleted(saveData.tableCompleted);
            gameManager.SetTableItemsCompleted(saveData.tableItemsCompleted);
            gameManager.SetIsChair04TableTableItemsCompleted(saveData.IsChair04TableTableItemsCompleted);

            gameManager.SetVaseCompleted(saveData.vaseCompleted);
            gameManager.SetMicrowaveCompleted(saveData.microwaveCompleted);
            gameManager.SetPanCompleted(saveData.panCompleted);
            gameManager.SetIsVaseMicrowavePanCompleted(saveData.IsVaseMicrowavePanCompleted);

            gameManager.SetDishesCompleted(saveData.dishesCompleted);
            gameManager.SetGlass01Completed(saveData.glass01Completed);
            gameManager.SetGlass02Completed(saveData.glass02Completed);
            gameManager.SetIsDishesGlass01Glass02Completed(saveData.IsDishesGlass01Glass02Completed);

            gameManager.SetGlass03Completed(saveData.glass03Completed);
            gameManager.SetGlass04Completed(saveData.glass04Completed);
            gameManager.SetGlass05Completed(saveData.glass05Completed);
            gameManager.SetIsGlass03Glass04Glass05Completed(saveData.IsGlass03Glass04Glass05Completed);


            // Set up the scene based on progress
            if (saveData.IsKitchenCookerFridgeCompleted && !saveData.IsChair01Chair02Chair03Completed)
            {
                chair01Object.SetActive(true);
                chair02Object.SetActive(true);
                chair03Object.SetActive(true);
                partManager03.InitializeChair01Chair02Chair03Parts();
                cameraController03.defaultTransform = cameraController03.tableCameraPosition;
                cameraController03.ResetZoom();
            }

            else if (saveData.IsChair01Chair02Chair03Completed && !saveData.IsChair04TableTableItemsCompleted)
            {
                chair04Object.SetActive(true);
                tableObject.SetActive(true);
                tableItemsObject.SetActive(true);
                partManager03.InitializeChair04TableTableItemsParts();
                cameraController03.defaultTransform = cameraController03.tableCameraPosition;
                cameraController03.ResetZoom();
            }

            else if (saveData.IsChair04TableTableItemsCompleted && !saveData.IsVaseMicrowavePanCompleted)
            {
                vaseObject.SetActive(true);
                microwaveObject.SetActive(true);
                panObject.SetActive(true);
                partManager03.InitializeVaseMicrowavePanParts();
                cameraController03.defaultTransform = cameraController03.vaseCameraPosition;
                cameraController03.defaultZoom = cameraController03.vaseZoom;
                cameraController03.ResetZoom();
            }

            else if (saveData.IsVaseMicrowavePanCompleted && !saveData.IsDishesGlass01Glass02Completed)
            {
                dishesObject.SetActive(true);
                glass01Object.SetActive(true);
                glass02Object.SetActive(true);
                partManager03.InitializeDishesGlass01Glass02Parts();
                cameraController03.defaultTransform = cameraController03.dishesCameraPosition;
                cameraController03.defaultZoom = cameraController03.dishesZoom;
                cameraController03.ResetZoom();
            }

            else if (saveData.IsDishesGlass01Glass02Completed && !saveData.IsGlass03Glass04Glass05Completed)
            {
                glass03Object.SetActive(true);
                glass04Object.SetActive(true);
                glass05Object.SetActive(true);
                partManager03.InitializeGlass03Glass04Glass05Parts();
                cameraController03.defaultTransform = cameraController03.dishesCameraPosition;
                cameraController03.defaultZoom = cameraController03.dishesZoom;
                cameraController03.ResetZoom();
            }

            else if (saveData.IsGlass03Glass04Glass05Completed)
            {
                cameraController03.defaultTransform = cameraController03.finalPosition;
                cameraController03.defaultZoom = cameraController03.finalZoom;
                cameraController03.ResetZoom();
                gameManager.infoManager.ShowPanel();
            }

            // Update objects based on saved state
            UpdateObjectState(kitchenObject, kitchenController, saveData.kitchenCompleted);
            UpdateObjectState(cookerObject, cookerController, saveData.cookerCompleted);
            UpdateObjectState(fridgeObject, fridgeController, saveData.fridgeCompleted);

            UpdateObjectState(chair01Object, chair01Controller, saveData.chair01Completed);
            UpdateObjectState(chair02Object, chair02Controller, saveData.chair02Completed);
            UpdateObjectState(chair03Object, chair03Controller, saveData.chair03Completed);

            UpdateObjectState(chair04Object, chair04Controller, saveData.chair04Completed);
            UpdateObjectState(tableObject, tableController, saveData.tableCompleted);
            UpdateObjectState(tableItemsObject, tableItemsController, saveData.tableItemsCompleted);

            UpdateObjectState(vaseObject, vaseController, saveData.vaseCompleted);
            UpdateObjectState(microwaveObject, microwaveController, saveData.microwaveCompleted);
            UpdateObjectState(panObject, panController, saveData.panCompleted);

            UpdateObjectState(dishesObject, dishesController, saveData.dishesCompleted);
            UpdateObjectState(glass01Object, glass01Controller, saveData.glass01Completed);
            UpdateObjectState(glass02Object, glass02Controller, saveData.glass02Completed);

            UpdateObjectState(glass03Object, glass03Controller, saveData.glass03Completed);
            UpdateObjectState(glass04Object, glass04Controller, saveData.glass04Completed);
            UpdateObjectState(glass05Object, glass05Controller, saveData.glass05Completed);

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
