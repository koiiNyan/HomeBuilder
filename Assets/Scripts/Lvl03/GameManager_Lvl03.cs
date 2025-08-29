using UnityEngine;
using System.Collections.Generic;
using General;


public class GameManager_Lvl03 : MonoBehaviour
{
    public static GameManager_Lvl03 Instance;

    private Dictionary<string, List<DraggablePart>> placedParts = new Dictionary<string, List<DraggablePart>>();

    public InfoManager infoManager;
    public ParticleManager particleManager;

    public int totalParts = 10;
    //first
    public KitchenController kitchenController;
    public CookerController cookerController;
    public FridgeController fridgeController;

    //second
    public GameObject chair01Object; 
    public Chair01Controller chair01Controller;

    public GameObject chair02Object;
    public Chair02Controller chair02Controller;

    public GameObject chair03Object;
    public Chair03Controller chair03Controller;

    //third
    public GameObject chair04Object;
    public Chair04Controller chair04Controller;

    public GameObject tableObject;
    public TableController tableController;

    public GameObject tableItemsObject;
    public TableItemsController tableItemsController;

    //forth
    public GameObject vaseObject;
    public VaseController vaseController;

    public GameObject microwaveObject;
    public MicrowaveController microwaveController;

    public GameObject panObject;
    public PanController panController;

    //fifth
    public GameObject dishesObject;
    public DishesController dishesController;

    public GameObject glass01Object;
    public Glass01Controller glass01Controller;

    public GameObject glass02Object;
    public Glass02Controller glass02Controller;

    //sixth
    public GameObject glass03Object;
    public Glass03Controller glass03Controller;

    public GameObject glass04Object;
    public Glass04Controller glass04Controller;

    public GameObject glass05Object;
    public Glass05Controller glass05Controller;

    private bool kitchenCompleted = false;
    private bool cookerCompleted = false;
    private bool fridgeCompleted = false;

    private bool chair01Completed = false;
    private bool chair02Completed = false;
    private bool chair03Completed = false;

    private bool chair04Completed = false;
    private bool tableCompleted = false;
    private bool tableItemsCompleted = false;

    private bool vaseCompleted = false;
    private bool microwaveCompleted = false;
    private bool panCompleted = false;

    private bool dishesCompleted = false;
    private bool glass01Completed = false;
    private bool glass02Completed = false;

    private bool glass03Completed = false;
    private bool glass04Completed = false;
    private bool glass05Completed = false;


    private CameraController_Lvl03 cameraController;
    public PartManager_Lvl03 partManager;

    private bool IsKitchenCookerFridgeCompleted = false;
    private bool IsChair01Chair02Chair03Completed = false;
    private bool IsChair04TableTableItemsCompleted = false;
    private bool IsVaseMicrowavePanCompleted = false;
    private bool IsDishesGlass01Glass02Completed = false;
    private bool IsGlass03Glass04Glass05Completed = false;


    // Public getters
    public bool GetKitchenCompleted() => kitchenCompleted;
    public bool GetCookerCompleted() => cookerCompleted;
    public bool GetFridgeCompleted() => fridgeCompleted;

    public bool GetChair01Completed() => chair01Completed;
    public bool GetChair02Completed() => chair02Completed;
    public bool GetChair03Completed() => chair03Completed;

    public bool GetChair04Completed() => chair04Completed;
    public bool GetTableCompleted() => tableCompleted;
    public bool GetTableItemsCompleted() => tableItemsCompleted;

    public bool GetVaseCompleted() => vaseCompleted;
    public bool GetMicrowaveCompleted() => microwaveCompleted;
    public bool GetPanCompleted() => panCompleted;

    public bool GetDishesCompleted() => dishesCompleted;
    public bool GetGlass01Completed() => glass01Completed;
    public bool GetGlass02Completed() => glass02Completed;

    public bool GetGlass03Completed() => glass03Completed;
    public bool GetGlass04Completed() => glass04Completed;
    public bool GetGlass05Completed() => glass05Completed;

    public bool GetIsKitchenCookerFridgeCompleted() => IsKitchenCookerFridgeCompleted;
    public bool GetIsChair01Chair02Chair03Completed() => IsChair01Chair02Chair03Completed;
    public bool GetIsChair04TableTableItemsCompleted() => IsChair04TableTableItemsCompleted;
    public bool GetIsVaseMicrowavePanCompleted() => IsVaseMicrowavePanCompleted;
    public bool GetIsDishesGlass01Glass02Completed() => IsDishesGlass01Glass02Completed;
    public bool GetIsGlass03Glass04Glass05Completed() => IsGlass03Glass04Glass05Completed;

    // Public setters
    public void SetKitchenCompleted(bool value) => kitchenCompleted = value;
    public void SetCookerCompleted(bool value) => cookerCompleted = value;
    public void SetFridgeCompleted(bool value) => fridgeCompleted = value;

    public void SetChair01Completed(bool value) => chair01Completed = value;
    public void SetChair02Completed(bool value) => chair02Completed = value;
    public void SetChair03Completed(bool value) => chair03Completed = value;

    public void SetChair04Completed(bool value) => chair04Completed = value;
    public void SetTableCompleted(bool value) => tableCompleted = value;
    public void SetTableItemsCompleted(bool value) => tableItemsCompleted = value;

    public void SetVaseCompleted(bool value) => vaseCompleted = value;
    public void SetMicrowaveCompleted(bool value) => microwaveCompleted = value;
    public void SetPanCompleted(bool value) => panCompleted = value;

    public void SetDishesCompleted(bool value) => dishesCompleted = value;
    public void SetGlass01Completed(bool value) => glass01Completed = value;
    public void SetGlass02Completed(bool value) => glass02Completed = value;

    public void SetGlass03Completed(bool value) => glass03Completed = value;
    public void SetGlass04Completed(bool value) => glass04Completed = value;
    public void SetGlass05Completed(bool value) => glass05Completed = value;

    public void SetIsKitchenCookerFridgeCompleted(bool value) => IsKitchenCookerFridgeCompleted = value;
    public void SetIsChair01Chair02Chair03Completed(bool value) => IsChair01Chair02Chair03Completed = value;
    public void SetIsChair04TableTableItemsCompleted(bool value) => IsChair04TableTableItemsCompleted = value;
    public void SetVaseMicrowavePanCompleted(bool value) => IsVaseMicrowavePanCompleted = value;
    public bool SetIsDishesGlass01Glass02Completed(bool value) => IsDishesGlass01Glass02Completed = value;
    public bool SetIsGlass03Glass04Glass05Completed(bool value) => IsGlass03Glass04Glass05Completed = value;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        placedParts["Kitchen"] = new List<DraggablePart>();
        placedParts["Cooker"] = new List<DraggablePart>();
        placedParts["Fridge"] = new List<DraggablePart>();

        placedParts["Chair01"] = new List<DraggablePart>();
        placedParts["Chair02"] = new List<DraggablePart>();
        placedParts["Chair03"] = new List<DraggablePart>();

        placedParts["Chair04"] = new List<DraggablePart>();
        placedParts["Table"] = new List<DraggablePart>();
        placedParts["TableItems"] = new List<DraggablePart>();

        placedParts["Vase"] = new List<DraggablePart>();
        placedParts["Microwave"] = new List<DraggablePart>();
        placedParts["Pan"] = new List<DraggablePart>();

        placedParts["Dishes"] = new List<DraggablePart>();
        placedParts["Glass01"] = new List<DraggablePart>();
        placedParts["Glass02"] = new List<DraggablePart>();

        placedParts["Glass03"] = new List<DraggablePart>();
        placedParts["Glass04"] = new List<DraggablePart>();
        placedParts["Glass05"] = new List<DraggablePart>();

        FindCameraController();
    }

    void FindCameraController()
    {
        // First, try to find CameraRig in the scene
        GameObject cameraRig = GameObject.Find("CameraRig");
        if (cameraRig != null)
        {
            cameraController = cameraRig.GetComponent<CameraController_Lvl03>();
        }

        // If not found on CameraRig, try to find it in the scene
        cameraController = FindObjectOfType<CameraController_Lvl03>();
        if (cameraController == null)
        {
            Debug.LogError("CameraController not found in the scene");
        }
    }

    public void OnPartPlaced(DraggablePart part, string objectType)
    {
        if (!placedParts[objectType].Contains(part))
        {
            placedParts[objectType].Add(part);
            CheckObjectCompletion(objectType, totalParts);
        }


        partManager.UpdateVisibleParts();
    }

    void CheckObjectCompletion(string objectType, int total)
    {
        int requiredParts = total;
        if (placedParts[objectType].Count == requiredParts)
        {
            Debug.Log($"All parts placed correctly for {objectType}!");


            if (!IsKitchenCookerFridgeCompleted)
            {
                if (objectType == "Kitchen" && kitchenController != null)
                {
                    kitchenCompleted = true;
                    kitchenController.OnAllPartsPlaced();
                }
                else if (objectType == "Cooker" && cookerController != null)
                {
                    cookerCompleted = true;
                    cookerController.OnAllPartsPlaced();
                }
                else if (objectType == "Fridge" && fridgeController != null)
                {
                    fridgeCompleted = true;
                    fridgeController.OnAllPartsPlaced();
                }


                CheckKitchenCookerFridgeObjectsCompleted();
            }
            if (!IsChair01Chair02Chair03Completed)
            {
                if (objectType == "Chair01" && chair01Controller != null)
                {
                    chair01Completed = true;
                    chair01Controller.OnAllPartsPlaced();
                }

                if (objectType == "Chair02" && chair02Controller != null)
                {
                    chair02Completed = true;
                    chair02Controller.OnAllPartsPlaced();
                }

                if (objectType == "Chair03" && chair03Controller != null)
                {
                    chair03Completed = true;
                    chair03Controller.OnAllPartsPlaced();
                }

                CheckChair01Chair02Chair03Completed();
            }

            if (!IsChair04TableTableItemsCompleted)
            {
                if (objectType == "Chair04" && chair04Controller != null)
                {
                    chair04Completed = true;
                    chair04Controller.OnAllPartsPlaced();
                }

                if (objectType == "Table" && tableController != null)
                {
                    tableCompleted = true;
                    tableController.OnAllPartsPlaced();
                }

                if (objectType == "TableItems" && tableItemsController != null)
                {
                    tableItemsCompleted = true;
                    tableItemsController.OnAllPartsPlaced();
                }

                CheckChair04TableTableItemsCompleted();
            }

            if (!IsVaseMicrowavePanCompleted)
            {
                if (objectType == "Vase" && vaseController != null)
                {
                    vaseCompleted = true;
                    vaseController.OnAllPartsPlaced();
                }

                if (objectType == "Microwave" && microwaveController != null)
                {
                    microwaveCompleted = true;
                    microwaveController.OnAllPartsPlaced();
                }

                if (objectType == "Pan" && panController != null)
                {
                    panCompleted = true;
                    panController.OnAllPartsPlaced();
                }

                CheckVaseMicrowavePanCompleted();
            }

            if (!IsDishesGlass01Glass02Completed)
            {
                if (objectType == "Dishes" && dishesController != null)
                {
                    dishesCompleted = true;
                    dishesController.OnAllPartsPlaced();
                }

                if (objectType == "Glass01" && glass01Controller != null)
                {
                    glass01Completed = true;
                    glass01Controller.OnAllPartsPlaced();
                }

                if (objectType == "Glass02" && glass02Controller != null)
                {
                    glass02Completed = true;
                    glass02Controller.OnAllPartsPlaced();
                }

                CheckDishesGlass01Glass02Completed();
            }


            if (!IsGlass03Glass04Glass05Completed)
            {
                if (objectType == "Glass03" && glass03Controller != null)
                {
                    glass03Completed = true;
                    glass03Controller.OnAllPartsPlaced();
                }

                if (objectType == "Glass04" && glass04Controller != null)
                {
                    glass04Completed = true;
                    glass04Controller.OnAllPartsPlaced();
                }

                if (objectType == "Glass05" && glass05Controller != null)
                {
                    glass05Completed = true;
                    glass05Controller.OnAllPartsPlaced();
                }

                CheckGlass03Glass04Glass05Completed();
            }

        }


    }

    void CheckKitchenCookerFridgeObjectsCompleted()
    {
        if (kitchenCompleted && cookerCompleted && fridgeCompleted)
        {
            ActivateChair01Chair02Chair03();
            IsKitchenCookerFridgeCompleted = true;
        }
    }


    void ActivateChair01Chair02Chair03()
    {
        if (chair01Object != null && chair02Object != null && chair03Object != null)
        {
            chair01Object.SetActive(true);
            chair02Object.SetActive(true);
            chair03Object.SetActive(true);
            partManager.InitializeChair01Chair02Chair03Parts();

            // Move camera to new position
            if (cameraController != null)
            {
                cameraController.MoveCameraToTable();
            }
            else
            {
                Debug.LogError("CameraController is not available!");
            }

        }
        else
        {
            Debug.LogError("Washer object is not assigned in the GameManager!");
        }
    }

    public bool AreAnyPartsBeingPlaced()
    {
        return (placedParts["Kitchen"].Count > 0 && placedParts["Kitchen"].Count < totalParts) ||
               (placedParts["Cooker"].Count > 0 && placedParts["Cooker"].Count < totalParts) ||
               (placedParts["Fridge"].Count > 0 && placedParts["Fridge"].Count < totalParts) ||

               (placedParts["Chair01"].Count > 0 && placedParts["Chair01"].Count < totalParts) ||
               (placedParts["Chair02"].Count > 0 && placedParts["Chair02"].Count < totalParts) ||
               (placedParts["Chair03"].Count > 0 && placedParts["Chair03"].Count < totalParts) ||

               (placedParts["Chair04"].Count > 0 && placedParts["Chair04"].Count < totalParts) ||
               (placedParts["Table"].Count > 0 && placedParts["Table"].Count < totalParts) ||
               (placedParts["TableItems"].Count > 0 && placedParts["TableItems"].Count < totalParts) ||

               (placedParts["Vase"].Count > 0 && placedParts["Vase"].Count < totalParts) ||
               (placedParts["Microwave"].Count > 0 && placedParts["Microwave"].Count < totalParts) ||
               (placedParts["Pan"].Count > 0 && placedParts["Pan"].Count < totalParts) ||

               (placedParts["Dishes"].Count > 0 && placedParts["Dishes"].Count < totalParts) ||
               (placedParts["Glass01"].Count > 0 && placedParts["Glass01"].Count < totalParts) ||
               (placedParts["Glass02"].Count > 0 && placedParts["Glass02"].Count < totalParts) ||

               (placedParts["Glass03"].Count > 0 && placedParts["Glass03"].Count < totalParts) ||
               (placedParts["Glass04"].Count > 0 && placedParts["Glass04"].Count < totalParts) ||
               (placedParts["Glass05"].Count > 0 && placedParts["Glass05"].Count < totalParts);

    }


    void CheckChair01Chair02Chair03Completed()
    {
        if (chair01Completed && chair02Completed && chair03Completed)
        {
            ActivateChair04TableTableItems();
            IsChair01Chair02Chair03Completed = true;
        }
    }

    void ActivateChair04TableTableItems()
    {
        if (chair04Object != null && tableObject != null && tableItemsObject != null)
        {
            chair04Object.SetActive(true);
            tableObject.SetActive(true);
            tableItemsObject.SetActive(true);
            partManager.InitializeChair04TableTableItemsParts();

        }
        else
        {
            Debug.LogError("Chair04 object is not assigned in the GameManager!");
        }
    }

    void CheckChair04TableTableItemsCompleted()
    {
        if (chair04Completed && tableCompleted && tableItemsCompleted)
        {
            ActivateVaseMicrowavePan();
            IsChair04TableTableItemsCompleted = true;
        }
    }

    void ActivateVaseMicrowavePan()
    {
        if (vaseObject != null && microwaveObject != null && panObject != null)
        {
            vaseObject.SetActive(true);
            microwaveObject.SetActive(true);
            panObject.SetActive(true);
            partManager.InitializeVaseMicrowavePanParts();

            // Move camera to new position
            if (cameraController != null)
            {
                cameraController.MoveCameraToVase();
            }
            else
            {
                Debug.LogError("CameraController is not available!");
            }

        }
        else
        {
            Debug.LogError("Vase object is not assigned in the GameManager!");
        }
    }

    void CheckVaseMicrowavePanCompleted()
    {
        if (vaseCompleted && microwaveCompleted && panCompleted)
        {
            ActivateDishesGlass01Glass02();
            IsVaseMicrowavePanCompleted = true;
        }
    }

    void ActivateDishesGlass01Glass02()
    {
        if (dishesObject != null && glass01Object != null && glass02Object != null)
        {
            dishesObject.SetActive(true);
            glass01Object.SetActive(true);
            glass02Object.SetActive(true);
            partManager.InitializeDishesGlass01Glass02Parts();

            // Move camera to new position
            if (cameraController != null)
            {
                cameraController.MoveCameraToDishes();
            }
            else
            {
                Debug.LogError("CameraController is not available!");
            }

        }
        else
        {
            Debug.LogError("Dishes object is not assigned in the GameManager!");
        }
    }

    void CheckDishesGlass01Glass02Completed()
    {
        if (dishesCompleted && glass01Completed && glass02Completed)
        {
            ActivateGlass03Glass04Glass05();
            IsDishesGlass01Glass02Completed = true;
        }
    }

    void ActivateGlass03Glass04Glass05()
    {
        if (glass03Object != null && glass04Object != null && glass05Object != null)
        {
            glass03Object.SetActive(true);
            glass04Object.SetActive(true);
            glass05Object.SetActive(true);
            partManager.InitializeGlass03Glass04Glass05Parts();

        }
        else
        {
            Debug.LogError("Glass03 object is not assigned in the GameManager!");
        }
    }


    void CheckGlass03Glass04Glass05Completed()
    {
        if (glass03Completed && glass04Completed && glass05Completed)
        {
            IsGlass03Glass04Glass05Completed = true;
            if (cameraController != null)
            {
                cameraController.MoveCameraToFinal();
                infoManager.ShowPanel();
                particleManager.PlayFinalParticle();
            }
            else
            {
                Debug.LogError("CameraController is not available!");
            }
        }
    }

}