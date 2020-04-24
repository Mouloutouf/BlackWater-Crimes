using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationObject : ObjectData<Location>
{
    public Camera cam;

    public GameObject locationSprite;
    public GameObject locationName;

    public GameObject menuAccessButton;
    public GameObject menuBlockedButton;
    
    private Location myType = new Location();

    void Start()
    {
        GetGameData();

        if (!instantiate) LoadDataOfType(myType, gameData.locations);
    }

    public override void Protocol()
    {
        if (!data.known) gameObject.SetActive(false);
        if (!data.visible) locationSprite.SetActive(false);
        if (!data.accessible) { menuAccessButton.SetActive(false); menuBlockedButton.SetActive(true); }

        locationName.GetComponentInChildren<TextMesh>().text = data.locationName;

        if (!data.visible) locationName.SetActive(false);
        
        //if (data.completed); // code for completion location active

        base.Protocol();
    }

    void Update()
    {
        if (!loaded)
        {
            Protocol();
        }
        /*
        RaycastHit2D[] hits = Physics2D.RaycastAll(cam.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1));

        if (hits.Count() > 0 && hits[0].transform.GetComponent<CircleCollider2D>() != null)
        {
            if (data.visible) transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        */
    }
}
