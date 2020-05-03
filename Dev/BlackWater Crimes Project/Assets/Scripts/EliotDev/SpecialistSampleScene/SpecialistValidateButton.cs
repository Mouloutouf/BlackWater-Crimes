using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialistValidateButton : MonoBehaviour
{
    [SerializeField] SpecialistType specialistType;
    [SerializeField] SpecialistClueShowerScript script;
    [SerializeField] Text dialogueText;
    [SerializeField] string introText;
    [SerializeField] string validateText;
    GameData gameData;

    private bool match;

    private void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
        dialogueText.text = introText;
    }
    
    public void Validate()
    {
        foreach (Indics indic in gameData.allReports.Keys)
        {
            foreach (Report report in gameData.allReports[indic])
            {
                if (report.elementName == script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.codeName && report.index != 0)
                {
                    if (report.elementDetailName == null)
                    {
                        UnlockReport(report);

                        match = true;
                    }
                    else
                    {
                        foreach (Intel intel in script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.intels)
                        {
                            Debug.Log(intel.name + intel.revealed);

                            if (intel.revealed && report.elementDetailName == intel.name)
                            {
                                UnlockReport(report);

                                match = true;
                            }
                        }
                    }
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

        dialogueText.text = validateText;
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

            gameData.newStuff = true;
        }
    }

    void UnlockFailedReport(Indics indic)
    {
        gameData.allReports[indic][0].unlockedData = true;
        gameData.allReports[indic][0].elementSprite = script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.photo;
        gameData.allReports[indic][0].elementName = script.currentClueShowed.GetComponent<PhotoSpecialistObject>().data.codeName;
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