using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoObject : ObjectData<Evidence>
{
    public override void Protocol()
    {
        transform.GetChild(1).GetComponent<Image>().sprite = data.photo;

        base.Protocol();
    }
}
