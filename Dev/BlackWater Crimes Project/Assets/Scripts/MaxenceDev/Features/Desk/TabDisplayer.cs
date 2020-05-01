using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Tabs
{
    public GameObject tab;
    public GameObject content;
}

public class TabDisplayer : MonoBehaviour
{
    public List<Tabs> tabsList = new List<Tabs>();

    public Color darkColor;

    void Start()
    {
        SwitchTab(0);
    }

    public void SwitchTab(int index)
    {
        foreach (Tabs _tab in tabsList)
        {
            foreach (Transform tr in _tab.content.transform) { tr.gameObject.SetActive(false); }
            _tab.tab.GetComponent<Image>().color = darkColor;
        }

        foreach (Transform tr in tabsList[index].content.transform) { tr.gameObject.SetActive(true); }
        tabsList[index].tab.GetComponent<Image>().color = Color.white;
    }
}
