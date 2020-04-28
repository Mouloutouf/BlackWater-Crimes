using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InstantiatePhotosSpecialist : InstantiationProcess<Evidence>
{
    SpecialistClueShowerScript clueShowerScript;
    int index;

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
        clueShowerScript = Object.FindObjectOfType<SpecialistClueShowerScript>().GetComponent<SpecialistClueShowerScript>();

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

        _prefab.GetComponent<Button>().onClick.AddListener( delegate { clueShowerScript.ShowClue(_prefab); } );

        return _prefab;
    }
}
