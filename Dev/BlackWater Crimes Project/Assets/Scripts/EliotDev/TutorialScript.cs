using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class TutorialScript : SerializedMonoBehaviour
{
    [Title("Dialogue References")]
    public GameObject dialogueCanvas;
    public Text dialogueUIText;
    public Image charaUIImage;
    public Dictionary<string[], Sprite> dialogues = new Dictionary<string[], Sprite>();


    [Title("Dialogue Texts")]

    [MultiLineProperty(3)]
    public List<List<string>> englishDialogues = new List<List<string>>();

    [MultiLineProperty(3)]
    public List<List<string>> frenchDialogues = new List<List<string>>();

    int textIndex = 0;
    List<List<string>> currentDialogueLanguage = new List<List<string>>();
    int dialogueIndex = 0;



    [Title("Docks References")]
    public Dictionary<string, GameObject> clues = new Dictionary<string, GameObject>();
    public Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    public Toggle fpToggle;
    public EvidenceInteraction evidenceScript;



    [Title("Game Data")]
    public GameData gameData;



    bool waitingForBulletCaseInteraction = false;
    bool waitingForBulletCasePhotograph = false;
    bool waitingForBulletCaseDezoom = false;

    bool waitingForLetterInteraction = false;
    bool waitingForLetterText = false;
    bool waitingForLetterNoText = false;
    bool waitingForLetterFPOn = false;


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        switch (gameData.gameLanguage)
        {
            case Languages.English:
                currentDialogueLanguage = englishDialogues;
                break;

            case Languages.French:
                currentDialogueLanguage = frenchDialogues;
                break;
        }

        DockStart();
    }

    void Update() 
    {
        if (waitingForBulletCaseInteraction) 
        {
            if (evidenceScript.currentEvidenceHeld == clues["BulletCase"])
            {
                waitingForBulletCaseInteraction = false;
                dialogueCanvas.SetActive(true);

                dialogueIndex ++;
                NextLine();
            }
        }

        else if (waitingForBulletCasePhotograph) 
        {
            if (evidenceScript.currentEvidenceHeld.GetComponent<EvidenceObject>().data.photographed)
            {
                waitingForBulletCasePhotograph = false;
                dialogueCanvas.SetActive(true);

                dialogueIndex ++;
                NextLine();
            }
        }

        else if (waitingForBulletCaseDezoom) 
        {
            if (evidenceScript.currentEvidenceHeld == null)
            {
                waitingForBulletCaseDezoom = false;
                dialogueCanvas.SetActive(true);

                dialogueIndex ++;
                NextLine();
            }
        }

        else if (waitingForLetterInteraction) 
        {
            if (evidenceScript.currentEvidenceHeld == clues["Letter"])
            {
                waitingForLetterInteraction = false;
                dialogueCanvas.SetActive(true);

                dialogueIndex ++;
                buttons["Photo"].interactable = false;
                buttons["Dezoom"].interactable = false;
                NextLine();
            }
        }

        else if (waitingForLetterText)
        {
            if (evidenceScript.currentEvidenceHeld.GetComponent<EvidenceObject>().isShown)
            {
                waitingForLetterText = false;
                dialogueCanvas.SetActive(true);

                dialogueIndex ++;
                NextLine();
            }
        }

        else if (waitingForLetterNoText)
        {
            if (!evidenceScript.currentEvidenceHeld.GetComponent<EvidenceObject>().isShown)
            {
                waitingForLetterNoText = false;
                dialogueCanvas.SetActive(true);

                dialogueIndex ++;
                NextLine();
            }
        }

        else if (waitingForLetterFPOn)
        {
            if (fpToggle.isOn)
            {
                waitingForLetterFPOn = false;
                dialogueCanvas.SetActive(true);

                dialogueIndex ++;
                NextLine();
            }
        }
    }

    void DockStart()
    {
        foreach (GameObject clue in clues.Values)
        {
            clue.GetComponent<Collider>().enabled = false;
            foreach(Collider collider in clue.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
        }

        foreach(Button button in buttons.Values)
        {
            button.interactable = false;
        }

        fpToggle.interactable = false;
    
        NextLine();

        dialogueCanvas.SetActive(false);
        StartCoroutine(WaitForEndOfAnimation());
    }

    public void NextLine()
    {
        if(textIndex < currentDialogueLanguage[dialogueIndex].Count)
        {
            List<string> tempList = currentDialogueLanguage[dialogueIndex];
            dialogueUIText.text = tempList[textIndex];
            textIndex ++;
        }
        else
        {
            EndCurrentDialogue();
        }
    }

    void EndCurrentDialogue()
    {
        dialogueCanvas.SetActive(false);
        textIndex = 0;

        PrepareNextDialogue();
    }

    void PrepareNextDialogue()
    {
        List<string> tempList = currentDialogueLanguage[dialogueIndex];

        if(dialogueIndex == 0)
        {
            clues["BulletCase"].GetComponent<Collider>().enabled = true;
            foreach(Collider collider in clues["BulletCase"].GetComponentsInChildren<Collider>())
            {
                collider.enabled = true;
            }
            waitingForBulletCaseInteraction = true;
        }

        else if(dialogueIndex == 1)
        {
            buttons["Photo"].interactable = true;
            waitingForBulletCasePhotograph = true;
        }

        else if(dialogueIndex == 2)
        {
            buttons["Dezoom"].interactable = true;
            waitingForBulletCaseDezoom = true;
        }

        else if(dialogueIndex == 3)
        {
            buttons["UpCamera"].interactable = true;
            clues["Letter"].GetComponent<Collider>().enabled = true;
            foreach(Collider collider in clues["Letter"].GetComponentsInChildren<Collider>())
            {
                collider.enabled = true;
            }
            waitingForLetterInteraction = true;
        }

        else if(dialogueIndex == 4)
        {
            buttons["Text"].interactable = true;
            waitingForLetterText = true;
        }

        else if(dialogueIndex == 5)
        {
            waitingForLetterNoText = true;
        }

        else if(dialogueIndex == 6)
        {
            fpToggle.interactable = true;
            waitingForLetterFPOn = true;
        }
    }

    IEnumerator WaitForEndOfAnimation()
    {
        yield return new WaitForSeconds(3);
        dialogueCanvas.SetActive(true);
    }
}
