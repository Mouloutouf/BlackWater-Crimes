using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsButtonsScript : MonoBehaviour
{
    [SerializeField] Text dialogueText;

    public void VenueButton(GameObject venueRequest)
    {
        venueRequest.SetActive(true);
        dialogueText.text = "So you want to access a venue...";
        gameObject.SetActive(false);
    }

    public void InterrogatingButton(GameObject interrogatingRequest)
    {
        interrogatingRequest.SetActive(true);
        dialogueText.text = "So you want to interrogate a suspect...";
        gameObject.SetActive(false);
    }

    public void ProsecutionButton(GameObject prosecutionRequest)
    {
        prosecutionRequest.SetActive(true);
        dialogueText.text = "So you found the one who did it...";
        gameObject.SetActive(false);
    }
}
