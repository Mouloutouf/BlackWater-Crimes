using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DeskEvidenceObject : ObjectData<Evidence>
{
    public List<GameObject> objects = new List<GameObject>();

    public Button button;
    
    // Load
    public override void Protocol()
    {
        objects[0].GetComponent<Image>().sprite = data.photo;

        objects[1].GetComponent<Text>().text = data.codeName;

        objects[2].GetComponent<Text>().text = data.description;

		button.onClick.AddListener( delegate { Close(); } );

        if (data != null) gameObject.SetActive(false);

        base.Protocol();
    }

    public void Close()
    {
        //data.taken = false;
    }
}
