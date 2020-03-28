using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialistValidateButton : MonoBehaviour
{
    [SerializeField] SpecialistType specialistType;
    [SerializeField] SpecialistClueShowerScript script;
    [SerializeField] Text dialogueText;

    public void Validate()
    {
        dialogueText.text = "Okay, let me check that! Anything else?";
        script.ResetClue();
        GetComponent<Button>().interactable = false;
        GetComponentInChildren<Text>().text = "Missing elements";
    }
}

public enum SpecialistType
{
    ClothDesigner, CustomsOfficer, ForensicOfficer, USCCSecretary
}