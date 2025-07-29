using UnityEngine;
using System.Collections.Generic;
using General;


public class GameManager_Lvl03 : MonoBehaviour
{
    public static GameManager_Lvl03 Instance;

    private Dictionary<string, List<DraggablePart>> placedParts = new Dictionary<string, List<DraggablePart>>();

    public InfoManager infoManager;

    public int totalParts = 10;

    public KitchenController kitchenController;
    public CookerController cookerController;
    public FridgeController fridgeController;

    public GameObject chair01Object; 
    public Chair01Controller chair01Controller;

    public GameObject chair02Object;
    public Chair02Controller chair02Controller;

    public GameObject chair03Object;
    public Chair03Controller chair03Controller;

    /*
    public GameObject posterObject;
    public PosterController posterController;

    public GameObject sofaObject;
    public SofaController sofaController;

    public GameObject tvObject;
    public TVController tvController;*/

    private bool kitchenCompleted = false;
    private bool cookerCompleted = false;
    private bool fridgeCompleted = false;

    private bool chair01Completed = false;
    private bool chair02Completed = false;
    private bool chair03Completed = false;

    /*
    private bool rugCompleted = false;
    private bool posterCompleted = false;

    private bool sofaCompleted = false;
    private bool tvCompleted = false;*/

    private CameraController_Lvl03 cameraController;
    public PartManager_Lvl03 partManager;

    private bool IsKitchenCookerFridgeCompleted = false;
    private bool IsChair01Chair02Chair03Completed = false;
    //private bool IsRugPosterCompleted = false;
    //private bool IsSofaTVCompleted = false;


    // Public getters
    public bool GetKitchenCompleted() => kitchenCompleted;
    public bool GetCookerCompleted() => cookerCompleted;
    public bool GetFridgeCompleted() => fridgeCompleted;

    public bool GetChair01Completed() => chair01Completed;
    public bool GetChair02Completed() => chair02Completed;
    public bool GetChair03Completed() => chair03Completed;
    /*public bool GetSofaCompleted() => sofaCompleted;
    public bool GetTVCompleted() => tvCompleted;
    public bool GetIsBedBookCompleted() => IsBedBookCompleted;
    public bool GetIsDeskChairCompleted() => IsDeskChairCompleted;
    public bool GetIsRugPosterCompleted() => IsRugPosterCompleted; */
    public bool GetIsKitchenCookerFridgeCompleted() => IsKitchenCookerFridgeCompleted;
    public bool GetIsChair01Chair02Chair03Completed() => IsChair01Chair02Chair03Completed;


    // Public setters
    public void SetKitchenCompleted(bool value) => kitchenCompleted = value;
    public void SetCookerCompleted(bool value) => cookerCompleted = value;
    public void SetFridgeCompleted(bool value) => fridgeCompleted = value;

    public void SetChair01Completed(bool value) => chair01Completed = value;
    public void SetChair02Completed(bool value) => chair02Completed = value;
    public void SetChair03Completed(bool value) => chair03Completed = value;
    /*public void SetSofaCompleted(bool value) => sofaCompleted = value;
    public void SetTVCompleted(bool value) => tvCompleted = value;*/

    public void SetIsKitchenCookerFridgeCompleted(bool value) => IsKitchenCookerFridgeCompleted = value;
    public void SetIsChair01Chair02Chair03Completed(bool value) => IsChair01Chair02Chair03Completed = value;
    //public void SetIsRugPosterCompleted(bool value) => IsRugPosterCompleted = value;
    //public void SetIsSofaTVCompleted(bool value) => IsSofaTVCompleted = value;

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
        /*placedParts["Sofa"] = new List<DraggablePart>();
        placedParts["TV"] = new List<DraggablePart>();*/

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

                //CheckChair01Chair02Chair03Completed();
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
               (placedParts["Chair03"].Count > 0 && placedParts["Chair03"].Count < totalParts);

    }


    
    /*void CheckTableDishesGlass01ObjectsCompleted()
    {
        if (tableCompleted && dishesCompleted && glass01Completed)
        {
            IsWasherCarpetTowelCompleted = true;
            if (cameraController != null)
            {
                cameraController.MoveCameraToFinal();
                infoManager.ShowPanel();
            }
            else
            {
                Debug.LogError("CameraController is not available!");
            }
        }
    }*/

}