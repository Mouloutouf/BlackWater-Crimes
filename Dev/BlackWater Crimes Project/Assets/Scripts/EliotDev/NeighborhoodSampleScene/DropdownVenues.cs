using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownVenues : MonoBehaviour
{
    GameData gameData;
    public Dictionary<string, Locations> _venues = new Dictionary<string, Locations>();
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
                    //_venues.Add(locationAddress, location.myLocation);
                }
            }
        }
        else
        {
            foreach(Location location in gameData.locations)
            {
                if(location.visible && !location.accessible)
                {
                    //_venues.Add(locationAddress, location.myLocation);
                }
            }
        }

        //GetComponent<Dropdown>().AddOptions(_venues.Keys);
    }
}
