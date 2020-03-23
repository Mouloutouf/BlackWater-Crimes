using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Modes
{
    CrimeScene,
    Suspect,
    Type
}

[Serializable]
public class SortMode
{
    public string name;

    public int numberOfTabs;
    public string[] namesOfTabs;

    public Modes mode;

    public Color modeColor;
    [HideInInspector] public GameObject colorObject;
}

public class TabManager : MonoBehaviour
{
    private InstantiationProcessHubDesk desk;

    public List<SortMode> sortModes;
    public SortMode currentMode { get; private set; }
    private int mIndex;

    public GameObject[] pages;

    public GameObject tabPrefab;
    public GameObject colorPrefab;

    public Color colorBaseColor;

    public GameObject tabParent;

    [HideInInspector] public List<Dictionary<string, GameObject>> currentTabsContents = new List<Dictionary<string, GameObject>>();
    
    public GameObject modeText;
    public GameObject modeObject;

    void Start()
    {
        desk = GetComponent<InstantiationProcessHubDesk>();

        currentTabsContents.Add(new Dictionary<string, GameObject>());
        currentTabsContents.Add(new Dictionary<string, GameObject>());

        CreateModes();

        CreateTabs(currentMode);
    }
    
    public void SetPhotosPosition()
    {
        for (int y = 0; y < currentTabsContents.Count; y++)
        {
            foreach (GameObject tabContent in currentTabsContents[y].Values)
            {
                int index = 0;

                foreach (Transform photo in tabContent.transform)
                {
                    photo.GetComponent<RectTransform>().anchoredPosition = desk.spawnPoints[index];
                    index++;
                }
            }
        }
    }

    public void CreateModes()
    {
        float size = modeObject.GetComponent<RectTransform>().rect.width / sortModes.Count;
        float offset = size / 2;

        foreach (SortMode mode in sortModes)
        {
            mode.colorObject = Instantiate(colorPrefab);
            mode.colorObject.transform.SetParent(modeObject.transform);

            mode.colorObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            mode.colorObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, -18);
            mode.colorObject.GetComponent<RectTransform>().sizeDelta = new Vector2(size, 4.3f);
            offset += size;

            mode.colorObject.GetComponent<Image>().color = colorBaseColor;
        }

        currentMode = sortModes[0];

        currentMode.colorObject.GetComponent<Image>().color = currentMode.modeColor;
        modeText.GetComponent<Text>().text = currentMode.name;
    }

    public void CreateTabs(SortMode sortMode)
    {
        float offsetValue = -(tabParent.GetComponent<RectTransform>().sizeDelta.x / sortMode.numberOfTabs) / 2;

        for (int i = 0; i < sortMode.numberOfTabs; i++)
        {
            for (int v = 0; v < pages.Length; v++)
            {
                GameObject tabContent = Instantiate(new GameObject());
                tabContent.transform.SetParent(pages[v].transform);
                tabContent.AddComponent<RectTransform>();

                tabContent.GetComponent<RectTransform>().anchorMin = Vector2.zero;
                tabContent.GetComponent<RectTransform>().anchorMax = Vector2.one;

                tabContent.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                tabContent.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                tabContent.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

                tabContent.name = sortMode.namesOfTabs[i] + " Tab Content";

                tabContent.SetActive(false);
                
                currentTabsContents[v].Add(sortMode.namesOfTabs[i], tabContent);
                
                if (i == 0) tabContent.SetActive(true);
            }

            GameObject tabButton = Instantiate(tabPrefab);
            tabButton.transform.SetParent(tabParent.transform);

            tabButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            tabButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetValue, -15);
            offsetValue += -(tabParent.GetComponent<RectTransform>().sizeDelta.x / sortMode.numberOfTabs);

            tabButton.transform.GetComponentInChildren<Text>().text = sortMode.namesOfTabs[i];

            if (i != 0) tabButton.GetComponent<Image>().color = colorBaseColor;
        }
    }

    public void ChangeMode()
    {
        mIndex++;
        if (mIndex >= sortModes.Count) mIndex = 0;
        currentMode = sortModes[mIndex];

        foreach (Transform childTab in tabParent.transform) Destroy(childTab.gameObject);

        ResetPhotosToParent();
        
        CreateTabs(currentMode);

        foreach (SortMode mode in sortModes) mode.colorObject.GetComponent<Image>().color = colorBaseColor;
        currentMode.colorObject.GetComponent<Image>().color = currentMode.modeColor;
        modeText.GetComponent<Text>().text = currentMode.name;
    }

    public void ChangeTab()
    {

    }

    public void ResetPhotosToParent()
    {
        for (int x = 0; x < currentTabsContents.Count; x++)
        {
            foreach (GameObject _tabContent in currentTabsContents[x].Values)
            {
                List<Transform> objectsToMove = new List<Transform>();

                foreach (Transform tr in _tabContent.transform) objectsToMove.Add(tr);
                foreach (Transform _tr in objectsToMove) _tr.SetParent(pages[0].transform);

                Destroy(_tabContent);
            }

            currentTabsContents[x].Clear();
        }
    }
}
