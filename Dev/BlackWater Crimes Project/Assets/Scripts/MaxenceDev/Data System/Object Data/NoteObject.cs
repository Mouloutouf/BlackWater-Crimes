using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteObject : ObjectData<Note>
{
    public List<GameObject> textObjects;

    private Note myType = new Note();

    void Start()
    {
        GetGameData();

        if (!instantiate) LoadDataOfType(myType, gameData.notes);
    }

    public override void Protocol()
    {
        textObjects[0].GetComponent<Text>().text = data.name;

        textObjects[1].GetComponent<Text>().text = data.text;

        base.Protocol();
    }
}
