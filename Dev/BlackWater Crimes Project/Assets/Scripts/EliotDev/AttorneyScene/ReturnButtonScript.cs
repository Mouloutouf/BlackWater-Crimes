﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnButtonScript : MonoBehaviour
{
    [SerializeField] GameObject actions;
    [SerializeField] Localisation dialogueKey;
    [SerializeField] GameObject validateButton;
    [SerializeField] ActionType type;

    public void ReturnButton(GameObject parent)
    {
        actions.SetActive(true);
        dialogueKey.key = actions.GetComponent<ActionsButtonsScript>().returnKey;
        dialogueKey.RefreshText();

        switch(type)
        {
            case ActionType.Venue:
                validateButton.GetComponent<VenueValidateButton>().Reset();
                break;
            
            case ActionType.Interrogate:
                validateButton.GetComponent<InterrogateValidateButton>().Reset();
                break;

            case ActionType.Prosecution:
                validateButton.GetComponent<ProsecutionValidateButton>().Reset();
                break;
        }

        parent.SetActive(false);
    }
}

public enum ActionType
{
    Venue, Interrogate, Prosecution
}