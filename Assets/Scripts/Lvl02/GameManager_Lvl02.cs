using UnityEngine;
using System.Collections.Generic;
using General;


public class GameManager_Lvl02 : MonoBehaviour
{
    public static GameManager_Lvl02 Instance;

    private Dictionary<string, List<DraggablePart>> placedParts = new Dictionary<string, List<DraggablePart>>();

    public InfoManager infoManager;

    //public int totalBedParts = 5;
    //public int totalBookshelfParts = 5;
    public int totalParts = 10;

    public BathController bathController;
    public SinkController sinkController;
    public ToiletController toiletController;
    /*public GameObject deskObject; // Reference to the inactive desk in the scene

    public DeskController deskController;

    public GameObject chairObject;
    public ChairController chairController;

    public GameObject rugObject;
    public RugController rugController;

    public GameObject posterObject;
    public PosterController posterController;

    public GameObject sofaObject;
    public SofaController sofaController;

    public GameObject tvObject;
    public TVController tvController;*/

    private bool bathCompleted = false;
    private bool sinkCompleted = false;
    private bool toiletCompleted = false;

    /*private bool deskCompleted = false;
    private bool chairCompleted = false;


    private bool rugCompleted = false;
    private bool posterCompleted = false;

    private bool sofaCompleted = false;
    private bool tvCompleted = false;*/

    private CameraController_Lvl02 cameraController;
    public PartManager_Lvl02 partManager;

    private bool IsBathSinkToiletCompleted = false;
    //private bool IsDeskChairCompleted = false;
    //private bool IsRugPosterCompleted = false;
    //private bool IsSofaTVCompleted = false;


    // Public getters
    public bool GetBathCompleted() => bathCompleted;
    public bool GetSinkCompleted() => sinkCompleted;
    public bool GetToiletCompleted() => toiletCompleted;
    /*public bool GetChairCompleted() => chairCompleted;
    public bool GetRugCompleted() => rugCompleted;
    public bool GetPosterCompleted() => posterCompleted;
    public bool GetSofaCompleted() => sofaCompleted;
    public bool GetTVCompleted() => tvCompleted;
    public bool GetIsBedBookCompleted() => IsBedBookCompleted;
    public bool GetIsDeskChairCompleted() => IsDeskChairCompleted;
    public bool GetIsRugPosterCompleted() => IsRugPosterCompleted;
    public bool GetIsSofaTVCompleted() => IsSofaTVCompleted;*/


    // Public setters
    public void SetBathCompleted(bool value) => bathCompleted = value;
    public void SetSinkCompleted(bool value) => sinkCompleted = value;
    public void SetToiletCompleted(bool value) => toiletCompleted = value;
    /*public void SetChairCompleted(bool value) => chairCompleted = value;
    public void SetRugCompleted(bool value) => rugCompleted = value;
    public void SetPosterCompleted(bool value) => posterCompleted = value;
    public void SetSofaCompleted(bool value) => sofaCompleted = value;
    public void SetTVCompleted(bool value) => tvCompleted = value;*/

    public void SetIsBathSinkToiletCompleted(bool value) => IsBathSinkToiletCompleted = value;
    //public void SetIsDeskChairCompleted(bool value) => IsDeskChairCompleted = value;
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
       /* placedParts["Chair"] = new List<DraggablePart>();
        placedParts["Rug"] = new List<DraggablePart>();
        placedParts["Poster"] = new List<DraggablePart>();
        placedParts["Sofa"] = new List<DraggablePart>();
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
            /*if (cameraController != null)
            {
                Debug.Log("CameraController found on CameraRig");
                return;
            }*/
        }

        // If not found on CameraRig, try to find it in the scene
        cameraController = FindObjectOfType<CameraController_Lvl02>();
        if (cameraController == null)
        {
            // Debug.Log("CameraController found in the scene");

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
            /*if (!IsDeskChairCompleted)
            {
                if (objectType == "Desk" && deskController != null)
                {
                    deskCompleted = true;
                    deskController.OnAllPartsPlaced();
                }

                if (objectType == "Chair" && chairController != null)
                {
                    chairCompleted = true;
                    chairController.OnAllPartsPlaced();
                }

                CheckDeskChairObjectsCompleted();
            }

            if (!IsRugPosterCompleted)
            {
                if (objectType == "Rug" && rugController != null)
                {
                    rugCompleted = true;
                    rugController.OnAllPartsPlaced();
                }

                if (objectType == "Poster" && posterController != null)
                {
                    posterCompleted = true;
                    posterController.OnAllPartsPlaced();
                }

                CheckRugPosterObjectsCompleted();
            }

            if (!IsSofaTVCompleted)
            {
                if (objectType == "Sofa" && sofaController != null)
                {
                    sofaCompleted = true;
                    sofaController.OnAllPartsPlaced();
                }

                if (objectType == "TV" && tvController != null)
                {
                    tvCompleted = true;
                    tvController.OnAllPartsPlaced();
                }

                CheckSofaTVObjectsCompleted();
            }*/
        }


    }

    void CheckBathSinkToiletObjectsCompleted()
    {
        if (bathCompleted && sinkCompleted && toiletCompleted)
        {
            //ActivateDeskChair();
            IsBathSinkToiletCompleted = true;
        }
    }

    /*void ActivateDeskChair()
    {
        if (deskObject != null && chairObject != null)
        {
            deskObject.SetActive(true);
            chairObject.SetActive(true);
            partManager.InitializeDeskParts();


            // Move camera to new position
            if (cameraController != null)
            {
                cameraController.MoveCameraToDesk();
            }
            else
            {
                Debug.LogError("CameraController is not available!");
            }
        }
        else
        {
            Debug.LogError("Desk object is not assigned in the GameManager!");
        }
    }*/

    public bool AreAnyPartsBeingPlaced()
    {
        return (placedParts["Bath"].Count > 0 && placedParts["Bath"].Count < totalParts) ||
               (placedParts["Sink"].Count > 0 && placedParts["Sink"].Count < totalParts) ||
               (placedParts["Toilet"].Count > 0 && placedParts["Toilet"].Count < totalParts);

               /*(placedParts["Desk"].Count > 0 && placedParts["Desk"].Count < totalParts) ||
               (placedParts["Chair"].Count > 0 && placedParts["Chair"].Count < totalParts) ||

               (placedParts["Rug"].Count > 0 && placedParts["Rug"].Count < totalParts) ||
               (placedParts["Poster"].Count > 0 && placedParts["Poster"].Count < totalParts) ||

                (placedParts["Sofa"].Count > 0 && placedParts["Sofa"].Count < totalParts) ||
                (placedParts["TV"].Count > 0 && placedParts["TV"].Count < totalParts);*/
    }

    /*public void ResetPlacedParts()
    {
        placedParts["Bed"].Clear();
        placedParts["Bookshelf"].Clear();
        placedParts["Desk"].Clear();
        bedCompleted = false;
        bookshelfCompleted = false;
        deskCompleted = false;
    }*/

    /*void CheckDeskChairObjectsCompleted()
    {
        if (deskCompleted && chairCompleted)
        {
            ActivateRugPoster();
            IsDeskChairCompleted = true;
        }
    }

    void ActivateRugPoster()
    {
        if (rugObject != null && posterObject != null)
        {
            rugObject.SetActive(true);
            posterObject.SetActive(true);
            partManager.InitializeRugParts();


            // Move camera to new position
            if (cameraController != null)
            {
                cameraController.MoveCameraToRug();
            }
            else
            {
                Debug.LogError("CameraController is not available!");
            }
        }
        else
        {
            Debug.LogError("Desk object is not assigned in the GameManager!");
        }
    }

    void CheckRugPosterObjectsCompleted()
    {
        if (rugCompleted && posterCompleted)
        {
            ActivateSofaTV();
            IsRugPosterCompleted = true;
        }
    }

    void ActivateSofaTV()
    {
        if (sofaObject != null && tvObject != null)
        {
            sofaObject.SetActive(true);
            tvObject.SetActive(true);
            partManager.InitializeSofaParts();


            // Move camera to new position
            if (cameraController != null)
            {
                cameraController.MoveCameraToSofa();
            }
            else
            {
                Debug.LogError("CameraController is not available!");
            }
        }
        else
        {
            Debug.LogError("Desk object is not assigned in the GameManager!");
        }
    }

    void CheckSofaTVObjectsCompleted()
    {
        if (sofaCompleted && tvCompleted)
        {
            //ActivateDeskChair();
            IsSofaTVCompleted = true;
            // Move camera to new position
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