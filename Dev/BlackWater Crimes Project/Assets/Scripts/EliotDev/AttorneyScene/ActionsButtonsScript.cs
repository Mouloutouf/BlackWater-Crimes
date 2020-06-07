using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionsButtonsScript : MonoBehaviour
{
    [SerializeField] Localisation dialogueKey;
    [SerializeField] string introKey;
    [SerializeField] string interrogationKey;
    [SerializeField] string prosecutionKey;
    [SerializeField] public string returnKey;

    private void Start() 
    {
        dialogueKey.key = introKey;
        dialogueKey.RefreshText();
    }
    
    public void InterrogatingButton(GameObject interrogatingRequest)
    {
        interrogatingRequest.SetActive(true);
        dialogueKey.key = interrogationKey;
        dialogueKey.RefreshText();
        gameObject.SetActive(false);
    }

    public void ProsecutionButton(GameObject prosecutionRequest)
    {
        prosecutionRequest.SetActive(true);
        dialogueKey.key = prosecutionKey;
        dialogueKey.RefreshText();
        gameObject.SetActive(false);
    }
}
