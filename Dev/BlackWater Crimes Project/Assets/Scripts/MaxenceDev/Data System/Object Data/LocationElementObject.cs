using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class LocationElementObject : ObjectData<Location>
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
        imageObject.GetComponent<Image>().sprite = data.locationCroppedImage;

        if (textObject != null) { textObject.GetComponent<Localisation>().key = data.nameKey; textObject.GetComponent<Localisation>().RefreshText(); }

        base.Protocol();
    }
}