using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CharacterObject : ObjectData<Character>
{
    [Title("References")]

    public Image imageComponent;
    public Localisation textComponent;
    
    public float factor;

    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        imageComponent.sprite = data.sprite;
        imageComponent.SetNativeSize();
        RectTransform rect = imageComponent.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.rect.width / factor, rect.rect.height / factor);

        if (textComponent != null) { textComponent.key = data.nameKey; textComponent.RefreshText(); }

        base.Protocol();
    }
}
