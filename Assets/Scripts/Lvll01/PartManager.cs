using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PartManager : MonoBehaviour
{
    public RectTransform uiPanel;

    public GameObject[] partPrefabs;
    public RectTransform[] targetAreas; // New array for target areas

    public GameObject[] deskChairPartPrefabs;
    public RectTransform[] deskChairTargetAreas;

    public GameObject[] rugPosterPartPrefabs;
    public RectTransform[] rugPosterTargetAreas;

    public GameObject[] sofaTVPartPrefabs;
    public RectTransform[] sofaTVTargetAreas;

    private List<GameObject> parts = new List<GameObject>();
    private List<GameObject> deskChairParts = new List<GameObject>();
    private List<GameObject> rugPosterParts = new List<GameObject>();
    private List<GameObject> sofaTVParts = new List<GameObject>();


    void Start()
    {
        //Debug.Log($"Initializing {partPrefabs.Length} parts");
        InitializeParts(partPrefabs, targetAreas, uiPanel, parts);
        UpdateVisibleParts(parts);
    }

    public void InitializeDeskParts()
    {
        //Debug.Log($"Initializing {deskChairPartPrefabs.Length} desk parts");
        InitializeParts(deskChairPartPrefabs, deskChairTargetAreas, uiPanel, deskChairParts);
        UpdateVisibleParts(deskChairParts);
    }

    public void InitializeRugParts()
    {
        //Debug.Log($"Initializing {rugPosterPartPrefabs.Length} desk parts");
        InitializeParts(rugPosterPartPrefabs, rugPosterTargetAreas, uiPanel, rugPosterParts);
        UpdateVisibleParts(rugPosterParts);
    }

    public void InitializeSofaParts()
    {
        //Debug.Log($"Initializing {rugPosterPartPrefabs.Length} desk parts");
        InitializeParts(sofaTVPartPrefabs, sofaTVTargetAreas, uiPanel, sofaTVParts);
        UpdateVisibleParts(sofaTVParts);
    }

    void InitializeParts(GameObject[] prefabs, RectTransform[] areas, RectTransform panel, List<GameObject> partsList)
    {
        float totalWidth = panel.rect.width;
        float basePartWidth = 100f; // Base width for scaling
        float currentX = 0f;

        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject partObject = Instantiate(prefabs[i], panel);
            RectTransform rectTransform = partObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0.5f);
            rectTransform.anchorMax = new Vector2(0, 0.5f);

            Vector3 scale = GetScaleForPart(partObject.name);
            rectTransform.localScale = scale;

            float partWidth = basePartWidth * scale.x;
            float spacing = (totalWidth - partWidth * prefabs.Length) / (prefabs.Length + 1);

            currentX += spacing + partWidth / 2f;
            rectTransform.anchoredPosition = new Vector2(currentX, 0);
            currentX += partWidth / 2f;

            partsList.Add(partObject);

            // Assign target area if available
            if (i < areas.Length)
            {
                DraggablePart draggablePart = partObject.GetComponent<DraggablePart>();
                if (draggablePart != null)
                {
                    draggablePart.SetTargetArea(areas[i]);
                    //Debug.Log($"Assigned target area to part {i}: {areas[i].name}");
                }
                else
                {
                    Debug.LogWarning($"DraggablePart component not found on part {i}");
                }
            }
            else
            {
                Debug.LogWarning($"No target area available for part {i}");
            }

            //Debug.Log($"Part {i} position: {rectTransform.anchoredPosition}, scale: {scale}");
        }
    }

    Vector3 GetScaleForPart(string partName)
    {
        // Your existing GetScaleForPart logic here
        return new Vector3(0.76f, 6.05f, 1f);
    }

    void UpdateVisibleParts(List<GameObject> partsList)
    {
        for (int i = 0; i < partsList.Count; i++)
        {
            partsList[i].SetActive(true);
        }
    }
}
