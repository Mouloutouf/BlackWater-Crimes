﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VenueValidateButton : MonoBehaviour
{
    [SerializeField] GameObject dropdown;
    [SerializeField] GameObject clueShower;
    [SerializeField] Text dialogueText;
    string currentLocationAddress;
    GameData gameData;

    private void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
    }

    public void Validate()
    {
        currentLocationAddress = dropdown.GetComponentInChildren<Text>().text;

        if(Match() == true)
        {
            dialogueText.text = "This seems logic. You can go there, anything else?";
            dropdown.GetComponent<Dropdown>().value = 0;
            clueShower.GetComponent<AttorneyClueShowerScript>().ResetClue();
            GetComponent<Button>().interactable = false;
            GetComponentInChildren<Text>().text = "Missing elements";

            foreach (Location _location in gameData.locations)
            {
                if (clueShower.GetComponent<AttorneyClueShowerScript>().currentEvidencesDisplayed[0].GetComponent<PhotoAttorneyObject>().data.unlockableLocation == _location.myLocation)
                {
                    _location.accessible = true;
                }
            }
        }
        else
        {
            dialogueText.text = "Hmm... No, this isn't a good reason enough.";
            dropdown.GetComponent<Dropdown>().value = 0;
            clueShower.GetComponent<AttorneyClueShowerScript>().ResetClue();
            GetComponent<Button>().interactable = false;
            GetComponentInChildren<Text>().text = "Missing elements";
        }
    }

    bool Match()
    {
        if (clueShower.GetComponent<AttorneyClueShowerScript>().currentEvidencesDisplayed[0].GetComponent<PhotoAttorneyObject>().data.useToUnlock)
        {
            Locations currentLocation = dropdown.GetComponent<DropdownVenues>()._venues[dropdown.GetComponentInChildren<Text>().text];
            if (clueShower.GetComponent<AttorneyClueShowerScript>().currentEvidencesDisplayed[0].GetComponent<PhotoAttorneyObject>().data.unlockableLocation == currentLocation)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        else
        {
            return false;
        }
    }

    public void Reset()
    {
        dropdown.GetComponent<Dropdown>().value = 0;
        clueShower.GetComponent<AttorneyClueShowerScript>().ResetClue();
        GetComponent<Button>().interactable = false;
        GetComponentInChildren<Text>().text = "Missing elements";
    }
}
