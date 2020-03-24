using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeighborhoodValidateButton : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] Text dialogueText;

    public void Validate()
    {
        dialogueText.text = "I will go there! Anything else?";
        dropdown.value = 0;
    }
}
