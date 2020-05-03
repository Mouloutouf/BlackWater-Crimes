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

    public List<Sprite> polaroids = new List<Sprite>();

    public GameObject imageObject;
    public GameObject polaroidObject;
    public GameObject textObject;

    public ElementHolder holder;

    private Evidence myType = new Evidence();

    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        imageObject.GetComponent<Image>().sprite = data.photo;

        //polaroidObject.GetComponent<Image>().sprite = polaroids[UnityEngine.Random.Range(0, polaroids.Count)];

        if (textObject != null) textObject.GetComponent<Text>().text = data.codeName;

        if (holder != null) holder.seen = data.seen;

        base.Protocol();
    }

    void Update()
    {
        if (!loaded)
        {
            Protocol();
        }
    }

    public int GetTabParent(bool yes, SortMode currentMode)
    {
        if (currentMode.mode == Modes.Location)
        {
            if (data.modeCategory.location == Locations.Docks) return 0;
            else if (data.modeCategory.location == Locations.Brothel) return 1;
            else if (data.modeCategory.location == Locations.Anna_House) return 2;
            else return 0;
        }
        else if (currentMode.mode == Modes.Suspect)
        {
            if (data.modeCategory.suspect == Suspects.Abigail_White) return 0;
            else if (data.modeCategory.suspect == Suspects.Umberto_Moretti) return 1;
            else if (data.modeCategory.suspect == Suspects.Richard_Anderson) return 2;
            else return 0;
        }
        else if (currentMode.mode == Modes.Type)
        {
            if (data.modeCategory.type == Types.Brands) return 0;
            else if (data.modeCategory.type == Types.Crime) return 1;
            else if (data.modeCategory.type == Types.Clothing) return 2;
            else return 0;
        }
        else return 0;
    }

    #region Old
    // Tab Organisation

    //currentMode = photosBooklet.GetComponent<TabManager>().currentMode;

    //TabManager tabScript = photosBooklet.GetComponent<TabManager>();
    //transform.SetParent(tabScript.tabsContents[tabScript.tabsObjects[GetTabParent(true)]][pageNumber].transform);
    
    private string GetTabParent()
    {
        string word = "";

        if (currentMode.mode == Modes.Location)
        {
            if (data.modeCategory.location == Locations.Docks) word = "Docks";
            else if (data.modeCategory.location == Locations.Brothel) word = "Bordel";
            else if (data.modeCategory.location == Locations.Anna_House) word = "Maison";
        }
        else if (currentMode.mode == Modes.Suspect)
        {
            if (data.modeCategory.suspect == Suspects.Abigail_White) word = "Anna";
            else if (data.modeCategory.suspect == Suspects.Umberto_Moretti) word = "Jack";
            else if (data.modeCategory.suspect == Suspects.Richard_Anderson) word = "Oliver";
        }
        else if (currentMode.mode == Modes.Type)
        {
            if (data.modeCategory.type == Types.Brands) word = "Organic";
            else if (data.modeCategory.type == Types.Crime) word = "Ballistic";
            else if (data.modeCategory.type == Types.Clothing) word = "Other";
        }

        return word;
    }
    #endregion
}
