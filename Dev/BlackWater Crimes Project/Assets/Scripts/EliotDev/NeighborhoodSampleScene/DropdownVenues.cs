using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownVenues : MonoBehaviour
{
    GameData gameData;
    List<string> venues = new List<string>();
    [SerializeField] bool attorneyDropdown;

    private void Start()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;

        if(!attorneyDropdown)
        {
            foreach(Location location in gameData.locations)
            {
                if(location.accessible)
                {
                    //venues.Add(location.locationAddress);
                }
            }
        }
        else
        {
            foreach(Location location in gameData.locations)
            {
                if(location.visible && !location.accessible)
                {
                    //venues.Add(location.locationAddress);
                }
            }
        }

        //GetComponent<Dropdown>().AddOptions(venues);
    }
}
