using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class TutorialScript : SerializedMonoBehaviour
{
    [Title("Dialogue References")]
    public GameObject dialogueUI;
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


    [Title("Objective References")]
    public GameObject objectiveUI;
    public Text objectiveText;


    [Title("Objective Text")]
    public List<string> englishObjectives = new List<string>();
    public List<string> frenchObjectives = new List<string>();    
    List<string> currentObjectivesLanguage = new List<string>();
    int objectiveIndex = 0;


    [Title("Docks References")]
    public Dictionary<string, GameObject> clues = new Dictionary<string, GameObject>();
    public Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    public Toggle fpToggle;
    public EvidenceInteraction evidenceScript;


    [Title("Menu Desk References")]
    Button folderButton;
    Button mapButton;
    Button phoneButton;
    Button mainMenuButton;


    [Title("Folder Desk References")]
    Button reportsButton;
    Button locationsButton;
    Button charactersButton;
    Button evidencesButton;
    Button returnButtonFolder;


    [Title("Phone References")]
    CurrentDialingScript dialingScript;
    CadranScript cadranScript;
    Button eraseButton;
    Button returnButtonPhone;


    [Title("Indic References")]
    SpecialistCheck specialistCheck;
    GameObject evidenceReceiver;
    HeadHunterCheck headHunterCheck;
    Button validateButton;
    Button returnButtonIndic;


    [Title("Map References")]
    GameObject magMileQuarter;
    GameObject docks;
    GameObject annaHouse;


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
    bool waitingForLabelDisplayed = false;
    bool waitingForSpecialistValidate = false;

    bool waitingForReturnFolderScene = false;

    bool waitingForSecondReturnToDesk = false;

    bool waitingForHeadHunterScene = false;

    bool waitingForSecondReturnFolderScene = false;

    bool waitingForThirdReturnToDesk = false;

    bool waitingForMapScene = false;
    bool waitingForMagMileZoom = false;
    bool waitingForAnnaHouseDiscover = false;


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
                    currentObjectivesLanguage = englishObjectives;
                    break;

                case Languages.French:
                    currentDialogueLanguage = frenchDialogues;
                    currentObjectivesLanguage = frenchObjectives;
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

                IndicStart(true);
            
                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForLabelDisplayed)
        {
            if(evidenceReceiver.GetComponent<SpecialistEvidenceDisplayer>().currentEvidenceDisplayed != null)
            {
                if (evidenceReceiver.GetComponent<SpecialistEvidenceDisplayer>().currentEvidenceDisplayed.GetComponent<PhotoSpecialistObject>().data.codeName == "Etiquette Vetement")
                {
                    waitingForLabelDisplayed = false;

                    StartCoroutine(WaitForNextDialogue());
                }
            }
        }

        else if (waitingForSpecialistValidate)
        {
           if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == validateButton.gameObject)
            {
                waitingForSpecialistValidate = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForReturnFolderScene)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "NewDeskScene")
            {
                waitingForReturnFolderScene = false;

                FolderDeskStart();

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForSecondReturnToDesk)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MenuDeskScene")
            {
                waitingForSecondReturnToDesk = false;

                MenuDeskStart();

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForHeadHunterScene)
        {
           if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "IndicScene" && gameData.currentIndic == Indics.James_Walker)
            {
                waitingForHeadHunterScene = false;

                IndicStart(false);
            
                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForSecondReturnFolderScene)
        {
           if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "NewDeskScene")
            {
                waitingForSecondReturnFolderScene = false;

                FolderDeskStart();

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForThirdReturnToDesk)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MenuDeskScene")
            {
                waitingForThirdReturnToDesk = false;

                MenuDeskStart();

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForMapScene)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MapScene")
            {
                waitingForMapScene = false;

                MapStart();

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForMagMileZoom)
        {
            if (!magMileQuarter.activeSelf)
            {
                waitingForMagMileZoom = false;

                StartCoroutine(WaitForNextDialogue());
            }
        }

        else if (waitingForAnnaHouseDiscover)
        {
            if (annaHouse.GetComponent<LocationObject>().data.visible)
            {
                waitingForAnnaHouseDiscover = false;

                annaHouse.GetComponent<CircleCollider2D>().enabled = false;

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

        dialogueUI.SetActive(false);
        StartCoroutine(WaitForEndOfAnimation());
    }

    void MenuDeskStart()
    {
        folderButton = GameObject.Find("Desk Folders Button").GetComponent<Button>();
        mapButton = GameObject.Find("Map Scene Button").GetComponent<Button>();
        phoneButton = GameObject.Find("Telephone Scene Button").GetComponentInChildren<Button>();
        mainMenuButton = GameObject.Find("Return To Menu Button").GetComponent<Button>();

        folderButton.interactable = false;
        mapButton.interactable = false;
        phoneButton.interactable = false; 
        mainMenuButton.interactable = false;
    }

    void FolderDeskStart()
    {
        reportsButton = GameObject.Find("All Reports Tab").GetComponent<Button>();
        locationsButton = GameObject.Find("Locations Tab").GetComponent<Button>();
        charactersButton = GameObject.Find("Characters Tab").GetComponentInChildren<Button>();
        evidencesButton = GameObject.Find("Evidences Tab").GetComponent<Button>();
        returnButtonFolder = GameObject.Find("Return Button").GetComponent<Button>();

        reportsButton.interactable = false;
        locationsButton.interactable = false;
        charactersButton.interactable = false; 
        evidencesButton.interactable = false; 
        returnButtonFolder.interactable = false;
    }

    void PhoneStart()
    {
        dialingScript = GameObject.FindObjectOfType<CurrentDialingScript>();
        cadranScript = GameObject.FindObjectOfType<CadranScript>();
        eraseButton = GameObject.Find("EraseButton").GetComponent<Button>();
        returnButtonPhone = GameObject.Find("Return Button").GetComponent<Button>();

        cadranScript.enabled = false;
        eraseButton.interactable = false;
        returnButtonPhone.interactable = false;
    }

    void IndicStart(bool isSpecialist)
    {
        validateButton = GameObject.Find("Validate Button").GetComponent<Button>();
        returnButtonIndic = GameObject.Find("Return Button").GetComponent<Button>();

        validateButton.interactable = false;
        returnButtonIndic.interactable = false;

        if(isSpecialist)
        {
            specialistCheck = GameObject.FindObjectOfType<SpecialistCheck>();
            evidenceReceiver = GameObject.FindObjectOfType<SpecialistEvidenceDisplayer>().gameObject;

            evidenceReceiver.GetComponent<UnityEngine.EventSystems.EventTrigger>().enabled = false;
        }
        else
        {
            headHunterCheck = GameObject.FindObjectOfType<HeadHunterCheck>();
        }
    }

    void MapStart()
    {
        magMileQuarter = GameObject.Find("Quarter Mag Mile");
        docks = GameObject.Find("Location Docks");
        annaHouse = GameObject.Find("Location Anna_House");

        docks.GetComponent<CircleCollider2D>().enabled = false;
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
        dialogueUI.SetActive(false);
        textIndex = 0;

        objectiveUI.SetActive(true);
        objectiveText.text = "- " + currentObjectivesLanguage[objectiveIndex];
        objectiveIndex ++;


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
            returnButtonFolder.interactable = true;
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

        else if (dialogueIndex == 20)
        {
            evidenceReceiver.GetComponent<UnityEngine.EventSystems.EventTrigger>().enabled = true;
            waitingForLabelDisplayed = true;
        }

        else if (dialogueIndex == 21)
        {
            validateButton.interactable = true;
            waitingForSpecialistValidate = true;
        }

        else if (dialogueIndex == 22)
        {
            waitingForReturnFolderScene = true;
        }

        else if (dialogueIndex == 23)
        {
            returnButtonFolder.interactable = true;

            waitingForSecondReturnToDesk = true;
        }

        else if (dialogueIndex == 24)
        {
            folderButton.interactable = true;
            mapButton.interactable = true;
            phoneButton.interactable = true; 

            waitingForHeadHunterScene = true;
        }

        else if (dialogueIndex == 25)
        {
            waitingForSecondReturnFolderScene = true;
        }

        else if (dialogueIndex == 26)
        {
            returnButtonFolder.interactable = true;

            waitingForThirdReturnToDesk = true;
        }

        else if (dialogueIndex == 27)
        {
            folderButton.interactable = true;
            mapButton.interactable = true;
            phoneButton.interactable = true; 

            waitingForMapScene = true;
        }

        else if (dialogueIndex == 28)
        {
            waitingForMagMileZoom = true;
        }

        else if (dialogueIndex == 29)
        {
            waitingForAnnaHouseDiscover = true;
        }

        else if (dialogueIndex == 30)
        {
            annaHouse.GetComponent<CircleCollider2D>().enabled = true;
            docks.GetComponent<CircleCollider2D>().enabled = true;

            Destroy(this.gameObject);
        }
    }

    IEnumerator WaitForEndOfAnimation()
    {
        yield return new WaitForSeconds(3);
        dialogueUI.SetActive(true);
    }

    IEnumerator WaitForNextDialogue()
    {
        objectiveUI.SetActive(false);
        yield return new WaitForSeconds(.3f);
        dialogueUI.SetActive(true);
        dialogueIndex ++;
        NextLine();
    }

    IEnumerator CadranTimer()
    {
        yield return new WaitForSeconds(.5f);
        cadranScript.enabled = false;
    }
}
