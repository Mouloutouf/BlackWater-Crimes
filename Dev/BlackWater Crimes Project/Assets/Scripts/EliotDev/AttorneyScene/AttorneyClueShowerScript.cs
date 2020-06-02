using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class AttorneyClueShowerScript : MonoBehaviour
{
    [SerializeField] GameObject clueFolder;
    [SerializeField] Button validateButton;

    public string missingKey;
    public string validateKey;

    public bool hasTwoDisplayers;

    GameObject evidenceDisplayerUsed;

    public List<GameObject> currentEvidencesDisplayed = new List<GameObject>();

    bool folderOpen = false;
    int index;

    public bool debug;

    void Start()
    {
        if (debug)
        {
            validateButton.interactable = true;
            validateButton.GetComponentInChildren<Localisation>().key = validateKey;
            validateButton.GetComponentInChildren<Localisation>().RefreshText();
        }

        else
        {
            validateButton.GetComponentInChildren<Localisation>().key = missingKey;
            validateButton.GetComponentInChildren<Localisation>().RefreshText();
        }
    }

    public void OpenFolder(GameObject displayer)
    {
        if(folderOpen == false)
        {
            evidenceDisplayerUsed = displayer;

            index = evidenceDisplayerUsed.transform.GetSiblingIndex();
            clueFolder.SetActive(true);

            folderOpen = true;
            validateButton.interactable = false;
        }
    }

    public void CloseFolder()
    {
        clueFolder.SetActive(false);

        folderOpen = false;
        evidenceDisplayerUsed = null;
        
        if (currentEvidencesDisplayed[0] != null)
        {
            if (hasTwoDisplayers && currentEvidencesDisplayed[1] == null) return;

            validateButton.interactable = true;
            validateButton.GetComponentInChildren<Localisation>().key = validateKey;
            validateButton.GetComponentInChildren<Localisation>().RefreshText();
        }
    }

    public void ShowClue(GameObject clue)
    {
        if (currentEvidencesDisplayed[index] != null) Destroy(currentEvidencesDisplayed[index]);

        foreach (GameObject evidence in currentEvidencesDisplayed)
        {
            if (evidence != null && clue.GetComponent<PhotoAttorneyObject>().data.codeName == evidence.GetComponent<PhotoAttorneyObject>().data.codeName)
            { 
                CloseFolder();
                return;
            }
        }

        currentEvidencesDisplayed[index] = Instantiate(clue, evidenceDisplayerUsed.transform);
        currentEvidencesDisplayed[index].GetComponent<RectTransform>().localPosition = Vector3.zero;

        currentEvidencesDisplayed[index].GetComponent<RectTransform>().localScale = new Vector3(2.3f, 2.3f, 1);

        Destroy(currentEvidencesDisplayed[index].GetComponent<Button>());
        currentEvidencesDisplayed[index].GetComponent<PhotoAttorneyObject>().isEvidenceDisplayed = true;

        CloseFolder();
    }

    public void ResetClue()
    {
        Destroy(currentEvidencesDisplayed[0]);
        Destroy(currentEvidencesDisplayed[1]);
    }
}
