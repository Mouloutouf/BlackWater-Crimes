using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteObject : ObjectData<Note>
{
    public Text textComponent;

    private Note myType = new Note();

    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        textComponent.text = data.text;

        base.Protocol();
    }
}
