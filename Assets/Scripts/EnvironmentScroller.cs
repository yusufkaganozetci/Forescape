using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScroller : MonoBehaviour
{
    [SerializeField] float scrollingSpeed;
    [SerializeField] RectTransform[] environmentParts;
    private int farLeftIndex = 0;
    private int currentSeemIndex = 2;
    private int farRightIndex;
    private bool placementNeeded = false;
    private Canvas canvas;

    public int GetCurrentSeemIndex()
    {
        return currentSeemIndex;
    }
    void Start()
    {
        farRightIndex = environmentParts.Length - 1;
        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckPlacementNeeded();
        ScrollEnvironment();
        //AstarPath.active.Scan();
        
    }

    private IEnumerator UpdatePath()
    {
        while (true)
        {
            AstarPath.active.Scan();
            yield return new WaitForSeconds(0.1f);
            yield return new WaitForEndOfFrame();
        }
        
    }
    private void CheckPlacementNeeded()
    {
        if (environmentParts[farLeftIndex].anchoredPosition.x <= -3840)
        {
            placementNeeded = true;
        }
        if(environmentParts[currentSeemIndex].anchoredPosition.x <= 960)
        {
            currentSeemIndex++;
            if (currentSeemIndex == environmentParts.Length)
            {
                currentSeemIndex = 0;
            }
        }
    }
    private void ScrollEnvironment()
    {
        if (placementNeeded)
        {
            placementNeeded = false;
            ReplaceRoad();
        }
        for(int i = 0; i < environmentParts.Length; i++)
        {
            environmentParts[i].anchoredPosition -= new Vector2(scrollingSpeed * Time.deltaTime, 0);
        }
        
    }

    private void ReplaceRoad()
    {
        
        environmentParts[farLeftIndex].anchoredPosition = environmentParts[farRightIndex].anchoredPosition + new Vector2(1920, 0);
        farRightIndex = farLeftIndex;
        farLeftIndex += 1;
        if(farLeftIndex == environmentParts.Length)
        {
            farLeftIndex = 0;
        }
    }
}
