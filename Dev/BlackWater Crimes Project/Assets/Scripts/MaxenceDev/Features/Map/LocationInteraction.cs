using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LocationInteraction : SerializedMonoBehaviour
{
    private GameData gameData;

    public GameObject utilities;

    public Camera _camera;

    public GameObject locationMenu;
    public Text menuName;
    public Text menuDescription;
    public Text menuClueCount;

    public GameObject accessButton;
    public GameObject blockedButton;

    private LocationObject locationObject;

    public Dictionary<Locations, string> sceneNames = new Dictionary<Locations, string>();

    void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;

        foreach (Transform tr in transform)
        {
            Location location = tr.GetComponent<LocationObject>().data;

            if (!location.unlockedData)
            {
                location.unlockedData = true;
                gameData.locations.Add(location);
            }
        }
    }

    void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(_camera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1));

        if (hits.Count() > 0 && hits[0].transform.GetComponent<CircleCollider2D>() != null && Input.GetMouseButtonDown(0))
        {
            locationObject = hits[0].transform.GetComponent<LocationObject>();

            if (!locationObject.data.visible)
            {
                RevealLocation(locationObject);
                locationObject.data.visible = true;
                locationObject.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                OpenLocationMenu(locationObject);
            }
        }
        /*
        else if (Input.GetMouseButtonDown(0) && locationObject != null)
        {
            locationObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        */
    }

    void RevealLocation(LocationObject _object)
    {
        _object.locationSprite.SetActive(true);

        //Particle Effects & anims & sounds etc.
    }

    void OpenLocationMenu(LocationObject _object)
    {
        locationMenu.SetActive(true);
        menuName.text = _object.data.locationName;
        menuDescription.text = _object.data.locationDescription;
        menuClueCount.text = "Evidences found  :  " + _object.data.evidenceCollected.ToString();

        accessButton.GetComponent<Button>().onClick.AddListener(delegate 
        { 
            utilities.GetComponent<SceneLoaderSimple>().LoadScene(sceneNames[_object.data.myLocation]); 
        }
        );

        if (!_object.data.accessible)
        {
            accessButton.SetActive(false);
            blockedButton.SetActive(true);
        }
        else
        {
            accessButton.SetActive(true);
            blockedButton.SetActive(false);
        }
    }

    public void CloseLocationMenu()
    {
        locationMenu.SetActive(false);
    }
}
