﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialistClueShowerScript : MonoBehaviour
{
    [SerializeField] GameObject clueFolder;
    [SerializeField] Button validateButton;
    public GameObject currentClueShowed;
    bool folderOpen = false;

    public void OpenFolder()
    {
        if(folderOpen == false)
        {
            clueFolder.SetActive(true);
            folderOpen = true;
            validateButton.interactable = false;
        }
    }

    public void CloseFolder()
    {
        clueFolder.SetActive(false);
        folderOpen = false;
        if(currentClueShowed != null)
        {
            validateButton.interactable = true;
            validateButton.GetComponentInChildren<Text>().text = "Validate";
        }
    }

    public void ShowClue(GameObject clue)
    {
        if (currentClueShowed != null) Destroy(currentClueShowed);

        currentClueShowed = Instantiate(clue, this.transform);
        currentClueShowed.GetComponent<RectTransform>().localPosition = Vector3.zero;
        currentClueShowed.GetComponent<RectTransform>().localScale = new Vector3(2.3f, 2.3f, 1);
        Destroy(currentClueShowed.GetComponent<Button>());
        currentClueShowed.GetComponent<PhotoSpecialistObject>().isClueShower = true;
        CloseFolder();
    }

    public void ResetClue()
    {
        Destroy(currentClueShowed);
    }
}
