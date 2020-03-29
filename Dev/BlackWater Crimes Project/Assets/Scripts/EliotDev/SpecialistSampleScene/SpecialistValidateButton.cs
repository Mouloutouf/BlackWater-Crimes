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
            foreach(Indics indic in gameData.allReports.Keys)
            {
                foreach(Report report in gameData.allReports[indic])
                {
                    if(report.elementName == script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.name)
                    {
                        report.unlockedData = true;
                    }
                }  
            }
        }
        else
        {
            if(specialistType == SpecialistType.ClothDesigner)
            {
                gameData.allReports[Indics.Brandon_Bennington][0].unlockedData = true;
                gameData.allReports[Indics.Brandon_Bennington][0].elementSprite = script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.photo;
                gameData.allReports[Indics.Brandon_Bennington][0].elementName = script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.name;
            }
            else if(specialistType == SpecialistType.CustomsOfficer)
            {
                gameData.allReports[Indics.Arnold_Steele][0].unlockedData = true;
                gameData.allReports[Indics.Arnold_Steele][0].elementSprite = script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.photo;
                gameData.allReports[Indics.Arnold_Steele][0].elementName = script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.name;
            }
            else if(specialistType == SpecialistType.ForensicOfficer)
            {
                gameData.allReports[Indics.Quentin_Copeland][0].unlockedData = true;
                gameData.allReports[Indics.Quentin_Copeland][0].elementSprite = script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.photo;
                gameData.allReports[Indics.Quentin_Copeland][0].elementName = script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.name;
            }
            else if(specialistType == SpecialistType.USCCSecretary)
            {
                gameData.allReports[Indics.Miss_Marshall][0].unlockedData = true;
                gameData.allReports[Indics.Miss_Marshall][0].elementSprite = script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.photo;
                gameData.allReports[Indics.Miss_Marshall][0].elementName = script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.name;
            }
        }

        dialogueText.text = "Okay, let me check that! Anything else?";
        script.ResetClue();
        GetComponent<Button>().interactable = false;
        GetComponentInChildren<Text>().text = "Missing elements";
    }

    bool MatchType()
    {
        if(specialistType == SpecialistType.ClothDesigner && script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.modeCategory.type == Types.Clothing) 
        {
            return true;
        }
        else if (specialistType == SpecialistType.CustomsOfficer && script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.modeCategory.type == Types.Documents) 
        {
            return true;
        }
        else if (specialistType == SpecialistType.ForensicOfficer && script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.modeCategory.type == Types.Crime) 
        {
            return true;
        }
        else if (specialistType == SpecialistType.USCCSecretary && script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.modeCategory.type == Types.Brands) 
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