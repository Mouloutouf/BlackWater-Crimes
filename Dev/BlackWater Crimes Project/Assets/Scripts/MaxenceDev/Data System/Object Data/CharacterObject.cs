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

    public float factor;

    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        imageObject.GetComponent<Image>().sprite = data.sprite;
        imageObject.GetComponent<Image>().SetNativeSize();
        RectTransform rect = imageObject.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.rect.width / factor, rect.rect.height / factor);

        if (textObject != null) textObject.GetComponent<Text>().text = data.name;

        base.Protocol();
    }
}
