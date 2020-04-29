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

        if (!attorneyDropdown) // Neighborhood Investigation
        {
            foreach (Location location in gameData.locations)
            {
                if (location.accessible)
                {
                    _venues.Add(location.locationAdress, location.myLocation);
                }
            }
        }
        else // Attorney Unlock Location
        {
            foreach (Location location in gameData.locations)
            {
                if (location.visible && !location.accessible)
                {
                    _venues.Add(location.locationAdress, location.myLocation);
                }
            }

            if (_venues.Values.Count == 0) GetComponentInChildren<Dropdown>().interactable = false;
        }

        GetComponentInChildren<Dropdown>().ClearOptions();
        List<string> listOfKeys = new List<string>();
        foreach (string key in _venues.Keys)
        {
            listOfKeys.Add(key);
        }
        GetComponentInChildren<Dropdown>().AddOptions(listOfKeys);
    }
}
