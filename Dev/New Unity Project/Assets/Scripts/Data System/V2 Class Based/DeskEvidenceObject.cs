using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DeskEvidenceObject : ObjectData
{
    public List<GameObject> objects = new List<GameObject>();

    public Button button;
    
    public override void Protocol()
    {
        objects[0].GetComponent<Image>().sprite = evidence.render2D;

        objects[1].GetComponent<Text>().text = evidence.name;

        objects[2].GetComponent<Text>().text = evidence.description;

        button.onClick.AddListener( delegate { Close(); } );

        base.Protocol();
    }

    public override void Check()
    {
        if (!evidence.taken) gameObject.SetActive(false);
    }

    public void Close()
    {
        evidence.taken = false;
    }
}
