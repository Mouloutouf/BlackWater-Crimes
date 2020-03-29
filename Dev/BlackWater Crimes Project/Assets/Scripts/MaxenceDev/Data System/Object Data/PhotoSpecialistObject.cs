using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSpecialistObject : ObjectData<Evidence>
{
    public GameObject imageObject;

    private Evidence myType;

    void Start()
    {

    }

    public override void Protocol()
    {
        imageObject.GetComponent<Image>().sprite = data.photo;

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
