using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PhotoAttorneyObject : ObjectData<Evidence>
{
    public GameObject imageObject;

    private Evidence myType;

    public bool isEvidenceDisplayed = false;

    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        if(!isEvidenceDisplayed)
        {
            imageObject.GetComponent<Image>().sprite = data.photo;
        }

        base.Protocol();
    }

    void Update()
    {
        // Data Protocol

        if (!loaded)
        {
            Protocol();
        }
    }
}
