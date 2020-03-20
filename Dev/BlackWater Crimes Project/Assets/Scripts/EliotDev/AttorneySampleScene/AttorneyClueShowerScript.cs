using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttorneyClueShowerScript : MonoBehaviour
{
    [SerializeField] GameObject clueFolder;
    [SerializeField] Button validateButton;
    GameObject clueShowerUsed;
    public GameObject currentClueShowed1;
    public GameObject currentClueShowed2;
    bool folderOpen = false;

    public void OpenFolder(GameObject clueShower)
    {
        if(folderOpen == false)
        {
            clueShowerUsed = clueShower;
            clueFolder.SetActive(true);
            folderOpen = true;
            validateButton.interactable = false;
        }
    }

    public void CloseFolder()
    {
        clueFolder.SetActive(false);
        folderOpen = false;
        clueShowerUsed = null;
        if(currentClueShowed1 != null && currentClueShowed2 != null)
        {
            validateButton.interactable = true;
            validateButton.GetComponentInChildren<Text>().text = "Validate";
        }
    }

    public void ShowClue(GameObject clue)
    {
        if(clueShowerUsed.name == "ClueShowerReceiver 1"){
            if(currentClueShowed1 == null)
            {
                currentClueShowed1 = Instantiate(clue, clueShowerUsed.transform);
                currentClueShowed1.GetComponent<RectTransform>().localPosition = Vector3.zero;
                currentClueShowed1.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
                Destroy(currentClueShowed1.GetComponent<EventTrigger>());
                CloseFolder();
            }
            else
            {
                Destroy(currentClueShowed1);
                currentClueShowed1 = Instantiate(clue, clueShowerUsed.transform);
                currentClueShowed1.GetComponent<RectTransform>().localPosition = Vector3.zero;
                currentClueShowed1.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
                Destroy(currentClueShowed1.GetComponent<EventTrigger>());
                CloseFolder();
            }
        }
        else if(clueShowerUsed.name == "ClueShowerReceiver 2")
        {
            if(currentClueShowed2 == null)
            {
                currentClueShowed2 = Instantiate(clue, clueShowerUsed.transform);
                currentClueShowed2.GetComponent<RectTransform>().localPosition = Vector3.zero;
                currentClueShowed2.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
                Destroy(currentClueShowed2.GetComponent<EventTrigger>());
                CloseFolder();
            }
            else
            {
                Destroy(currentClueShowed2);
                currentClueShowed2 = Instantiate(clue, clueShowerUsed.transform);
                currentClueShowed2.GetComponent<RectTransform>().localPosition = Vector3.zero;
                currentClueShowed2.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, 1);
                Destroy(currentClueShowed2.GetComponent<EventTrigger>());
                CloseFolder();
            }
        }
    }

    public void ResetClue()
    {
        Destroy(currentClueShowed1);
        Destroy(currentClueShowed2);
    }
}
