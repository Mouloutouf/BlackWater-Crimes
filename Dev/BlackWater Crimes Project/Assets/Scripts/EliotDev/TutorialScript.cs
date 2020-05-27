﻿using System.Collections;
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


    [Title("Dialogue Texts")]

    [MultiLineProperty(3)]
    public List<List<string>> englishDialogues = new List<List<string>>();

    [MultiLineProperty(3)]
    public List<List<string>> frenchDialogues = new List<List<string>>();

    int textIndex = 0;
    List<List<string>> currentDialogueLanguage = new List<List<string>>();
    int dialogueIndex = 0;

    
    
    [Title("Dialogue Sprites")]

    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<Sprite> dialoguesSprites = new List<Sprite>();
    int spriteIndex = 0;



    [Title("Docks References")]
    public Dictionary<string, GameObject> clues = new Dictionary<string, GameObject>();
    public Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    public Toggle fpToggle;
    public EvidenceInteraction evidenceScript;



    [Title("Menu Desk References")]
    Button folderButton;
    Button mapButton;
    Button phoneButton;


    [Title("Folder Desk References")]
    Button reportsButton;
    Button locationsButton;
    Button charactersButton;
    Button evidencesButton;
    Button returnButton;

    [Title("Folder Desk References")]
    CurrentDialingScript dialingScript;
    CadranScript cadranScript;
    Button eraseButton;



    [Title("Game Data")]
    public GameData gameData;



    bool waitingForBulletCaseInteraction = false;
    bool waitingForBulletCasePhotograph = false;
    bool waitingForBulletCaseDezoom = false;

    bool waitingForLetterInteraction = false;
    bool waitingForLetterText = false;
    bool waitingForLetterNoText = false;
    bool waitingForLetterFpOn = false;
    bool waitingForLetterFpReveal = false;
    bool waitingForLetterPhoto = false;
    bool waitingForLetterDezoom = false;

    bool waitingForLabelInteraction = false;
    bool waitingForLabelPhoto = false;

    bool waitingForDeskScene = false;

    bool waitingForFolderScene = false;
    bool waitingForEvidenceTab = false;

    bool waitingForReturnToDesk = false;

    bool waitingForPhoneScene = false;
    bool waitingForSeven = false;
    bool waitingForErase = false;
    bool checkDialing = false;

    bool waitingForSpecialistScene = false;


    void Start()
    {
        if(gameData.firstTimeInTuto)
        {
            gameData.firstTimeInTuto = false;
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
        else Destroy(this.gameObject);
    }

    void Update() 
    {
        if (waitingForBulletCaseInteraction) 
        {
            if (evidenceScript.currentEvidenceHeld == clues["BulletCase"])
            {
                waitingForBulletCaseInteraction = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForBulletCasePhotograph) 
        {
            if (evidenceScript.currentEvidenceHeld.GetComponent<EvidenceObject>().data.photographed)
            {
                waitingForBulletCasePhotograph = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForBulletCaseDezoom) 
        {
            if (evidenceScript.currentEvidenceHeld == null)
            {
                waitingForBulletCaseDezoom = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForLetterInteraction) 
        {
            if (evidenceScript.currentEvidenceHeld == clues["Letter"])
            {
                waitingForLetterInteraction = false;

                buttons["Photo"].interactable = false;
                buttons["Dezoom"].interactable = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForLetterText)
        {
            if (evidenceScript.currentEvidenceHeld.GetComponent<EvidenceObject>().isShown)
            {
                waitingForLetterText = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForLetterNoText)
        {
            if (!evidenceScript.currentEvidenceHeld.GetComponent<EvidenceObject>().isShown)
            {
                waitingForLetterNoText = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForLetterFpOn)
        {
            if (fpToggle.isOn)
            {
                waitingForLetterFpOn = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForLetterFpReveal)
        {
            foreach (Intel intel in evidenceScript.currentEvidenceHeld.GetComponent<EvidenceObject>().data.intels)
            {
                if (intel.revealed)
                { 
                    waitingForLetterFpReveal = false;

                    StartCoroutine(WaitForNextDialogue());
                }
            }
        }

        else if (waitingForLetterPhoto)
        {
            if (evidenceScript.currentEvidenceHeld.GetComponent<EvidenceObject>().data.photographed)
            {
                waitingForLetterPhoto = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForLetterDezoom) 
        {
            if (evidenceScript.currentEvidenceHeld == null)
            {
                waitingForLetterDezoom = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForLabelInteraction) 
        {
            if (evidenceScript.currentEvidenceHeld == clues["Label"])
            {
                waitingForLabelInteraction = false;

                buttons["Photo"].interactable = false;
                buttons["Dezoom"].interactable = false;
                fpToggle.interactable = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForLabelPhoto) 
        {
            if (evidenceScript.currentEvidenceHeld.GetComponent<EvidenceObject>().data.photographed)
            {
                waitingForLabelPhoto = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForDeskScene)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MenuDeskScene")
            {
                waitingForDeskScene = false;

                MenuDeskStart();

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForFolderScene)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "NewDeskScene")
            {
                waitingForFolderScene = false;

                FolderDeskStart();

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForEvidenceTab)
        {
            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == evidencesButton.gameObject)
            {
                waitingForEvidenceTab = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForReturnToDesk)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MenuDeskScene")
            {
                waitingForReturnToDesk = false;

                MenuDeskStart();

                StartCoroutine(WaitForNextDialogue());
            }
        }
        
        else if (waitingForPhoneScene)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TelephoneScene")
            {
                waitingForPhoneScene = false;

                PhoneStart();

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForSeven)
        {
            if (dialingScript.currentDialing.Count > 0 && dialingScript.currentDialing[0] == 7)
            {
                waitingForSeven = false;

                StartCoroutine(CadranTimer());

                StartCoroutine(WaitForNextDialogue());
            }

            else if (dialingScript.currentDialing.Count > 0 && dialingScript.currentDialing[0] != 7)
            {
                dialingScript.ResetDial();
            }
        }

        else if (waitingForErase)
        {
            if (dialingScript.currentDialing.Count == 0)
            {
                waitingForErase = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        if (checkDialing)
        {
            if (dialingScript.currentContact != "" && dialingScript.currentContact != "B. Bennington")
            {
                dialingScript.currentContact = "";
                dialingScript.currentContactScene = "";
                dialingScript.callingText.text = "";
                dialingScript.StopAllCoroutines();
                dialingScript.ResetDial();
            }
        }

        if (waitingForSpecialistScene)
        {
           if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "IndicScene")
            {
                checkDialing = false;
                waitingForSpecialistScene = false;
            
                StartCoroutine(WaitForNextDialogue());
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

    void MenuDeskStart()
    {
        folderButton = GameObject.Find("Desk Folders Button").GetComponent<Button>();
        mapButton = GameObject.Find("Map Scene Button").GetComponent<Button>();
        phoneButton = GameObject.Find("Telephone Scene Button").GetComponentInChildren<Button>();

        folderButton.interactable = false;
        mapButton.interactable = false;
        phoneButton.interactable = false; 
    }

    void FolderDeskStart()
    {
        reportsButton = GameObject.Find("All Reports Tab").GetComponent<Button>();
        locationsButton = GameObject.Find("Locations Tab").GetComponent<Button>();
        charactersButton = GameObject.Find("Characters Tab").GetComponentInChildren<Button>();
        evidencesButton = GameObject.Find("Evidences Tab").GetComponent<Button>();
        returnButton = GameObject.Find("Return Button").GetComponent<Button>();

        reportsButton.interactable = false;
        locationsButton.interactable = false;
        charactersButton.interactable = false; 
        evidencesButton.interactable = false; 
        returnButton.interactable = false;
    }

    void PhoneStart()
    {
        dialingScript = GameObject.FindObjectOfType<CurrentDialingScript>();
        cadranScript = GameObject.FindObjectOfType<CadranScript>();
        eraseButton = GameObject.Find("EraseButton").GetComponent<Button>();

        cadranScript.enabled = false;
        eraseButton.interactable = false;
    }

    public void NextLine()
    {
        if(textIndex < currentDialogueLanguage[dialogueIndex].Count)
        {
            List<string> tempList = currentDialogueLanguage[dialogueIndex];
            dialogueUIText.text = tempList[textIndex];

            charaUIImage.sprite = dialoguesSprites[spriteIndex];

            textIndex ++;
            spriteIndex ++;
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
            waitingForLetterFpOn = true;
        }

        else if(dialogueIndex == 7)
        {
            waitingForLetterFpReveal = true;
        }

        else if(dialogueIndex == 8)
        {
            buttons["Photo"].interactable = true;
            waitingForLetterPhoto = true;
        }

        else if(dialogueIndex == 9)
        {
            buttons["Dezoom"].interactable = true;
            waitingForLetterDezoom = true;
        }
        
        else if (dialogueIndex == 10)
        {
            buttons["DownCamera"].interactable = true;
            clues["Label"].GetComponent<Collider>().enabled = true;
            foreach(Collider collider in clues["Label"].GetComponentsInChildren<Collider>())
            {
                collider.enabled = true;
            }
            waitingForLabelInteraction = true;
        }

        else if (dialogueIndex == 11)
        {
            buttons["Photo"].interactable = true;
            waitingForLabelPhoto = true;
        }

        else if (dialogueIndex == 12)
        {
            buttons["Dezoom"].interactable = true;
            buttons["ReturnDesk"].interactable = true;
            waitingForDeskScene = true;
        }

        else if (dialogueIndex == 13)
        {
            folderButton.interactable = true;
            waitingForFolderScene = true;
        }

        else if (dialogueIndex == 14)
        {
            evidencesButton.interactable = true;
            waitingForEvidenceTab = true;
        }

        else if (dialogueIndex == 15)
        {
            returnButton.interactable = true;
            waitingForReturnToDesk = true;
        }

        else if (dialogueIndex == 16)
        {
            phoneButton.interactable = true;
            waitingForPhoneScene = true;
        }

        else if (dialogueIndex == 17)
        {
            cadranScript.enabled = true;
            waitingForSeven = true;
        }

        else if (dialogueIndex == 18)
        {
            eraseButton.interactable = true;
            waitingForErase = true;
        }

        else if (dialogueIndex == 19)
        {
            cadranScript.enabled = true;
            checkDialing = true;
            waitingForSpecialistScene = true;
        }
    }

    IEnumerator WaitForEndOfAnimation()
    {
        yield return new WaitForSeconds(3);
        dialogueCanvas.SetActive(true);
    }

    IEnumerator WaitForNextDialogue()
    {
        yield return new WaitForSeconds(.3f);
        dialogueCanvas.SetActive(true);
        dialogueIndex ++;
        NextLine();
    }

    IEnumerator CadranTimer()
    {
        yield return new WaitForSeconds(.5f);
        cadranScript.enabled = false;
    }
}
