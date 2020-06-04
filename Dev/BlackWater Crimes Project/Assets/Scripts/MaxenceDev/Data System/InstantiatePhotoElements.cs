using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InstantiatePhotoElements : InstantiateElements<Evidence>
{
    public ZoomPhoto zoomPhoto;

    protected override List<List<Evidence>> GetAllElements()
    {
        List<List<Evidence>> mainList = new List<List<Evidence>>();

        foreach (List<Evidence> evidencesList in gameData.allEvidences.Values)
        {
            mainList.Add(evidencesList);
        }

        return mainList;
    }

    void Start()
    {
        // InstantiateContents();

        Initialize();
    }

    protected override void AdditionalSettings(GameObject __prefab)
    {
        __prefab.transform.GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(delegate { zoomPhoto.ZoomObject(__prefab); } );

        __prefab.GetComponent<ElementHolder>().bind = __prefab;
    }

    protected override string GetDataName(Evidence data)
    {
        return data.codeName;
    }
}
