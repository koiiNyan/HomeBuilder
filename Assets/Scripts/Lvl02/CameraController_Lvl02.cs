using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CameraController_Lvl02 : MonoBehaviour, ICameraController
{
    public float zoomSpeed = 1f;
    public float minZoom = 2f;
    public float maxZoom = 10f;
    public float defaultZoom = 5f;
    public float cameraMovementSpeed = 10f;
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI dragModeText; // New variable for the drag mode text
    public float warningDisplayTime = 2f;
    private Camera cam;
    public Camera particleCam;

    private float warningTimer;
    private Vector3 cameraMovement;
    public Vector3 defaultTransform = new Vector3(6.1f, 16.73f, -10f);
    public Vector3 washerCameraPosition = new Vector3(6.11f, 13.4f, -10f);
    public float cameraMoveSpeed = 2f;
    private bool isTransitioning = false;

    public Vector3 finalPosition = new Vector3(5.7f, 16.7f,-10.5f);

    public interface ICameraController
    {
        bool IsDefaultZoom();
        bool IsDefaultTransform();
        void ShowWarning(string message);
    }

    void Start()
    {
        cam = Camera.main;
        cam.orthographicSize = defaultZoom; // Ensure the camera is set to the default zoom size
        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
        }
        UpdateDragModeText(); // Initial update of drag mode text
    }

    void Update()
    {
        UpdateDragModeText();


        if (!isTransitioning )//&& defaultTransform != finalPosition)
        {

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                if (CanZoom())
                {
                    float newOrthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom);
                    cam.orthographicSize = newOrthographicSize;
                   // UpdateDragModeText(); // Update text when zoom changes
                }
                else
                {
                    ShowWarning("Camera is disabled while parts are being placed.");
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                ResetZoom();
               // UpdateDragModeText(); // Update text when zoom is reset
            }

            // Handle camera movement
            if (CanMove())
            {
                cameraMovement.x = Input.GetAxis("Horizontal") * cameraMovementSpeed;
                cameraMovement.y = Input.GetAxis("Vertical") * cameraMovementSpeed;
                transform.Translate(cameraMovement * Time.deltaTime);
            }
            else
            {
                //ShowWarning("Camera is disabled while parts are being placed.");
                cameraMovement = Vector3.zero;
            }
        }

        if (warningTimer > 0)
        {
            warningTimer -= Time.deltaTime;
            if (warningTimer <= 0)
            {
                HideWarning();
            }
        }
    }

    bool CanZoom()
    {
        return !isTransitioning && !GameManager_Lvl02.Instance.AreAnyPartsBeingPlaced();
    }

    bool CanMove()
    {
        return !isTransitioning && !GameManager_Lvl02.Instance.AreAnyPartsBeingPlaced();
    }

    public void ShowWarning(string message)
    {
        if (warningText != null)
        {
            warningText.text = message;
            warningText.gameObject.SetActive(true);
            warningTimer = warningDisplayTime;
        }
    }

    void HideWarning()
    {
        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
        }
    }

    public void ResetZoom()
    {
        cam.orthographicSize = defaultZoom;
        //transform.position = new Vector3(10, 18, -10);
        transform.position = defaultTransform;
    }

    public bool IsDefaultZoom()
    {
        return Mathf.Approximately(cam.orthographicSize, defaultZoom);
    }

    public bool IsDefaultTransform()
    {
        return Mathf.Approximately(transform.position.x, defaultTransform.x) &&
               Mathf.Approximately(transform.position.y, defaultTransform.y) &&
               Mathf.Approximately(transform.position.z, defaultTransform.z);
    }


    void UpdateDragModeText()
    {
        if (dragModeText != null)
        {
            dragModeText.text = CanMove() && CanZoom() ? "Camera Mode" : "Camera Locked";
        }
    }

    public void MoveCameraToWasher()
    {
        StartCoroutine(MoveCameraCoroutine(washerCameraPosition, false));
    }

    private IEnumerator MoveCameraCoroutine(Vector3 position, bool needResize)
    {
        isTransitioning = true;
        Vector3 startPosition = transform.position;
        float journeyLength = Vector3.Distance(startPosition, position);
        float startTime = Time.time;

        float startSize = cam.orthographicSize;
        float targetSize = 10f;

        while (transform.position != position)
        {
            float distanceCovered = (Time.time - startTime) * cameraMoveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, position, fractionOfJourney);

            if (needResize)
            {
                cam.orthographicSize = Mathf.Lerp(startSize, targetSize, fractionOfJourney);
                particleCam.orthographicSize = Mathf.Lerp(startSize, targetSize, fractionOfJourney);
            }

            yield return null;
        }

        isTransitioning = false;
        defaultTransform = position; // Update the default transform
    }

    public void MoveCameraToFinal()
    {
        StartCoroutine(MoveCameraCoroutine(finalPosition, true));
    }

}