using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PhotoObject : ObjectData<Evidence>
{
    public GameObject imageObject;
    public GameObject textObject;

    public ElementHolder holder;
    
    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        imageObject.GetComponent<Image>().sprite = data.photo;
        
        if (textObject != null) textObject.GetComponent<Text>().text = data.codeName;

        if (holder != null) holder.seen = data.seen;

        base.Protocol();
    }

    #region Old
    public int pageNumber;

    public int GetTabParent(bool yes, SortMode currentMode)
    {
        return 0;
    }
    #endregion
}
