using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InterrogateValidateButton : MonoBehaviour
{
    [SerializeField] Text inputText;
    [SerializeField] GameObject evidenceDisplayer;
    [SerializeField] Text dialogueText;
    [SerializeField] Text interrogationsNumberText;
    [SerializeField] GameData gameData;
    Suspects inputSuspect;

    public bool debug;

    private void Start() 
    {
        interrogationsNumberText.text += gameData.interrogations;
    }

    public void Validate()
    {
        if (debug)
        {
            dialogueText.text = "This seems logic. I will bring this person!";
            StartCoroutine(DelayToInterrogate(2));

            Reset();

            return;
        }

        if (Match())
        {
            if (gameData.interrogations > 0)
            {
                dialogueText.text = "This seems logic. I will bring this person!";
                StartCoroutine(DelayToInterrogate(2));
            }
            else
            {
                dialogueText.text = "Hold on a minute, you can't go on interrogating the whole city !";
            }
        }
        else dialogueText.text = "This does not make any sense... Please detective, show me something concrete!";
        
        Reset();
    }

    bool Match()
    {
        AttorneyClueShowerScript script = evidenceDisplayer.GetComponent<AttorneyClueShowerScript>();

        if (script.currentEvidencesDisplayed[0].GetComponent<PhotoAttorneyObject>().data.modeCategory.suspect == script.currentEvidencesDisplayed[1].GetComponent<PhotoAttorneyObject>().data.modeCategory.suspect)
        {
            Suspects evidenceSuspect = script.currentEvidencesDisplayed[0].GetComponent<PhotoAttorneyObject>().data.modeCategory.suspect;

            switch (inputText.text)
            {
                case "Abigail White":
                    inputSuspect = Suspects.Abigail_White;
                break;

                case "Richard Anderson":
                    inputSuspect = Suspects.Richard_Anderson;
                break;           

                case "Bob Jenkins":
                    inputSuspect = Suspects.Bob_Jenkins;
                break;
                    
                case "Umberto Moretti":
                    inputSuspect = Suspects.Umberto_Moretti;
                break;
        
                default:
                    Debug.Log("Input field text is not valid!");
                return false;
            }

            if (inputSuspect == evidenceSuspect)
            {
                return true;
            }
            else return false;
        }
        else return false;
    }

    public void Reset()
    {
        evidenceDisplayer.GetComponent<AttorneyClueShowerScript>().ResetClue();
        evidenceDisplayer.SetActive(false);

        inputText.GetComponentInParent<InputField>().text = "";
        inputText.color = Color.black;

        GetComponent<Button>().interactable = false;
        GetComponentInChildren<Text>().text = "Missing elements";
    }
    
    IEnumerator DelayToInterrogate(int time)
    {
        yield return new WaitForSeconds(time);
        gameData.currentSuspect = inputSuspect;

        gameData.interrogations--;

        UnityEngine.SceneManagement.SceneManager.LoadScene("InterrogationScene", LoadSceneMode.Single);
    }
}
