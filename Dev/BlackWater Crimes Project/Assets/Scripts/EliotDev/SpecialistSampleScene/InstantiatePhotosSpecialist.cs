using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InstantiatePhotosSpecialist : InstantiationProcess<Evidence>
{
    private SpecialistEvidenceDisplayer evidenceDisplayer;

    public Transform content;

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
        evidenceDisplayer = Object.FindObjectOfType<SpecialistEvidenceDisplayer>().GetComponent<SpecialistEvidenceDisplayer>();

        GameObject _prefab = Instantiate(prefab) as GameObject;

        _prefab.transform.SetParent(content);

        if (index % cluesPerRow == 0)
        {
            indexInRow = 0;
            rowNumber ++;

            yPos = -(300*rowNumber);

            if (rowNumber >= 2)
            {
                content.parent.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 300);
            }
        }

        xPos = (350 * indexInRow) - 500;

        _prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);

        _prefab.GetComponent<Button>().onClick.AddListener( delegate { evidenceDisplayer.ShowClue(_prefab); } );
        
        index++;
        indexInRow ++;

        return _prefab;
    }
}
