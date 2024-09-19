using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public int targetWidth = 1920;
    public int targetHeight = 1080;

    void Start()
    {
        DontDestroyOnLoad(gameObject); 
        SetResolution();
    }

    void SetResolution()
    {
        // Get the current screen resolution
        Resolution currentResolution = Screen.currentResolution;

        // Determine if we should use fullscreen or windowed mode
        bool useFullscreen = currentResolution.width <= targetWidth || currentResolution.height <= targetHeight;

        // Set the resolution and screen mode
        Screen.SetResolution(targetWidth, targetHeight, useFullscreen);

        if (!useFullscreen)
        {
            // If in windowed mode, set to windowed mode explicitly
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        // Ensure the aspect ratio is maintained
        float targetAspect = (float)targetWidth / targetHeight;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = Camera.main.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            Camera.main.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = Camera.main.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            Camera.main.rect = rect;
        }
    }

    void OnApplicationFocus(bool focusStatus)
    {
        if (focusStatus)
        {
            SetResolution();
        }
    }
}
