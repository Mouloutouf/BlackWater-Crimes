using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeighborhoodValidateButton : MonoBehaviour
{
    [SerializeField] Dropdown dropdown;
    [SerializeField] Text dialogueText;
    GameData gameData;

    private void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
    }
    public void Validate()
    {
        string currentLocationAddress = dropdown.GetComponentInChildren<Text>().text;

        foreach(Report report in gameData.reports)
        {
            if(report.elementName == currentLocationAddress)
            {
                //report.sent = true;
            }
        }

        dialogueText.text = "I will go there! Anything else?";
        dropdown.value = 0;
    }
}
