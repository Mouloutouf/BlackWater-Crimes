using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InstantiateNotes : InstantiationProcess<Note>
{
    public GameObject addNotePrefab;

    [Title("Settings")]

    public float offset;

    [HideInInspector] public List<Vector2> spawnPoints = new List<Vector2>();

    [Title("Contents", horizontalLine: false)]
    
    public List<Transform> contents;
    private Transform currentContent;
    
    private int spawnIndex = 0;
    
    private List<Note> notesList = new List<Note>();

    private GameObject addNoteObject;

    [Title("Components")]

    public NotesRecorder notesRecorder;

    void Start()
    {
        GetGameData();
        
        SetLayout();

        int local = 0;
        currentContent = contents[local];
        
        foreach (Note note in gameData.notes)
        {
            if (note.unlockedData) notesList.Add(note);
        }

        notesList.Reverse();

        foreach (Note _note in notesList) InstantiateObjectOfType(_note, this.prefab);

        addNoteObject = Instantiation(addNotePrefab);
    }

    public override GameObject Instantiation(GameObject prefab)
    {
        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(currentContent, false);

        _prefab.GetComponent<RectTransform>().anchoredPosition = spawnPoints[spawnIndex];

        _prefab.GetComponent<RectTransform>().offsetMin = new Vector2(50, _prefab.GetComponent<RectTransform>().offsetMin.y);
        _prefab.GetComponent<RectTransform>().offsetMax = new Vector2(-50, _prefab.GetComponent<RectTransform>().offsetMax.y);

        _prefab.GetComponentInChildren<Button>().onClick.AddListener(delegate { notesRecorder.OpenEditNote(_prefab); });

        spawnIndex++;
        
        return _prefab;
    }
    
    void SetLayout()
    {
        float sizeX = contents[0].GetComponent<RectTransform>().rect.width;
        float sizeY = offset;

        float posX;
        float posY;

        for (int w = 0; w < gameData.notes.Count + 1; w++)
        {
            posY = (sizeY / 2) + sizeY * w;
            posX = (sizeX / 2);
            
            spawnPoints.Add(new Vector2(posX, -posY));
        }
    }

    public void CreateNewNote(Note note)
    {
        float posX = (contents[0].GetComponent<RectTransform>().rect.width) / 2;
        float posY = (offset / 2) + offset * gameData.notes.Count + 1;

        spawnIndex = gameData.notes.Count;

        InstantiateObjectOfType(note, this.prefab);

        spawnPoints.Add(new Vector2(posX, -posY));
        spawnIndex = gameData.notes.Count + 1;

        Destroy(addNoteObject);
        addNoteObject = Instantiation(addNotePrefab);
    }
}
