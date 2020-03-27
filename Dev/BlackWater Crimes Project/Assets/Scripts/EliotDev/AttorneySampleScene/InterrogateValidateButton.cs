using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterrogateValidateButton : MonoBehaviour
{
    [SerializeField] Text inputText;
    [SerializeField] GameObject clueShower;
    [SerializeField] Text dialogueText;

    public void Validate()
    {
        if(Match() == true)
        {
            dialogueText.fontSize = 45;
            dialogueText.text = "This seems logic. I will bring this person, anything else?";
            clueShower.GetComponent<AttorneyClueShowerScript>().ResetClue();
            clueShower.SetActive(false);
            inputText.GetComponentInParent<InputField>().text = "";
            inputText.color = Color.black;
            GetComponent<Button>().interactable = false;
            GetComponentInChildren<Text>().text = "Missing elements";
        }
        else
        {
            dialogueText.fontSize = 40;
            dialogueText.text = "This does not make any sense... Please detective, show me something concrete!";
            clueShower.GetComponent<AttorneyClueShowerScript>().ResetClue();
            clueShower.SetActive(false);
            inputText.GetComponentInParent<InputField>().text = "";
            inputText.color = Color.black;
            GetComponent<Button>().interactable = false;
            GetComponentInChildren<Text>().text = "Missing elements";
        }

        
    }

    bool Match()
    {
        switch(inputText.text)
        {
            case "Abigail White":
                if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "BeerClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "BulletClueBG(Clone)")
                {
                    return true;
                }
                else if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "BeerClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "BulletClueBG(Clone)")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case "Richard Anderson":
                if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "PantsClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "BulletClueBG(Clone)")
                {
                    return true;
                }
                else if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "PantsClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "BulletClueBG(Clone)")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case "Bob Jenkins":
                if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "PantsClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "HandClueBG(Clone)")
                {
                    return true;
                }
                else if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "PantsClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "HandClueBG(Clone)")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
            case "Umberto Moretti":
                if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "HandClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "BeerClueBG(Clone)")
                {
                    return true;
                }
                else if(clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed2.name == "HandClueBG(Clone)" && clueShower.GetComponent<AttorneyClueShowerScript>().currentClueShowed1.name == "BeerClueBG(Clone)")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            default:
            Debug.Log("Input field text is not valid!");
                return false;
        }
    }

    public void Reset()
    {
        clueShower.GetComponent<AttorneyClueShowerScript>().ResetClue();
        clueShower.SetActive(false);
        inputText.GetComponentInParent<InputField>().text = "";
        inputText.color = Color.black;
        GetComponent<Button>().interactable = false;
        GetComponentInChildren<Text>().text = "Missing elements";
    }
}
