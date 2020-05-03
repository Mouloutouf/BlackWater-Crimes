using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesRecorder : MonoBehaviour
{
    public Text liveText;
    public GameObject editPanel;

    public GameData gameData;

    public InstantiateNotes instantiateNotes;

    private Note currentNote;
    private NoteObject currentObject;

    private bool newNote;

    private bool isOpen;
    private TouchScreenKeyboard keyboard;
    
    public void OpenEditNote(GameObject noteObject)
    {
        newNote = noteObject.GetComponent<NoteObject>() == null ? true : false;

        if (!newNote) // For Existing Notes
        {
            currentObject = noteObject.GetComponent<NoteObject>();
            this.currentNote = currentObject.data;
        }
        else // For Adding Note
        {
            Note note = new Note { name = "New Note" + (gameData.notes.Count + 1).ToString(), toEdit = true };
            gameData.notes.Add(note);

            this.currentNote = note;
        }

        editPanel.SetActive(true);
        isOpen = true;

        OpenKeyboard();
    }
    
    public void OpenKeyboard()
    {
        if (keyboard == null)
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);

            keyboard.text = currentNote.text;
            liveText.text = currentNote.text;
        }
    }
    
    void Update()
    {
        if (isOpen && keyboard != null)
        {
            liveText.text = keyboard.text;

            if (!TouchScreenKeyboard.visible && keyboard.status == TouchScreenKeyboard.Status.Done)
            {
                CloseEditNote();
            }
        }
    }

    void CloseEditNote()
    {
        currentNote.text = keyboard.text;
        keyboard = null;
        
        if (!newNote)
        {
            currentObject.Protocol();
        }
        else
        {
            instantiateNotes.CreateNewNote(currentNote);
        }

        editPanel.SetActive(false);
        isOpen = false;
    }

    #region Old
    void OldUpdate()
    {
        if (liveText.text == "Your notes...")
        {
            liveText.text = "";
        }
        if (liveText.text == "")
        {
            liveText.text += keyboard.text;

            gameData.notes[0].text = keyboard.text;
        }
        else
        {
            liveText.text += "\n" + keyboard.text;
        }
        keyboard = null;
    }
    #endregion
}
