using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PartManager_Lvl03 : MonoBehaviour
{
    public RectTransform uiPanel;
    public GameObject[] partPrefabs;
    public RectTransform[] targetAreas;
    public GameObject[] chair01Chair02Chair03PartPrefabs;
    public RectTransform[] chair01Chair02Chair03TargetAreas;
    /*public GameObject[] rugPosterPartPrefabs;
    public RectTransform[] rugPosterTargetAreas;
    public GameObject[] sofaTVPartPrefabs;
    public RectTransform[] sofaTVTargetAreas;*/
    public Button leftButton;
    public Button rightButton;

    private List<GameObject> parts = new List<GameObject>();
    private List<GameObject> chair01Chair02Chair03Parts = new List<GameObject>();
    //private List<GameObject> rugPosterParts = new List<GameObject>();
    //private List<GameObject> sofaTVParts = new List<GameObject>();

    private int partsPerPage = 10;
    private int currentPage = 0;
    public List<GameObject> currentActiveList;

    private float[] slotXPositions = new float[] {
        143.4545f, 324.9091f, 506.3636f, 687.8182f, 869.2727f,
        1050.727f, 1232.182f, 1413.636f, 1595.091f, 1776.545f//,
        //143.4545f, 324.9091f, 506.3636f, 687.8182f, 869.2727f,
        //1050.727f, 1232.182f, 1413.636f, 1595.091f, 1776.545f,
        //143.4545f, 324.9091f, 506.3636f, 687.8182f, 869.2727f,
        //1050.727f, 1232.182f, 1413.636f, 1595.091f, 1776.545f,
    };

    void Start()
    {
        InitializeParts(partPrefabs, targetAreas, uiPanel, parts);
        currentActiveList = parts;
        UpdateButtonsVisibility();
        UpdateVisibleParts();

        if (leftButton != null) leftButton.onClick.AddListener(OnLeftButtonClick);
        if (rightButton != null) rightButton.onClick.AddListener(OnRightButtonClick);
    }

    void InitializeParts(GameObject[] prefabs, RectTransform[] areas, RectTransform panel, List<GameObject> partsList)
    {
        float totalWidth = panel.rect.width;
        float basePartWidth = 100f;

        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject partObject = Instantiate(prefabs[i], panel);
            RectTransform rectTransform = partObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0.5f);
            rectTransform.anchorMax = new Vector2(0, 0.5f);
            Vector3 scale = GetScaleForPart(partObject.name);
            rectTransform.localScale = scale;

            // Store the original position calculation for later use
            float partWidth = basePartWidth * scale.x;
            int pageIndex = i / partsPerPage;
            int positionInPage = i % partsPerPage;
            float spacing = (totalWidth - partWidth * partsPerPage) / (partsPerPage + 1);
            float currentX = spacing + (spacing + partWidth) * positionInPage + partWidth / 2f;

            rectTransform.anchoredPosition = new Vector2(currentX, 0);
            partsList.Add(partObject);

            if (i < areas.Length)
            {
                DraggablePart draggablePart = partObject.GetComponent<DraggablePart>();
                if (draggablePart != null)
                {
                    draggablePart.SetTargetArea(areas[i]);
                }
            }

            partObject.SetActive(false);
        }
    }

    public void UpdateVisibleParts()
    {

        int startIndex = currentPage * partsPerPage;

        // Hide all parts first
        foreach (GameObject part in currentActiveList)
        {
            //Debug.Log($"part= {part}!");

            if (part)
            {
                DraggablePart draggable = part.GetComponent<DraggablePart>();
                if (draggable != null && draggable.isPlaced == false)
                {
                    part.SetActive(false);
                }
            }
        }

        // Show only the current page's parts
        for (int i = startIndex; i < startIndex + partsPerPage && i < currentActiveList.Count; i++)
        {
            if (currentActiveList[i] != null)
            {
                currentActiveList[i].SetActive(true);
            }
        }

        UpdateButtonsVisibility();
    }

    void UpdateButtonsVisibility()
    {
        if (leftButton != null)
            leftButton.gameObject.SetActive(currentPage > 0);

        if (rightButton != null)
            rightButton.gameObject.SetActive((currentPage + 1) * partsPerPage < currentActiveList.Count);
    }

    void OnLeftButtonClick()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateVisibleParts();
        }
    }

    void OnRightButtonClick()
    {
        if ((currentPage + 1) * partsPerPage < currentActiveList.Count)
        {
            currentPage++;
            UpdateVisibleParts();
        }
    }

    public void InitializeChair01Chair02Chair03Parts()
    {
        currentPage = 0;
        InitializeParts(chair01Chair02Chair03PartPrefabs, chair01Chair02Chair03TargetAreas, uiPanel, chair01Chair02Chair03Parts);
        currentActiveList = chair01Chair02Chair03Parts;
        UpdateVisibleParts();
    }
    /*
         public void InitializeRugParts()
         {
             currentPage = 0;
             InitializeParts(rugPosterPartPrefabs, rugPosterTargetAreas, uiPanel, rugPosterParts);
             currentActiveList = rugPosterParts;
             UpdateVisibleParts();
         }

         public void InitializeSofaParts()
         {
             currentPage = 0;
             InitializeParts(sofaTVPartPrefabs, sofaTVTargetAreas, uiPanel, sofaTVParts);
             currentActiveList = sofaTVParts;
             UpdateVisibleParts();
         }*/

    Vector3 GetScaleForPart(string partName)
    {
        return new Vector3(0.76f, 6.05f, 1f);
    }


    public void UpdatePartList(GameObject prt)
    {
        currentActiveList.RemoveAll(s => s == prt || s.Equals(prt));
        UpdatePartsPosition();

    }

    void UpdatePartsPosition()
    {
        for (int i = 0; i < currentActiveList.Count; i++)
        {
            GameObject part = currentActiveList[i];
            
            if (part != null && !part.GetComponent<DraggablePart>().isPlaced)
            {
                RectTransform rectTransform = part.GetComponent<RectTransform>();

                int slotIndex = i % slotXPositions.Length;
                float xPos = slotXPositions[slotIndex];
                rectTransform.anchoredPosition = new Vector2(xPos, rectTransform.anchoredPosition.y);
            }
        }
    }
}