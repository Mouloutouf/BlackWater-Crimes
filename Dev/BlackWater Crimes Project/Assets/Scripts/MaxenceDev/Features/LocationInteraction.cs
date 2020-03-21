using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LocationInteraction : MonoBehaviour
{
    public Camera _camera;

    public GameObject locationMenu;
    public Text menuName;

    public GameObject accessButton;
    public GameObject blockedButton;
    
    void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(_camera.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1));

        if (hits.Count() > 0 && hits[0].transform.GetComponent<CircleCollider2D>() != null && Input.GetMouseButtonDown(0))
        {
            LocationObject locationObject = hits[0].transform.GetComponent<LocationObject>();

            if (!locationObject.data.visible)
            {
                RevealLocation(locationObject);
                locationObject.data.visible = true;
            }
            else
            {
                OpenLocationMenu(locationObject);
            }
        }
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

        if (!_object.data.accessible)
        {
            Debug.Log("t'as rien à faire là en fait");
            accessButton.SetActive(false);
            blockedButton.SetActive(true);
        }
        else
        {
            Debug.Log("ce qui devrait être fait");
            accessButton.SetActive(true);
            blockedButton.SetActive(false);
        }
    }

    public void CloseLocationMenu()
    {
        locationMenu.SetActive(false);
    }
}
