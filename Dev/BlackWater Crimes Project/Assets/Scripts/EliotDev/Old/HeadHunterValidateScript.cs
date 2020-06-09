using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadHunterValidateScript : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Dropdown dropdown;
    [SerializeField] Text dialogueText;
    [SerializeField] string introText;
    [SerializeField] string validateText;
    string targetName;
    GameData gameData;

    private void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
        dialogueText.text = introText;
    }

    public void Validate()
    {
        if(MatchDetails())
        {
            foreach(Indics indic in gameData.allReports.Keys)
            {
                foreach(Report report in gameData.allReports[indic])
                {
                    if(report.elementKey == targetName && report.unlockedData == false)
                    {
                        report.unlockedData = true;
                        gameData.reportsCollected ++;
                        report.unlockOrderIndex = gameData.reportsCollected;

                        gameData.newStuff = true;
                    }
                }  
            } 
        }
        else
        {
            gameData.allReports[Indics.James_Walker][0].unlockedData = true;
            gameData.allReports[Indics.James_Walker][0].elementKey = targetName;
        }

        dialogueText.text = validateText;
    }

    bool MatchDetails()
    {
        if(inputField.text == "Abigail White")
        {
            if(dropdown.GetComponentInChildren<Text>().text == "Brown" || dropdown.GetComponentInChildren<Text>().text == "Mole")
            {
                targetName = "Abigail White";
                return true;
            }
            else
            {
                return false;
            }
        }
        else if(inputField.text == "Richard Anderson")
        {
            if (dropdown.GetComponentInChildren<Text>().text == "Blond" || dropdown.GetComponentInChildren<Text>().text == "Mustache")
            {
                targetName = "Richard Anderson";
                return true;
            }
            else
            {
                return false;
            }
        }
        else if(inputField.text == "Bob Jenkins")
        {
            if(dropdown.GetComponentInChildren<Text>().text == "Hazel eyes" || dropdown.GetComponentInChildren<Text>().text == "Mustache")
            {
                targetName = "Bob Jenkins";
                return true;
            }
            else
            {
                return false;
            }
        }
        else if(inputField.text == "Umberto Moretti")
        {
            if (dropdown.GetComponentInChildren<Text>().text == "Brown" || dropdown.GetComponentInChildren<Text>().text == "Scar" || dropdown.GetComponentInChildren<Text>().text == "Brown eyes")
            {
                targetName = "Umberto Moretti";
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (inputField.text == "Anna Jameswan")
        {
            if (dropdown.GetComponentInChildren<Text>().text == "Blond")
            {
                targetName = "Anna Jameswan";
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
