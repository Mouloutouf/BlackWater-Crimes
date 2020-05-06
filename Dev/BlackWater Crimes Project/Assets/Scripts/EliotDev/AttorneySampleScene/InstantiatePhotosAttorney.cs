using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InstantiatePhotosAttorney : InstantiationProcess<Evidence>
{
    AttorneySingleClueShowerScript singleClueShowerScript;
    AttorneyClueShowerScript clueShowerScript;
    public int cluesPerRow;
    int index;
    int indexInRow;
    int rowNumber;
    float yPos;
    float xPos;
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

        if(index % cluesPerRow == 0)
        {
            indexInRow = 0;
            rowNumber ++;
            yPos = -(300*rowNumber);
            if(rowNumber >= 2)
            {
                transform.parent.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 300);
            }
        }

        xPos = (350 * indexInRow) - 500;

        _prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);

        if(singleClueShower) _prefab.GetComponent<Button>().onClick.AddListener( delegate { singleClueShowerScript.ShowClue(_prefab); } );
        else _prefab.GetComponent<Button>().onClick.AddListener( delegate { clueShowerScript.ShowClue(_prefab); } );

        index++;
        indexInRow ++;

        return _prefab;
    }
}
