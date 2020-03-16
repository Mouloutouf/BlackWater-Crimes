using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidateScript : MonoBehaviour
{

    [SerializeField] Text dialogueText;

    public void Validate()
    {
        dialogueText.text = "Okay, I'm on it! Anything else?";
    }
}
