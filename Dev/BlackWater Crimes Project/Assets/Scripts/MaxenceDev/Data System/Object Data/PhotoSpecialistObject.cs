using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSpecialistObject : ObjectData<Evidence>
{
    public GameObject imageObject;
    
    public Localisation textKey;

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

            textKey.key = data.nameKey;
            textKey.RefreshText();
        }

        base.Protocol();
    }
}
