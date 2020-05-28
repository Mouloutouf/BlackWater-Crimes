using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSpecialistObject : ObjectData<Evidence>
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
            imageObject.GetComponent<Image>().sprite = data.photo;

            textComponent.text = data.codeName;
        }

        base.Protocol();
    }
}
