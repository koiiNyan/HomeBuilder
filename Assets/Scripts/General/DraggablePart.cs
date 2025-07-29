using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DraggablePart : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    public Vector3 initialPosition;
    private Vector3 initialScale;
    private Vector3 offset;

    [SerializeField]
    private RectTransform _targetArea;

    public RectTransform TargetArea
    {
        get { return _targetArea; }
        set
        {
            _targetArea = value;
            UpdateTargetArea();
        }
    }

    public float placementThreshold = 50f;
    [SerializeField]
    private Vector3 dragScale = new Vector3(1.9f, 15.125f, 1f);
    public string objectType = "Bed"; // Default to "Bed", set to "Bookshelf" for bookshelf parts

    public bool isPlaced = false;
    //private CameraController cameraController;

    private GameObject cameraRig;
    private ICameraController cameraController;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        FindCameraController();

        if (rectTransform == null) Debug.LogError("RectTransform not found on " + gameObject.name);
        if (canvas == null) Debug.LogError("Canvas not found in parent hierarchy of " + gameObject.name);
        if (canvasGroup == null) Debug.LogError("CanvasGroup not found on " + gameObject.name);

    }

    void UpdateTargetArea()
    {
        if (_targetArea == null)
        {
            Debug.LogWarning($"[{gameObject.name}] TargetArea is not assigned.");
        }
        //else
        //{
            //Debug.Log($"[{gameObject.name}] TargetArea assigned: {_targetArea.name}");
        //}
    }

    void Start()
    {
        initialScale = rectTransform.localScale;
        initialPosition = rectTransform.position;
       // Debug.Log($"[{gameObject.name}] Initial scale: {initialScale}, Initial position: {initialPosition}, TargetArea: {(_targetArea != null ? _targetArea.name : "Not Assigned")}");
    }

    void FindCameraController()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        GameObject cameraRig = GameObject.Find("CameraRig");

        if (cameraRig != null)
        {
            if (currentScene == "Level02")
            {
                cameraController = cameraRig.GetComponent<CameraController_Lvl02>();
            }

            else if (currentScene == "Level03")
            {
                cameraController = cameraRig.GetComponent<CameraController_Lvl03>();
            }

            else
            {
                cameraController = cameraRig.GetComponent<CameraController>();
            }
        }

        if (cameraController == null)
        {
            if (currentScene == "Level02")
            {
                cameraController = FindObjectOfType<CameraController_Lvl02>();
            }

            else if (currentScene == "Level03")
            {
                cameraController = FindObjectOfType<CameraController_Lvl03>();
            }

            else
            {
                cameraController = FindObjectOfType<CameraController>();
            }
        }

        if (cameraController == null)
        {
            Debug.LogError($"Camera controller not found in scene: {currentScene}");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlaced) return;

        if (cameraController == null)
        {
            Debug.LogError("Camera controller is null in OnBeginDrag. Attempting to find it again.");
            FindCameraController();
            if (cameraController == null) return;
        }

        if (!cameraController.IsDefaultZoom() || !cameraController.IsDefaultTransform())
        {
            cameraController.ShowWarning("Press F to reset zoom before dragging parts.");
            return;
        }

        if (rectTransform == null || canvas == null)
        {
            Debug.LogError("RectTransform or Canvas is null in OnBeginDrag");
            return;
        }

        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, canvas.worldCamera, out Vector3 worldPosition);
        offset = rectTransform.position - worldPosition;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }

        rectTransform.localScale = dragScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPlaced || cameraController == null || !cameraController.IsDefaultZoom() || !cameraController.IsDefaultTransform()) return;

        if (rectTransform == null || canvas == null)
        {
            Debug.LogError("RectTransform or Canvas is null in OnDrag");
            return;
        }

        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out Vector3 worldPosition);
        rectTransform.position = worldPosition + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPlaced || cameraController == null || !cameraController.IsDefaultZoom() || !cameraController.IsDefaultTransform()) return;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

        if (IsCorrectPlacement())
        {
           // AudioManager.Instance.PlaySoundEffect(0); TODO
            SnapToTarget();
            isPlaced = true;
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == "Level01" && GameManager.Instance != null)
            {
                GameManager.Instance.OnPartPlaced(this, objectType);
            }
            else if (currentScene == "Level02" && GameManager_Lvl02.Instance != null)
            {
                GameManager_Lvl02.Instance.OnPartPlaced(this, objectType);
            }

            else if (currentScene == "Level03" && GameManager_Lvl03.Instance != null)
            {
                GameManager_Lvl03.Instance.OnPartPlaced(this, objectType);
            }

            else
            {
                Debug.LogError("GameManager.Instance is null in OnEndDrag");
            }
        }
        else
        {
            rectTransform.position = initialPosition;
            rectTransform.localScale = initialScale;
        }
    }

    bool IsCorrectPlacement()
    {
        if (_targetArea == null)
        {
            Debug.LogWarning($"[{gameObject.name}] TargetArea is null, cannot check for correct placement");
            return false;
        }

        Vector3 targetPosition = _targetArea.position;
        float distance = Vector3.Distance(rectTransform.position, targetPosition);
        return distance <= placementThreshold;
    }

    void SnapToTarget()
    {
        if (_targetArea != null)
        {
            rectTransform.position = _targetArea.position;
        }
        else
        {
            Debug.LogWarning($"[{gameObject.name}] TargetArea is null, cannot snap to target");
        }
    }

    // Public method to get the target area
    public RectTransform GetTargetArea()
    {
        return _targetArea;
    }

    // Public method to set the target area
    public void SetTargetArea(RectTransform newTargetArea)
    {
        TargetArea = newTargetArea;
    }

}