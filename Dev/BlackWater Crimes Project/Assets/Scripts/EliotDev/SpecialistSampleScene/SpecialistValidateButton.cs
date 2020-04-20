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

    private bool match;

    private void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
    }
    
    public void Validate()
    {
        foreach (Indics indic in gameData.allReports.Keys)
        {
            foreach (Report report in gameData.allReports[indic])
            {
                if (report.elementName == script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.name && report.index != 0)
                {
                    if (report.elementDetailName == "")
                    {
                        UnlockReport(report);
                    }
                    else
                    {
                        foreach (Intel intel in script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.intels)
                        {
                            if (intel.revealed && report.elementDetailName == intel.name)
                            {
                                UnlockReport(report);
                            }
                        }
                    }
                    
                    match = true;
                }
            }
        }

        if (!match)
        {
            switch (specialistType)
            {
                case SpecialistType.ClothDesigner:
                    UnlockFailedReport(Indics.Brandon_Bennington);
                    break;
                case SpecialistType.CustomsOfficer:
                    UnlockFailedReport(Indics.Arnold_Steele);
                    break;
                case SpecialistType.ForensicOfficer:
                    UnlockFailedReport(Indics.Quentin_Copeland);
                    break;
                case SpecialistType.USCCSecretary:
                    UnlockFailedReport(Indics.Miss_Marshall);
                    break;
                default:
                    break;
            }
        }

        match = false;

        dialogueText.text = "Okay, let me check that! Anything else?";
        script.ResetClue();
        GetComponent<Button>().interactable = false;
        GetComponentInChildren<Text>().text = "Missing elements";
    }

    void UnlockReport(Report _report)
    {
        if (_report.unlockedData == false)
        {
            _report.unlockedData = true;
            _report.elementSprite = script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.photo;
            gameData.reportsCollected++;
            _report.unlockOrderIndex = gameData.reportsCollected;
        }
    }

    void UnlockFailedReport(Indics indic)
    {
        gameData.allReports[indic][0].unlockedData = true;
        gameData.allReports[indic][0].elementSprite = script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.photo;
        gameData.allReports[indic][0].elementName = script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.name;
    }

    bool MatchType()
    {
        if (specialistType == SpecialistType.ClothDesigner && script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.modeCategory.type == Types.Clothing) 
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