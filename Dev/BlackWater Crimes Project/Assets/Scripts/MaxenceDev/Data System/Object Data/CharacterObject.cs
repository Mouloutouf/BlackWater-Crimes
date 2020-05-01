using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CharacterObject : ObjectData<Character>
{
    [Title("References")]

    public GameObject imageObject;
    public GameObject textObject;

    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        imageObject.GetComponent<Image>().sprite = data.sprite;

        if (textObject != null) textObject.GetComponent<Text>().text = data.name;

        base.Protocol();
    }
}
