using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteObject : ObjectData<Note>
{
    public List<GameObject> textObjects;

    public override void Protocol()
    {
        textObjects[0].GetComponent<Text>().text = data.name;

        textObjects[1].GetComponent<Text>().text = data.text;

        base.Protocol();
    }

    public override void Check()
    {
        base.Check();
    }
}
