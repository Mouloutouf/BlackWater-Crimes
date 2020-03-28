using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VenueValidateButton : MonoBehaviour
{
    [SerializeField] GameObject dropdown;
    [SerializeField] GameObject clueShower;
    [SerializeField] Text dialogueText;

    public void Validate()
    {
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
            dialogueText.fontSize = 40;
            dialogueText.text = "Hmm... No, this isn't a good reason enough.";
            dropdown.GetComponent<Dropdown>().value = 0;
            clueShower.GetComponent<AttorneySingleClueShowerScript>().ResetClue();
            GetComponent<Button>().interactable = false;
            GetComponentInChildren<Text>().text = "Missing elements";
        }

        
    }

    bool Match()
    {
        switch(dropdown.GetComponent<Dropdown>().value)
        {
            case 0:
                if(clueShower.GetComponent<AttorneySingleClueShowerScript>().currentClueShowed.name == "HandClueBG(Clone)")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 1:
                if(clueShower.GetComponent<AttorneySingleClueShowerScript>().currentClueShowed.name == "PantsClueBG(Clone)")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 2:
                if(clueShower.GetComponent<AttorneySingleClueShowerScript>().currentClueShowed.name == "BulletClueBG(Clone)")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            default:
            Debug.Log("Dropdown value is not valid!");
                return false;
        }
    }

    public void Reset()
    {
        dropdown.GetComponent<Dropdown>().value = 0;
        clueShower.GetComponent<AttorneyClueShowerScript>().ResetClue();
        GetComponent<Button>().interactable = false;
        GetComponentInChildren<Text>().text = "Missing elements";
    }
}
