using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationObject : ObjectData<Location>
{
    public GameObject locationSprite;

    public GameObject menuAccessButton;
    public GameObject menuBlockedButton;
    
    public override void Protocol()
    {
        if (!data.known) gameObject.SetActive(false);
        if (!data.visible) locationSprite.SetActive(false);
        if (!data.accessible) { menuAccessButton.SetActive(false); menuBlockedButton.SetActive(true); }

        //if (data.completed); // code for completion location active

        base.Protocol();
    }

    public void OnMouseOver()
    {
        if (data.visible) transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
}
