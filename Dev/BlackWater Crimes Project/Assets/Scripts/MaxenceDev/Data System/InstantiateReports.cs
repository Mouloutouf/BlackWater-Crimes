using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstantiateReports : InstantiationProcess<Report>
{
    public Transform contentHolder;
    public GameObject noPrefab;

    private List<Report> instantiationList = new List<Report>();

    void Start()
    {
        GetGameData();

        foreach (List<Report> _list in gameData.allReports.Values)
        {
            foreach (Report report in _list)
            {
                if (report.unlockedData)
                {
                    instantiationList.Add(report);
                }
            }
        }

        if (instantiationList.Count == 0) Instantiation(noPrefab);

        else
        {
            instantiationList = instantiationList.OrderBy(w => w.unlockOrderIndex).ToList();
            instantiationList.Reverse();

            InstantiateDataOfType(type, instantiationList);
        }
    }

    public override GameObject Instantiation(GameObject prefab)
    {
        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(contentHolder, false);

        return _prefab;
    }
}
