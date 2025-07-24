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

    /*public GameObject washerObject; // Reference to the inactive desk in the scene
    public WasherController washerController;

    public GameObject carpetObject;
    public CarpetController carpetController;

    public GameObject towelObject;
    public TowelController towelController;

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

    /*private bool washerCompleted = false;
    private bool carpetCompleted = false;
    private bool towelCompleted = false;

    /*
    private bool rugCompleted = false;
    private bool posterCompleted = false;

    private bool sofaCompleted = false;
    private bool tvCompleted = false;*/

    private CameraController_Lvl03 cameraController;
    public PartManager_Lvl03 partManager;

    private bool IsKitchenCookerFridgeCompleted = false;
    //private bool IsWasherCarpetTowelCompleted = false;
    //private bool IsRugPosterCompleted = false;
    //private bool IsSofaTVCompleted = false;


    // Public getters
    public bool GetKitchenCompleted() => kitchenCompleted;
    public bool GetCookerCompleted() => cookerCompleted;
    public bool GetFridgeCompleted() => fridgeCompleted;

    /*public bool GetWasherCompleted() => washerCompleted;
    public bool GetCarpetCompleted() => carpetCompleted;
    public bool GetTowelCompleted() => towelCompleted;
    /*public bool GetSofaCompleted() => sofaCompleted;
    public bool GetTVCompleted() => tvCompleted;
    public bool GetIsBedBookCompleted() => IsBedBookCompleted;
    public bool GetIsDeskChairCompleted() => IsDeskChairCompleted;
    public bool GetIsRugPosterCompleted() => IsRugPosterCompleted; */
    public bool GetIsKitchenCookerFridgeCompleted() => IsKitchenCookerFridgeCompleted;
    //public bool GetIsWasherCarpetTowelCompleted() => IsWasherCarpetTowelCompleted;


    // Public setters
    public void SetKitchenCompleted(bool value) => kitchenCompleted = value;
    public void SetCookerCompleted(bool value) => cookerCompleted = value;
    public void SetFridgeCompleted(bool value) => fridgeCompleted = value;

    /*public void SetWasherCompleted(bool value) => washerCompleted = value;
    public void SetCarpetCompleted(bool value) => carpetCompleted = value;
    public void SetTowelCompleted(bool value) => towelCompleted = value;
    /*public void SetSofaCompleted(bool value) => sofaCompleted = value;
    public void SetTVCompleted(bool value) => tvCompleted = value;*/

    public void SetIsKitchenCookerFridgeCompleted(bool value) => IsKitchenCookerFridgeCompleted = value;
    //public void SetIsWasherCarpetTowelCompleted(bool value) => IsWasherCarpetTowelCompleted = value;
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

        /*placedParts["Washer"] = new List<DraggablePart>();
        placedParts["Carpet"] = new List<DraggablePart>();
        placedParts["Towel"] = new List<DraggablePart>();
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
            /*if (!IsWasherCarpetTowelCompleted)
            {
                if (objectType == "Washer" && washerController != null)
                {
                    washerCompleted = true;
                    washerController.OnAllPartsPlaced();
                }

                if (objectType == "Carpet" && carpetController != null)
                {
                    carpetCompleted = true;
                    carpetController.OnAllPartsPlaced();
                }

                if (objectType == "Towel" && towelController != null)
                {
                    towelCompleted = true;
                    towelController.OnAllPartsPlaced();
                }

                CheckWasherCarpetTowelObjectsCompleted();
            }*/

        }


    }

    void CheckKitchenCookerFridgeObjectsCompleted()
    {
        if (kitchenCompleted && cookerCompleted && fridgeCompleted)
        {
            //ActivateWasherCarpetTowel();
            IsKitchenCookerFridgeCompleted = true;
        }
    }

    /*void ActivateWasherCarpetTowel()
    {
        if (washerObject != null && carpetObject != null && towelObject != null)
        {
            washerObject.SetActive(true);
            carpetObject.SetActive(true);
            towelObject.SetActive(true);
            partManager.InitializeWasherCarpetTowelParts();

            // Move camera to new position
            if (cameraController != null)
            {
                cameraController.MoveCameraToWasher();
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
    }*/

    public bool AreAnyPartsBeingPlaced()
    {
        return (placedParts["Kitchen"].Count > 0 && placedParts["Kitchen"].Count < totalParts) ||
               (placedParts["Cooker"].Count > 0 && placedParts["Cooker"].Count < totalParts) ||
               (placedParts["Fridge"].Count > 0 && placedParts["Fridge"].Count < totalParts);

               /*(placedParts["Washer"].Count > 0 && placedParts["Washer"].Count < totalParts) ||
               (placedParts["Carpet"].Count > 0 && placedParts["Carpet"].Count < totalParts) ||
               (placedParts["Towel"].Count > 0 && placedParts["Towel"].Count < totalParts);*/

    }


    /*
    void CheckWasherCarpetTowelObjectsCompleted()
    {
        if (washerCompleted && carpetCompleted && towelCompleted)
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