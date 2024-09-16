using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Dictionary<string, List<DraggablePart>> placedParts = new Dictionary<string, List<DraggablePart>>();

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
                    bedController.OnAllPartsPlaced();
                    bedCompleted = true;
                }
                else if (objectType == "Bookshelf" && bookshelfController != null)
                {
                    bookshelfController.OnAllPartsPlaced();
                    bookshelfCompleted = true;
                }


                CheckBedBookshelfObjectsCompleted();
            }
            if (!IsDeskChairCompleted)
            {
                if (objectType == "Desk" && deskController != null)
                {
                    deskController.OnAllPartsPlaced();
                    deskCompleted = true;
                }

                if (objectType == "Chair" && chairController != null)
                {
                    chairController.OnAllPartsPlaced();
                    chairCompleted = true;
                }

                CheckDeskChairObjectsCompleted();
            }

            if (!IsRugPosterCompleted)
            {
                if (objectType == "Rug" && rugController != null)
                {
                    rugController.OnAllPartsPlaced();
                    rugCompleted = true;
                }

                if (objectType == "Poster" && posterController != null)
                {
                    posterController.OnAllPartsPlaced();
                    posterCompleted = true;
                }

                CheckRugPosterObjectsCompleted();
            }

            if (!IsSofaTVCompleted)
            {
                if (objectType == "Sofa" && sofaController != null)
                {
                    sofaController.OnAllPartsPlaced();
                    sofaCompleted = true;
                }

                if (objectType == "TV" && tvController != null)
                {
                    tvController.OnAllPartsPlaced();
                    tvCompleted = true;
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
            }
            else
            {
                Debug.LogError("CameraController is not available!");
            }
        }
    }
}
