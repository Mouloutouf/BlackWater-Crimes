using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PhotoAttorneyObject : ObjectData<Evidence>
{
    public GameObject imageObject;
    
    public Text textComponent;

    public bool isEvidenceDisplayed = false;

    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        if (!isEvidenceDisplayed)
        {
            data.photo = EvidenceInteraction.CreateSprite(data.photoPath);
            imageObject.GetComponent<Image>().sprite = data.photo;

            textComponent.gameObject.GetComponent<Localisation>().key = data.nameKey;
            textComponent.gameObject.GetComponent<Localisation>().RefreshText();
        }

        base.Protocol();
    }
}
