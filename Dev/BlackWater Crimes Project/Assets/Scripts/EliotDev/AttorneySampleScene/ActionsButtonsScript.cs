using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsButtonsScript : MonoBehaviour
{
    [SerializeField] Text dialogueText;
    [SerializeField] string introText;
    [SerializeField] string venueText;
    [SerializeField] string interrogationText;
    [SerializeField] string prosecutionText;
    [SerializeField] public string returnText;

    private void Start() 
    {
        dialogueText.text = introText;
    }

    public void VenueButton(GameObject venueRequest)
    {
        venueRequest.SetActive(true);
        dialogueText.text = venueText;
        gameObject.SetActive(false);
    }

    public void InterrogatingButton(GameObject interrogatingRequest)
    {
        interrogatingRequest.SetActive(true);
        dialogueText.text = interrogationText;
        gameObject.SetActive(false);
    }

    public void ProsecutionButton(GameObject prosecutionRequest)
    {
        prosecutionRequest.SetActive(true);
        dialogueText.text = prosecutionText;
        gameObject.SetActive(false);
    }
}
