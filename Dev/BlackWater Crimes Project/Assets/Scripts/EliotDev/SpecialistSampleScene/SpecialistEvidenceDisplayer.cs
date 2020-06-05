using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialistEvidenceDisplayer : MonoBehaviour
{
    [SerializeField] GameObject evidencesFolder;
    private bool folderOpen = false;

    public GameObject currentEvidenceDisplayed;

    private Button validateButton;

    public string validateKey;
    
    void Start()
    {
        validateButton = GameObject.Find("Validate Button").GetComponent<Button>();
    }

    public void OpenFolder()
    {
        if(folderOpen == false)
        {
            evidencesFolder.SetActive(true);
            folderOpen = true;
            validateButton.interactable = false;
        }
    }

    public void CloseFolder()
    {
        evidencesFolder.SetActive(false);
        folderOpen = false;
        if(currentEvidenceDisplayed != null)
        {
            validateButton.interactable = true;
            validateButton.GetComponentInChildren<Localisation>().key = validateKey;
            validateButton.GetComponentInChildren<Localisation>().RefreshText();
        }
    }

    public void ShowClue(GameObject clue)
    {
        if (currentEvidenceDisplayed != null) Destroy(currentEvidenceDisplayed);

        currentEvidenceDisplayed = Instantiate(clue, this.transform);
        currentEvidenceDisplayed.GetComponent<RectTransform>().localPosition = Vector3.zero;
        currentEvidenceDisplayed.GetComponent<RectTransform>().localScale = new Vector3(2.3f, 2.3f, 1);
        Destroy(currentEvidenceDisplayed.GetComponent<Button>());
        currentEvidenceDisplayed.GetComponent<PhotoSpecialistObject>().isEvidenceDisplayed = true;
        CloseFolder();
    }

    public void ResetClue()
    {
        Destroy(currentEvidenceDisplayed);
    }
}
