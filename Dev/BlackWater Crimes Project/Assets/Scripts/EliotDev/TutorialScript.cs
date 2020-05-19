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
    public List<string> firstDialogueEnglishTexts = new List<string>();
    [MultiLineProperty(3)]
    public List<string> dialoguesFrenchTexts = new List<string>();

    [MultiLineProperty(3)]
    public List<string> secondDialogueEnglishTexts = new List<string>();

    int textIndex = 0;
    List<string> currentDialogue = new List<string>();

    [Title("Docks References")]
    public Dictionary<string, GameObject> clues = new Dictionary<string, GameObject>();
    public Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    public Toggle fpToggle;
    public EvidenceInteraction evidenceScript;

    bool waitingForDouilleInteraction = false;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        DockStart();
    }

    void Update() 
    {
        if(waitingForDouilleInteraction) 
        {
            if(evidenceScript.currentEvidenceHeld == clues["Douille"])
            {
                waitingForDouilleInteraction = false;
                dialogueCanvas.SetActive(true);
                currentDialogue = secondDialogueEnglishTexts;
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
    
        currentDialogue = firstDialogueEnglishTexts;
        NextLine();

        dialogueCanvas.SetActive(false);
        StartCoroutine(WaitForEndOfAnimation());
    }

    public void NextLine()
    {
        if(textIndex < currentDialogue.Count)
        {
            dialogueUIText.text = currentDialogue[textIndex];
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
        if(currentDialogue == firstDialogueEnglishTexts)
        {
            clues["Douille"].GetComponent<Collider>().enabled = true;
            foreach(Collider collider in clues["Douille"].GetComponentsInChildren<Collider>())
            {
                collider.enabled = true;
            }
            waitingForDouilleInteraction = true;
        }
    }

    IEnumerator WaitForEndOfAnimation()
    {
        yield return new WaitForSeconds(3);
        dialogueCanvas.SetActive(true);
    }
}
