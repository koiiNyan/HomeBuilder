using UnityEngine;
using System.Collections.Generic;
using General;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Dictionary<string, List<DraggablePart>> placedParts = new Dictionary<string, List<DraggablePart>>();

    public InfoManager infoManager;

    //public int totalBedParts = 5;
    //public int totalBookshelfParts = 5;
    public int totalParts = 5;

    public BedController bedController;
    public BookshelfController bookshelfController;
    public GameObject deskObject; // Reference to the inactive desk in the scene

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
    public TVController tvController;

    private bool bedCompleted = false;
    private bool bookshelfCompleted = false;

    private bool deskCompleted = false;
    private bool chairCompleted = false;


    private bool rugCompleted = false;
    private bool posterCompleted = false;

    private bool sofaCompleted = false;
    private bool tvCompleted = false;

    private CameraController cameraController;
    public PartManager partManager;

    private bool IsBedBookCompleted = false;
    private bool IsDeskChairCompleted = false;
    private bool IsRugPosterCompleted = false;
    private bool IsSofaTVCompleted = false;


    // Public getters
    public bool GetBedCompleted()
    {
        Debug.Log($"GameManager: Getting bed completed state: {bedCompleted}");
        return bedCompleted;
    }
    public bool GetBookshelfCompleted() => bookshelfCompleted;
    public bool GetDeskCompleted() => deskCompleted;
    public bool GetChairCompleted() => chairCompleted;
    public bool GetRugCompleted() => rugCompleted;
    public bool GetPosterCompleted() => posterCompleted;
    public bool GetSofaCompleted() => sofaCompleted;
    public bool GetTVCompleted() => tvCompleted;
    public bool GetIsBedBookCompleted() => IsBedBookCompleted;
    public bool GetIsDeskChairCompleted() => IsDeskChairCompleted;
    public bool GetIsRugPosterCompleted() => IsRugPosterCompleted;
    public bool GetIsSofaTVCompleted() => IsSofaTVCompleted;


    // Public setters
    public void SetBedCompleted(bool value) => bedCompleted = value;
    public void SetBookshelfCompleted(bool value) => bookshelfCompleted = value;
    public void SetDeskCompleted(bool value) => deskCompleted = value;
    public void SetChairCompleted(bool value) => chairCompleted = value;
    public void SetRugCompleted(bool value) => rugCompleted = value;
    public void SetPosterCompleted(bool value) => posterCompleted = value;
    public void SetSofaCompleted(bool value) => sofaCompleted = value;
    public void SetTVCompleted(bool value) => tvCompleted = value;

    public void SetIsBedBookCompleted(bool value) => IsBedBookCompleted = value;
    public void SetIsDeskChairCompleted(bool value) => IsDeskChairCompleted = value;
    public void SetIsRugPosterCompleted(bool value) => IsRugPosterCompleted = value;
    public void SetIsSofaTVCompleted(bool value) => IsSofaTVCompleted = value;

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

        placedParts["Bed"] = new List<DraggablePart>();
        placedParts["Bookshelf"] = new List<DraggablePart>();
        placedParts["Desk"] = new List<DraggablePart>();
        placedParts["Chair"] = new List<DraggablePart>();
        placedParts["Rug"] = new List<DraggablePart>();
        placedParts["Poster"] = new List<DraggablePart>();
        placedParts["Sofa"] = new List<DraggablePart>();
        placedParts["TV"] = new List<DraggablePart>();

        FindCameraController();
    }

    void FindCameraController()
    {
        // First, try to find CameraRig in the scene
        GameObject cameraRig = GameObject.Find("CameraRig");
        if (cameraRig != null)
        {
            cameraController = cameraRig.GetComponent<CameraController>();
            /*if (cameraController != null)
            {
                Debug.Log("CameraController found on CameraRig");
                return;
            }*/
        }

        // If not found on CameraRig, try to find it in the scene
        cameraController = FindObjectOfType<CameraController>();
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


            if (!IsBedBookCompleted)
            {
                if (objectType == "Bed" && bedController != null)
                {
                    bedCompleted = true;
                    bedController.OnAllPartsPlaced();
                    Debug.Log("GameManager: Bed has been completed");                   
                }
                else if (objectType == "Bookshelf" && bookshelfController != null)
                {
                    bookshelfCompleted = true;
                    bookshelfController.OnAllPartsPlaced();
                }


                CheckBedBookshelfObjectsCompleted();
            }
            if (!IsDeskChairCompleted)
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
            }
        }


    }

    void CheckBedBookshelfObjectsCompleted()
    {
        if (bedCompleted && bookshelfCompleted)
        {
            ActivateDeskChair();
            IsBedBookCompleted = true;
        }
    }

    void ActivateDeskChair()
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
    }

    public bool AreAnyPartsBeingPlaced()
    {
        return (placedParts["Bed"].Count > 0 && placedParts["Bed"].Count < totalParts) ||
               (placedParts["Bookshelf"].Count > 0 && placedParts["Bookshelf"].Count < totalParts) ||

               (placedParts["Desk"].Count > 0 && placedParts["Desk"].Count < totalParts) ||
               (placedParts["Chair"].Count > 0 && placedParts["Chair"].Count < totalParts) ||

               (placedParts["Rug"].Count > 0 && placedParts["Rug"].Count < totalParts) ||
               (placedParts["Poster"].Count > 0 && placedParts["Poster"].Count < totalParts) ||

                (placedParts["Sofa"].Count > 0 && placedParts["Sofa"].Count < totalParts) ||
                (placedParts["TV"].Count > 0 && placedParts["TV"].Count < totalParts);
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

    void CheckDeskChairObjectsCompleted()
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
    }
}