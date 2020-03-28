using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VenueValidateButton : MonoBehaviour
{
    [SerializeField] GameObject dropdown;
    [SerializeField] GameObject clueShower;
    [SerializeField] Text dialogueText;
    string currentLocationAddress;
    GameData gameData;

    private void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
    }

    public void Validate()
    {
        currentLocationAddress = dropdown.GetComponentInChildren<Text>().text;

        if(Match() == true)
        {
            dialogueText.fontSize = 45;
            dialogueText.text = "This seems logic. You can go there, anything else?";
            dropdown.GetComponent<Dropdown>().value = 0;
            clueShower.GetComponent<AttorneySingleClueShowerScript>().ResetClue();
            GetComponent<Button>().interactable = false;
            GetComponentInChildren<Text>().text = "Missing elements";
        }
        else
        {
            dialogueText.text = "Hmm... No, this isn't a good reason enough.";
            dropdown.GetComponent<Dropdown>().value = 0;
            clueShower.GetComponent<AttorneySingleClueShowerScript>().ResetClue();
            GetComponent<Button>().interactable = false;
            GetComponentInChildren<Text>().text = "Missing elements";
        }

        
    }

    bool Match()
    {
        // A voir avec les variables des indices
        if(currentLocationAddress == "1048, W Madison Str." && clueShower.GetComponent<AttorneySingleClueShowerScript>().currentClueShowed.GetComponent<PhotoSpecialistObject>().data.name == "Key")
        {
            return true;
        } 
        else
        {
            return false;
        }
    }

    public void Reset()
    {
        dropdown.GetComponent<Dropdown>().value = 0;
        clueShower.GetComponent<AttorneySingleClueShowerScript>().ResetClue();
        GetComponent<Button>().interactable = false;
        GetComponentInChildren<Text>().text = "Missing elements";
    }
}
