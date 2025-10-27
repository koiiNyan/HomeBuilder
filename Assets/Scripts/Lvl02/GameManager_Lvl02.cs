using UnityEngine;
using System.Collections.Generic;
using General;


public class GameManager_Lvl02 : MonoBehaviour
{
    public static GameManager_Lvl02 Instance;

    private Dictionary<string, List<DraggablePart>> placedParts = new Dictionary<string, List<DraggablePart>>();

    public InfoManager infoManager;
    public ParticleManager particleManager;

    public int totalParts = 10;

    public BathController bathController;
    public SinkController sinkController;
    public ToiletController toiletController;
    
    public GameObject washerObject; // Reference to the inactive desk in the scene
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

    private bool bathCompleted = false;
    private bool sinkCompleted = false;
    private bool toiletCompleted = false;

    private bool washerCompleted = false;
    private bool carpetCompleted = false;
    private bool towelCompleted = false;

    /*
    private bool rugCompleted = false;
    private bool posterCompleted = false;

    private bool sofaCompleted = false;
    private bool tvCompleted = false;*/

    private CameraController_Lvl02 cameraController;
    public PartManager_Lvl02 partManager;

    private bool IsBathSinkToiletCompleted = false;
    private bool IsWasherCarpetTowelCompleted = false;
    //private bool IsRugPosterCompleted = false;
    //private bool IsSofaTVCompleted = false;


    // Public getters
    public bool GetBathCompleted() => bathCompleted;
    public bool GetSinkCompleted() => sinkCompleted;
    public bool GetToiletCompleted() => toiletCompleted;

    public bool GetWasherCompleted() => washerCompleted;
    public bool GetCarpetCompleted() => carpetCompleted;
    public bool GetTowelCompleted() => towelCompleted;
    /*public bool GetSofaCompleted() => sofaCompleted;
    public bool GetTVCompleted() => tvCompleted;
    public bool GetIsBedBookCompleted() => IsBedBookCompleted;
    public bool GetIsDeskChairCompleted() => IsDeskChairCompleted;
    public bool GetIsRugPosterCompleted() => IsRugPosterCompleted; */
    public bool GetIsBathSinkToiletCompleted() => IsBathSinkToiletCompleted;
    public bool GetIsWasherCarpetTowelCompleted() => IsWasherCarpetTowelCompleted;


    // Public setters
    public void SetBathCompleted(bool value) => bathCompleted = value;
    public void SetSinkCompleted(bool value) => sinkCompleted = value;
    public void SetToiletCompleted(bool value) => toiletCompleted = value;

    public void SetWasherCompleted(bool value) => washerCompleted = value;
    public void SetCarpetCompleted(bool value) => carpetCompleted = value;
    public void SetTowelCompleted(bool value) => towelCompleted = value;
    /*public void SetSofaCompleted(bool value) => sofaCompleted = value;
    public void SetTVCompleted(bool value) => tvCompleted = value;*/

    public void SetIsBathSinkToiletCompleted(bool value) => IsBathSinkToiletCompleted = value;
    public void SetIsWasherCarpetTowelCompleted(bool value) => IsWasherCarpetTowelCompleted = value;
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

        placedParts["Bath"] = new List<DraggablePart>();
        placedParts["Sink"] = new List<DraggablePart>();
        placedParts["Toilet"] = new List<DraggablePart>();

        placedParts["Washer"] = new List<DraggablePart>();
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
            cameraController = cameraRig.GetComponent<CameraController_Lvl02>();
        }

        // If not found on CameraRig, try to find it in the scene
        cameraController = FindObjectOfType<CameraController_Lvl02>();
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


            if (!IsBathSinkToiletCompleted)
            {
                if (objectType == "Bath" && bathController != null)
                {
                    bathCompleted = true;
                    bathController.OnAllPartsPlaced();                
                }
                else if (objectType == "Sink" && sinkController != null)
                {
                    sinkCompleted = true;
                    sinkController.OnAllPartsPlaced();
                }
                else if (objectType == "Toilet" && toiletController != null)
                {
                    toiletCompleted = true;
                    toiletController.OnAllPartsPlaced();
                }


                CheckBathSinkToiletObjectsCompleted();
            }
            if (!IsWasherCarpetTowelCompleted)
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
            }
            
        }


    }

    void CheckBathSinkToiletObjectsCompleted()
    {
        if (bathCompleted && sinkCompleted && toiletCompleted)
        {
            ActivateWasherCarpetTowel();
            IsBathSinkToiletCompleted = true;
        }
    }

    void ActivateWasherCarpetTowel()
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
    }

    public bool AreAnyPartsBeingPlaced()
    {
        return (placedParts["Bath"].Count > 0 && placedParts["Bath"].Count < totalParts) ||
               (placedParts["Sink"].Count > 0 && placedParts["Sink"].Count < totalParts) ||
               (placedParts["Toilet"].Count > 0 && placedParts["Toilet"].Count < totalParts) ||

               (placedParts["Washer"].Count > 0 && placedParts["Washer"].Count < totalParts) ||
               (placedParts["Carpet"].Count > 0 && placedParts["Carpet"].Count < totalParts) ||
               (placedParts["Towel"].Count > 0 && placedParts["Towel"].Count < totalParts);

    }



    void CheckWasherCarpetTowelObjectsCompleted()
    {
        if (washerCompleted && carpetCompleted && towelCompleted)
        {
            IsWasherCarpetTowelCompleted = true;

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySoundEffect(6);
            }


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