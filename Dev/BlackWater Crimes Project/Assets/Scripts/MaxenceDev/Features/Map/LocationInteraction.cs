﻿using Sirenix.OdinInspector;
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
    public Localisation menuNameKey;
    public Localisation menuDescriptionKey;
    public Image menuArtwork;
    public Localisation menuAddress;

    public GameObject accessButton;
    public GameObject blockedButton;

    private LocationObject locationObject;

    public Dictionary<Locations, string> sceneNames = new Dictionary<Locations, string>();

    public AudioSource source;
    public AudioClip revealSound;
    public AudioClip clickSound;

    void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
    }
    
    void Update()
    {
        if (!_camera.gameObject.GetComponent<DragAndZoom>().zoomed)
        {
            foreach (Transform tr in transform) tr.GetChild(1).gameObject.SetActive(false);

            return;
        }
        else foreach (Transform tr in transform) if (tr.GetComponent<LocationObject>().data.visible) tr.GetChild(1).gameObject.SetActive(true);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(_camera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1));

            if (hits.Count() > 0 && hits[0].transform.GetComponent<CircleCollider2D>() != null)
            {
                locationObject = hits[0].transform.GetComponent<LocationObject>();

                if (!locationObject.data.visible)
                {
                    RevealLocation(locationObject);
                }
                else
                {
                    OpenLocationMenu(locationObject);
                }
            }
        }
    }

    void RevealLocation(LocationObject _object)
    {
        _object.locationSprite.SetActive(true);
        _object.data.visible = true;
        _object.data.accessible = true;

        //Particle Effects & anims & sounds etc.

        utilities.GetComponent<VibrateSystem>().PhoneVibrate();
        source.PlayOneShot(revealSound);
        _object.transform.GetChild(2).GetComponent<ParticleSystem>().Play();
    }

    void OpenLocationMenu(LocationObject _object)
    {
        locationMenu.SetActive(true);
        menuNameKey.key = _object.data.nameKey; menuNameKey.RefreshText();
        menuDescriptionKey.key = _object.data.descriptionKey; menuDescriptionKey.RefreshText();
        menuArtwork.sprite = _object.data.locationArtwork;
        menuAddress.key = _object.data.addressKey; menuAddress.RefreshText();

        accessButton.GetComponent<Button>().onClick.AddListener(delegate 
        { 
            utilities.GetComponent<SceneLoaderSimple>().LoadScene(sceneNames[_object.data.myLocation]); 
        }
        );

        accessButton.GetComponent<Button>().onClick.AddListener(delegate 
        { 
            utilities.GetComponent<SceneLoaderSimple>().WithLoadingScreen(true);
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

        source.PlayOneShot(clickSound);
    }

    public void CloseLocationMenu()
    {
        locationMenu.SetActive(false);

        source.PlayOneShot(clickSound);
    }
}
