using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InstantiatePhotosAttorney : InstantiationProcess<Evidence>
{
    AttorneySingleClueShowerScript singleClueShowerScript;
    AttorneyClueShowerScript clueShowerScript;
    int index;
    bool singleClueShower;

    void Start()
    {
        GetGameData();

        foreach (List<Evidence> _list in gameData.allEvidences.Values)
        {
            InstantiateDataOfType(type, _list);
        }
    }

    public override GameObject Instantiation(GameObject prefab)
    {
        if(singleClueShower) singleClueShower = Object.FindObjectOfType<AttorneySingleClueShowerScript>().GetComponent<AttorneySingleClueShowerScript>();
        else clueShowerScript = Object.FindObjectOfType<AttorneyClueShowerScript>().GetComponent<AttorneyClueShowerScript>();

        GameObject _prefab = Instantiate(prefab) as GameObject;

        _prefab.transform.SetParent(transform);

        if(index % 2 == 0) //index is even
        {
            int xPos = -150;
            int yPos = -(150*index) + 500;
            _prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
            if(index >= 8)
            {
                transform.parent.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 300);
            }
        } 
        else //index is odd
        {
            int xPos = 150;
            int yPos = -(150*(index-1)) + 500;
            _prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
        }

        index++;

        if(singleClueShower) _prefab.GetComponent<Button>().onClick.AddListener( delegate { singleClueShowerScript.ShowClue(_prefab); } );
        else _prefab.GetComponent<Button>().onClick.AddListener( delegate { clueShowerScript.ShowClue(_prefab); } );

        return _prefab;
    }
}
