using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialistValidateButton : MonoBehaviour
{
    [SerializeField] SpecialistType specialistType;
    [SerializeField] SpecialistClueShowerScript script;
    [SerializeField] Text dialogueText;
    GameData gameData;

    private void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
    }
    
    public void Validate()
    {
        if(MatchType())
        {
            foreach(Report report in gameData.reports)
            {
                if(report.elementName == script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.name)
                {
                    //report.sent = true;
                }
            }
        }
        else
        {
            //gameData.reports.Find(x => x.elementName.Contains("ClothDesignerNTR")).sent = true;
        }

        dialogueText.text = "Okay, let me check that! Anything else?";
        script.ResetClue();
        GetComponent<Button>().interactable = false;
        GetComponentInChildren<Text>().text = "Missing elements";
    }

    bool MatchType()
    {
        if(specialistType == SpecialistType.ClothDesigner && script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.modeCategory.type == Types.Other)
        {
            return true;
        }
        else if (specialistType == SpecialistType.CustomsOfficer && script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.modeCategory.type == Types.Other)
        {
            return true;
        }
        else if (specialistType == SpecialistType.ForensicOfficer && script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.modeCategory.type == Types.Ballistic)
        {
            return true;
        }
        else if (specialistType == SpecialistType.USCCSecretary && script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.modeCategory.type == Types.Other)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public enum SpecialistType
{
    ClothDesigner, CustomsOfficer, ForensicOfficer, USCCSecretary
}