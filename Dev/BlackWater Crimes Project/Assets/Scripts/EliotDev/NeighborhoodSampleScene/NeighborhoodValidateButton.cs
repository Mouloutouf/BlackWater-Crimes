using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeighborhoodValidateButton : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] Text dialogueText;
    [SerializeField] string introText;
    [SerializeField] string validateText;
    GameData gameData;

    private void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
        dialogueText.text = introText;
    }
    public void Validate()
    {
        string currentLocationAddress = dropdown.GetComponentInChildren<Text>().text;

        foreach (Indics indic in gameData.allReports.Keys)
        {
            foreach (Report report in gameData.allReports[indic])
            {
                if (report.elementName == currentLocationAddress  && report.unlockedData == false)
                {
                    report.unlockedData = true;
                    gameData.reportsCollected ++;
                    report.unlockOrderIndex = gameData.reportsCollected;

                    gameData.newStuff = true;
                }
            }
        }
        
        dialogueText.text = validateText;
        dropdown.value = 0;
    }
}
