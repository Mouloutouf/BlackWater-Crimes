using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PhotoObject : ObjectData<Evidence>
{
    public GameObject photosBooklet;
    
    public int pageNumber;
    private SortMode currentMode;
    
    public GameObject imageObject;

    private Evidence myType;

    void Start()
    {
        //LoadDataOfType(myType);
    }

    public override void Protocol()
    {
        imageObject.GetComponent<Image>().sprite = data.photo;

        base.Protocol();
    }

    void Update()
    {
        // Data Protocol

        if (!loaded)
        {
            Protocol();
        }

        // Tab Organisation

        currentMode = photosBooklet.GetComponent<TabManager>().currentMode;
        
        TabManager tabScript = photosBooklet.GetComponent<TabManager>();
        transform.SetParent(tabScript.tabsContents[tabScript.tabsObjects[GetTabParent(true)]][pageNumber].transform);
    }
    
    private string GetTabParent()
    {
        string word = "";

        if (currentMode.mode == Modes.CrimeScene)
        {
            if (data.modeCategory.crimeScene == Locations.Docks) word = "Docks";
            else if (data.modeCategory.crimeScene == Locations.Whorehouse) word = "Bordel";
            else if (data.modeCategory.crimeScene == Locations.MayorHouse) word = "Maison";
        }
        else if (currentMode.mode == Modes.Suspect)
        {
            if (data.modeCategory.suspect == Characters.Anna) word = "Anna";
            else if (data.modeCategory.suspect == Characters.Jack) word = "Jack";
            else if (data.modeCategory.suspect == Characters.Oliver) word = "Oliver";
        }
        else if (currentMode.mode == Modes.Type)
        {
            if (data.modeCategory.type == Types.Organic) word = "Organic";
            else if (data.modeCategory.type == Types.Ballistic) word = "Ballistic";
            else if (data.modeCategory.type == Types.Other) word = "Other";
        }

        return word;
    }

    private int GetTabParent(bool yes)
    {
        if (currentMode.mode == Modes.CrimeScene)
        {
            if (data.modeCategory.crimeScene == Locations.Docks) return 0;
            else if (data.modeCategory.crimeScene == Locations.Whorehouse) return 1;
            else if (data.modeCategory.crimeScene == Locations.MayorHouse) return 2;
            else return 0;
        }
        else if (currentMode.mode == Modes.Suspect)
        {
            if (data.modeCategory.suspect == Characters.Anna) return 0;
            else if (data.modeCategory.suspect == Characters.Jack) return 1;
            else if (data.modeCategory.suspect == Characters.Oliver) return 2;
            else return 0;
        }
        else if (currentMode.mode == Modes.Type)
        {
            if (data.modeCategory.type == Types.Organic) return 0;
            else if (data.modeCategory.type == Types.Ballistic) return 1;
            else if (data.modeCategory.type == Types.Other) return 2;
            else return 0;
        }
        else return 0;
    }
}
