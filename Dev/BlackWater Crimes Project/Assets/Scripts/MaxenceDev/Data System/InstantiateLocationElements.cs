using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InstantiateLocationElements : InstantiateElements<Location>
{
    protected override List<List<Location>> GetAllElements()
    {
        List<List<Location>> mainList = new List<List<Location>>();

        List<Location> allLocations = new List<Location>();

        foreach (Location location in gameData.locations)
        {
            allLocations.Add(location);
        }

        mainList.Add(allLocations);

        return mainList;
    }

    protected override bool Check(Location data)
    {
        bool check = data.unlockedData && data.known;

        return check;
    }
    
    protected override string GetDataName(Location data)
    {
        return data.locationAdress;
    }
}
