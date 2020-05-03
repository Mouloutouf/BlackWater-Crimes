using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InstantiatePhotosSpecialist : InstantiationProcess<Evidence>
{
    SpecialistClueShowerScript clueShowerScript;
    public int cluesPerRow;
    int index;
    int indexInRow;
    int rowNumber;
    float yPos;
    float xPos;

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

        _prefab.GetComponent<Button>().onClick.AddListener( delegate { clueShowerScript.ShowClue(_prefab); } );
        
        index++;
        indexInRow ++;

        return _prefab;
    }
}
