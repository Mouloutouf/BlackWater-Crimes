using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnButtonScript : MonoBehaviour
{
    [SerializeField] GameObject actions;
    [SerializeField] Text dialogueText;

    public void ReturnButton(GameObject parent)
    {
        actions.SetActive(true);
        dialogueText.fontSize = 50;
        dialogueText.text = "Please tell me what you want...";
        parent.SetActive(false);
    }
}
