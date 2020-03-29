using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadHunterValidateScript : MonoBehaviour
{
    [SerializeField] Text dialogueText;
    [SerializeField] InputField inputField;
    [SerializeField] Dropdown dropdown;
    string targetName;
    GameData gameData;

    private void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
    }

    public void Validate()
    {
        if(MatchDetails())
        {
            /*foreach(Indics indic in gameData.allReports.Keys)
            {
                foreach(Report report in gameData.allReports[indic])
                {
                    if(report.elementName == targetName)
                    {
                        //report.unlockedData = true;
                    }
                }  
            }*/
        }
        else
        {
            //gameData.allReports[Indics.James_Walkder][0].unlockedData = true;
            //gameData.allReports[Indics.Brandon_Bennington][0].elementName = targetName;
        }

        dialogueText.text = "Okay, I'm on it! Anything else?";
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
            return false;
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
            return false;
        }
        else
        {
            return false;
        }
    }
}
