using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterrogateValidateButton : MonoBehaviour
{
    [SerializeField] Text inputText;
    [SerializeField] GameObject clueShower;
    [SerializeField] Text dialogueText;
    [SerializeField] GameData gameData;
    Suspects suspect;

    public void Validate()
    {
        if(Match() == true)
        {
            dialogueText.text = "This seems logic. I will bring this person!";
            StartCoroutine(DelayToInterrogate(2));
        }
        else
        {
            dialogueText.text = "This does not make any sense... Please detective, show me something concrete!";
        }

        Reset();
    }

    bool Match()
    {
        AttorneyClueShowerScript script = clueShower.GetComponent<AttorneyClueShowerScript>();

        if (script.currentEvidencesDisplayed[0].GetComponent<PhotoAttorneyObject>().data.modeCategory.suspect == script.currentEvidencesDisplayed[1].GetComponent<PhotoAttorneyObject>().data.modeCategory.suspect)
        {
            Suspects suspectClues = script.currentEvidencesDisplayed[0].GetComponent<PhotoAttorneyObject>().data.modeCategory.suspect;
            switch(inputText.text)
            {
                case "Abigail White":
                    suspect = Suspects.Abigail_White;
                break;
                case "Richard Anderson":
                    suspect = Suspects.Richard_Anderson;
                break;           

                case "Bob Jenkins":
                    suspect = Suspects.Bob_Jenkins;
                break;
                    
                case "Umberto Moretti":
                    suspect = Suspects.Umberto_Moretti;
                break;
        
                default:
                    Debug.Log("Input field text is not valid!");
                return false;
            }
            if(suspect == suspectClues)
            {
                return true;
            }
            else return false;
        }
        else return false;
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
    
    IEnumerator DelayToInterrogate(int time)
    {
        yield return new WaitForSeconds(time);
        gameData.currentSuspect = suspect;

        gameData.interrogations--;

        UnityEngine.SceneManagement.SceneManager.LoadScene("InterrogationScene", LoadSceneMode.Single);
    }
}
