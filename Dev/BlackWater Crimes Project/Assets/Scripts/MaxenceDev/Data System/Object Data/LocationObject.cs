using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class LocationObject : ObjectData<Location>
{
    [Title("PROPERTIES")]

    public Camera cam;

    public GameObject locationSprite;
    public GameObject locationName;
    
    public GameObject menuAccessButton;
    public GameObject menuBlockedButton;
    
    void Start()
    {
        GetGameData();

        LoadDataOfType(gameData.locations);
    }

    public override void Protocol()
    {
        if (!data.known) gameObject.SetActive(false);
        if (!data.visible) locationSprite.SetActive(false);
        if (!data.accessible) { menuAccessButton.SetActive(false); menuBlockedButton.SetActive(true); }

        locationName.GetComponentInChildren<LocalisationMesh>().key = data.nameKey;
        locationName.GetComponentInChildren<LocalisationMesh>().RefreshText();

        if (!data.visible) locationName.SetActive(false);
        
        base.Protocol();
    }

    void Update()
    {
        if (!loaded)
        {
            Protocol();
        }
    }
}
