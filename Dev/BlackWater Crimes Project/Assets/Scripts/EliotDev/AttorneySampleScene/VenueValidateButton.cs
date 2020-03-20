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
            clueShower.GetComponent<AttorneyClueShowerScript>().ResetClue();
            GetComponent<Button>().interactable = false;
            GetComponentInChildren<Text>().text = "Missing elements";
        }
        else
        {
            dialogueText.fontSize = 40;
            dialogueText.text = "This does not make any sense... Please detective, show me something concrete!";
            clueShower.GetComponent<AttorneyClueShowerScript>().ResetClue();
            GetComponent<Button>().interactable = false;
            GetComponentInChildren<Text>().text = "Missing elements";
        }

        
    }

    bool Match()
    {
        switch(dropdown.GetComponent<Dropdown>().value)
        {
            case 0:
                if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "HandClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "BulletClueBG(Clone)")
                {
                    return true;
                }
                else if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "HandClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "BulletClueBG(Clone)")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 1:
                if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "PantsClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "BeerClueBG(Clone)")
                {
                    return true;
                }
                else if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "PantsClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "BeerClueBG(Clone)")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 2:
                if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "BulletClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "BeerClueBG(Clone)")
                {
                    return true;
                }
                else if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "BulletClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "BeerClueBG(Clone)")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            default:
            Debug.Log("Dropdown value not valid!");
                return false;
        }
    }
}
