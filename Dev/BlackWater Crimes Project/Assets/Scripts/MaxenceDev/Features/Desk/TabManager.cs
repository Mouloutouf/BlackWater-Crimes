using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Modes
{
    Location,
    Suspect,
    Type
}

[Serializable]
public class SortMode
{
    public string name;

    public Modes mode;
    public Color modeColor;

    public int numberOfTabs;
    public string[] namesOfTabs;
    
    [HideInInspector] public GameObject colorObject;
}

public class TabManager : MonoBehaviour
{
    private InstantiatePhotos desk;

    public List<SortMode> sortModes;
    public SortMode currentMode { get; private set; }
    private int mIndex;

    public GameObject content;
    public GameObject[] pages;
    public GameObject tabs;
    public GameObject modeTab;

    public GameObject tabButtonPrefab;
    public GameObject colorPrefab;

    public Color baseColor;
    public Color lightColor;
    public GameObject modeText;

    //[HideInInspector] public List<Dictionary<string, GameObject>> currentTabsContents = new List<Dictionary<string, GameObject>>(); // Almost useless

    [HideInInspector] public Dictionary<GameObject, List<GameObject>> tabsContents = new Dictionary<GameObject, List<GameObject>>();
    [HideInInspector] public List<GameObject> tabsObjects = new List<GameObject>();

    private int pageLayout = 2;

    private bool photosSet;

    void Start()
    {
        // Set up Mode Tab & Tabs (Buttons and Contents)

        desk = GetComponent<InstantiatePhotos>();
        
        CreateModeTab();

        CreateTabs(currentMode);
    }

    public void SetPhotosPosition()
    {
        foreach (GameObject tabObject in tabsObjects)
        {
            for (int q = 0; q < tabsContents[tabObject].Count; q++)
            {
                int index = 0;

                foreach (Transform photo in tabsContents[tabObject][q].transform)
                {
                    photo.GetComponent<RectTransform>().anchoredPosition = desk.spawnPoints[index];
                    index++;
                }
            }
        }
    }

    #region ModeTab
    public void CreateModeTab()
    {
        // Creates Colors for the Mode Tab

        float size = modeTab.GetComponent<RectTransform>().rect.width / sortModes.Count;
        float offset = size / 2;

        foreach (SortMode mode in sortModes)
        {
            mode.colorObject = Instantiate(colorPrefab);
            mode.colorObject.transform.SetParent(modeTab.transform);

            mode.colorObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            mode.colorObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset, -18);
            mode.colorObject.GetComponent<RectTransform>().sizeDelta = new Vector2(size, 4.3f);
            offset += size;

            mode.colorObject.GetComponent<RectTransform>().localPosition = new Vector3(
                mode.colorObject.GetComponent<RectTransform>().localPosition.x,
                mode.colorObject.GetComponent<RectTransform>().localPosition.y,
                0);

            mode.colorObject.GetComponent<Image>().color = baseColor;
        }

        // Sets the Mode Tab to the Current Mode

        currentMode = sortModes[0];

        currentMode.colorObject.GetComponent<Image>().color = currentMode.modeColor;
        modeText.GetComponent<Text>().text = currentMode.name;
    }
    #endregion

    #region Tabs
    public void CreateTabs(SortMode sortMode)
    {
        float offsetValue = -(tabs.GetComponent<RectTransform>().sizeDelta.x / sortMode.numberOfTabs) / 2;

        for (int i = 0; i < sortMode.numberOfTabs; i++)
        {
            // New Method : Tabs Content --> Child Pages Content

            GameObject tab_content = Instantiate(new GameObject());
            tab_content.transform.SetParent(content.transform);
            tab_content.name = sortMode.namesOfTabs[i] + " Tab Content";

            SetContent(tab_content);

            tabsContents.Add(tab_content, new List<GameObject>());
            tabsObjects.Add(tab_content);

            if (i != 0) tab_content.SetActive(false);
            
            for (int u = 0; u < pageLayout; u++)
            {
                GameObject pageContent = Instantiate(new GameObject());
                pageContent.transform.SetParent(tab_content.transform);
                pageContent.name = "Page Content " + (u + 1).ToString();

                SetContent(pageContent);

                pageContent.GetComponent<RectTransform>().offsetMin = pages[u].GetComponent<RectTransform>().offsetMin;
                pageContent.GetComponent<RectTransform>().offsetMax = pages[u].GetComponent<RectTransform>().offsetMax;

                tabsContents[tab_content].Add(pageContent);
            }
            
            // Creates the Tab Buttons

            GameObject tabButton = Instantiate(tabButtonPrefab);
            tabButton.transform.SetParent(tabs.transform);

            tabButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            tabButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetValue, -15);
            offsetValue += -(tabs.GetComponent<RectTransform>().sizeDelta.x / sortMode.numberOfTabs);

            tabButton.transform.GetComponentInChildren<Text>().text = sortMode.namesOfTabs[i];

            int value = i;
            tabButton.GetComponent<Button>().onClick.AddListener(delegate { ChangeTab(value); } );
            
            if (i != 0) tabButton.GetComponent<Image>().color = baseColor;
        }
    }

    public void SetContent(GameObject content)
    {
        content.AddComponent<RectTransform>();

        content.GetComponent<RectTransform>().anchorMin = Vector2.zero;
        content.GetComponent<RectTransform>().anchorMax = Vector2.one;

        content.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        content.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        content.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        content.GetComponent<RectTransform>().localPosition = new Vector3(
            content.GetComponent<RectTransform>().localPosition.x, 
            content.GetComponent<RectTransform>().localPosition.y, 
            0);
    }
    #endregion

    #region Change Mode
    public void ChangeMode()
    {
        mIndex++; // Increase Mode Index
        if (mIndex >= sortModes.Count) mIndex = 0; // If Last Mode, Set Mode Index to 0
        currentMode = sortModes[mIndex]; // Set Current Mode to current Mode Index

        foreach (Transform _tabButton in tabs.transform) Destroy(_tabButton.gameObject); // Destroy all Tab Buttons

        ResetPhotosToParent(); // Take all Photos, move them out of the Tab Contents, Destroy all Tab Contents
        
        CreateTabs(currentMode); // (Re)-Create Tab Buttons and Tab Contents relative to the Current Mode

        SetPhotosPosition();

        foreach (SortMode mode in sortModes) mode.colorObject.GetComponent<Image>().color = baseColor; // Reset the Mode Tab's Colors
        currentMode.colorObject.GetComponent<Image>().color = currentMode.modeColor; // Set Current Mode's Color to the Mode Tab
        modeText.GetComponent<Text>().text = currentMode.name; // Set Current Mode's Name to the Mode Tab
    }

    public void ResetPhotosToParent()
    {
        // New Version Tabs --> Pages

        foreach (GameObject _tabObject in tabsObjects)
        {
            for (int y = 0; y < tabsContents[_tabObject].Count; y++)
            {
                List<Transform> photosToMove = new List<Transform>();

                foreach (Transform tr in tabsContents[_tabObject][y].transform) photosToMove.Add(tr);
                foreach (Transform _tr in photosToMove) _tr.SetParent(content.transform);

                Destroy(tabsContents[_tabObject][y]);
            }

            tabsContents[_tabObject].Clear();

            Destroy(_tabObject);
        }

        tabsObjects.Clear();
    }
    #endregion

    #region Change Tab
    public void ChangeTab(int index)
    {
        // Deactivates all Tab Contents, Set Active selected Tab Content
        foreach (GameObject tab in tabsObjects) tab.SetActive(false);
        tabsObjects[index].SetActive(true);

        // Set Base Color to all Tab Buttons, Set Light Color to selected Tab Button
        foreach (Transform tr in tabs.transform) tr.GetComponent<Image>().color = baseColor;
        tabs.transform.GetChild(index).GetComponent<Image>().color = lightColor;
    }
    #endregion

    #region Old
    /*
    // Ancient Version, Pages --> Tabs

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
    
            // Ancient Method : Pages Content --> Child Tabs Content
            
            for (int v = 0; v < pages.Length; v++)
            {
                GameObject tabContent = Instantiate(new GameObject());
                tabContent.transform.SetParent(pages[v].transform);
                tabContent.name = sortMode.namesOfTabs[i] + " Tab Content";

                SetContent(tabContent);

                currentTabsContents[v].Add(sortMode.namesOfTabs[i], tabContent);

                tabContent.SetActive(false);
                if (i == 0) tabContent.SetActive(true);
            }

    // Old Switch Pos Photos

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
    */
    #endregion
}
