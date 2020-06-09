using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public enum FileCategory { Medium, Mobile, Opportunity }

public class FileDisplayer : SerializedMonoBehaviour
{
    public GameObject folder;
    public Button validateButton;

    public string missingKey;
    public string validateKey;
    
    private GameObject currentFileReceiver;
    
    public Dictionary<FileCategory, GameObject> currentFilesDisplayed = new Dictionary<FileCategory, GameObject>();

    private bool isOpen = false;
    private FileCategory category;
    
    void Start()
    {
        validateButton.GetComponentInChildren<Localisation>().key = missingKey;
        validateButton.GetComponentInChildren<Localisation>().RefreshText();

        validateButton.interactable = false;
    }

    public void OpenFolder(GameObject receiver)
    {
        if (!isOpen)
        {
            currentFileReceiver = receiver;

            int index = currentFileReceiver.transform.GetSiblingIndex();
            category = index == 0 ? FileCategory.Medium : index == 1 ? FileCategory.Mobile : FileCategory.Opportunity; // Ligne du Démon

            folder.SetActive(true);
            isOpen = true;

            validateButton.interactable = false;
        }
    }

    public void CloseFolder()
    {
        folder.SetActive(false);

        isOpen = false;
        currentFileReceiver = null;
        
        bool allDisplayed = true;

        foreach (GameObject fileDisplayed in currentFilesDisplayed.Values)
        {
            if (fileDisplayed == null) allDisplayed = false;
        }

        if (allDisplayed)
        {
            validateButton.interactable = true;
            validateButton.GetComponentInChildren<Localisation>().key = validateKey;
            validateButton.GetComponentInChildren<Localisation>().RefreshText();
        }
        else
        {
            validateButton.interactable = false;
            validateButton.GetComponentInChildren<Localisation>().key = missingKey;
            validateButton.GetComponentInChildren<Localisation>().RefreshText();
        }
    }

    public void SelectFile(GameObject file)
    {
        // Removes the displayed file from the receiver if any
        if (currentFilesDisplayed[category] != null) Destroy(currentFilesDisplayed[category]);

        // Checks if the selected file is the same as any of the currently displayed files. If so (the file being already selected), returns
        foreach (GameObject fileDisplayed in currentFilesDisplayed.Values)
        {
            if (fileDisplayed != null)
            {
                FileObject file1 = file.GetComponent<FileObject>(); FileObject file2 = fileDisplayed.GetComponent<FileObject>();

                if (file1.codeKey == file2.codeKey && file1.type == file2.type) { CloseFolder(); return; }
            }
        }

        InstantiateFileInReceiver(file);

        CloseFolder();
    }

    void InstantiateFileInReceiver(GameObject _file)
    {
        currentFilesDisplayed[category] = Instantiate(_file);
        currentFilesDisplayed[category].transform.SetParent(currentFileReceiver.transform, false);

        currentFilesDisplayed[category].GetComponent<RectTransform>().localPosition = Vector3.zero;

        Destroy(currentFilesDisplayed[category].GetComponent<Button>()); // Destroy the button that was used for the selection in the folder
        currentFilesDisplayed[category].GetComponent<FileObject>().isFileDisplayed = true;
    }

    public void ResetFiles() // Could be a button option at the bottom of the screen if the player wants to reset his selection
    {
        foreach (GameObject fileDisplayed in currentFilesDisplayed.Values)
        {
            Destroy(fileDisplayed);
        }
    }
}
