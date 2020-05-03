using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySystem : MonoBehaviour
{
    public List<Button> displayButtons { get; set; } // Doit être remplie par le script d'instantiation des éléments

    public Transform content;

    public int startIndex;
    private int currentIndex = 0;

    private GameObject currentSelected;

    void Start()
    {
        currentIndex = startIndex;

        if (content.childCount != 0) for (int n = startIndex; n < content.childCount; n++) content.GetChild(n).gameObject.SetActive(false);
    }
    
    public void DisplayElement(GameObject bind)
    {
        for (int n = startIndex; n < content.childCount; n++) content.GetChild(n).gameObject.SetActive(false);
        
        bind.SetActive(true);
    }

    public void SelectElement(GameObject element)
    {
        if (currentSelected != null) currentSelected.SetActive(false);

        currentSelected = element;
        currentSelected.SetActive(true);
    }
}
