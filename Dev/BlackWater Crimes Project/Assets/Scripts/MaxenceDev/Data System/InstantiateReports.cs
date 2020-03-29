using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateReports : InstantiationProcess<Report>
{
    void Start()
    {
        GetGameData();

        foreach (List<Report> _list in gameData.allReports.Values)
        {
            InstantiateDataOfType(type, _list);
        }
    }

    public override GameObject Instantiation()
    {
        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(this.transform, false);

        return _prefab;
    }
}
