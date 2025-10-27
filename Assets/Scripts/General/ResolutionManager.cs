using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    void Awake()
    {
        int targetWidth = 1920;
        int targetHeight = 1080;

        if (Screen.currentResolution.width > targetWidth || Screen.currentResolution.height > targetHeight)
        {
            Screen.SetResolution(targetWidth, targetHeight, false);
        }
    }
}
