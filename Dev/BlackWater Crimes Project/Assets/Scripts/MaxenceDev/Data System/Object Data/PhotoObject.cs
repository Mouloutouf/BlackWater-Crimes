using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoObject : ObjectData<Evidence>
{
    public GameObject photosContent;

    private SortMode myMode;

    public int pageNumber;

    public override void Protocol()
    {
        transform.GetChild(2).GetComponent<Image>().sprite = data.photo;

        base.Protocol();
    }

    void Update()
    {
        myMode = photosContent.GetComponent<TabManager>().currentMode;
        
        transform.SetParent(photosContent.GetComponent<TabManager>().currentTabsContents[pageNumber][GetTabParent()].transform);
    }
    
    private string GetTabParent()
    {
        string word = "";

        if (myMode.mode == Modes.CrimeScene)
        {
            if (data.modeCategory.crimeScene == Locations.Docks) word = "Docks";
            else if (data.modeCategory.crimeScene == Locations.Whorehouse) word = "Bordel";
            else if (data.modeCategory.crimeScene == Locations.MayorHouse) word = "Maison";
        }
        else if (myMode.mode == Modes.Suspect)
        {
            if (data.modeCategory.suspect == Characters.Anna) word = "Anna";
            else if (data.modeCategory.suspect == Characters.Jack) word = "Jack";
            else if (data.modeCategory.suspect == Characters.Oliver) word = "Oliver";
        }
        else if (myMode.mode == Modes.Type)
        {
            if (data.modeCategory.type == Types.Organic) word = "Organic";
            else if (data.modeCategory.type == Types.Ballistic) word = "Ballistic";
            else if (data.modeCategory.type == Types.Other) word = "Other";
        }

        return word;
    }

    //Useless method
    string SwitchStuff<T>(T enumType) where T : Enum
    {
        switch (data.modeCategory)
        {
            default:
                break;
        }

        return "";
    }
}
