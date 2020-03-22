using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SortMode
{
    public int numberOfTabs;
    public string[] namesOfTabs;
}

public class TabManager : MonoBehaviour
{
    public List<SortMode> sortModes;

    public GameObject[] pages;

    public GameObject tabPrefab;

    public GameObject tabParent;

    void Start()
    {
        CreateTabs(sortModes[0]);
    }
    
    void Update()
    {
        
    }

    public void CreateTabs(SortMode sortMode)
    {
        float offsetValue = -(tabParent.GetComponent<RectTransform>().sizeDelta.x / sortMode.numberOfTabs) / 2;

        for (int i = 0; i < sortMode.numberOfTabs; i++)
        {
            foreach (GameObject page in pages)
            {
                GameObject tabContent = Instantiate(new GameObject()) as GameObject;
                tabContent.transform.SetParent(page.transform);
                tabContent.AddComponent<RectTransform>();

                tabContent.GetComponent<RectTransform>().anchorMin = Vector2.zero;
                tabContent.GetComponent<RectTransform>().anchorMax = Vector2.one;

                tabContent.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                tabContent.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                tabContent.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

                tabContent.name = "Tab Content " + i.ToString();
            }

            GameObject tabButton = Instantiate(tabPrefab) as GameObject;
            tabButton.transform.SetParent(tabParent.transform);

            tabButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            tabButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetValue, -15);
            offsetValue += -(tabParent.GetComponent<RectTransform>().sizeDelta.x / sortMode.numberOfTabs);

            tabButton.transform.GetComponentInChildren<Text>().text = sortMode.namesOfTabs[i];
        }
    }
}
