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
            validateButton.GetComponentInChildren<Text>().text = "Validate";
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

        if (hasTwoDisplayers && currentEvidencesDisplayed[0] != null && currentEvidencesDisplayed[1] != null)
        {
            validateButton.interactable = true;
            validateButton.GetComponentInChildren<Text>().text = "Validate";
        }

        else if (!hasTwoDisplayers && currentEvidencesDisplayed[0] != null)
        {
            validateButton.interactable = true;
            validateButton.GetComponentInChildren<Text>().text = "Validate";
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
