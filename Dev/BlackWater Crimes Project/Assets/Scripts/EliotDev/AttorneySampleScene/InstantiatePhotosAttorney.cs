using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InstantiatePhotosAttorney : InstantiationProcess<Evidence>
{
    [SerializeField] List<Vector2> photoPositions = new List<Vector2>();
    AttorneySingleClueShowerScript clueShowerScript;
    int index;

    public override GameObject Instantiation()
    {
        clueShowerScript = Object.FindObjectOfType<AttorneySingleClueShowerScript>().GetComponent<AttorneySingleClueShowerScript>();

        GameObject _prefab = Instantiate(prefab) as GameObject;

        _prefab.transform.SetParent(transform);
        _prefab.GetComponent<RectTransform>().anchoredPosition = photoPositions[index];
        index++;

        _prefab.GetComponent<Button>().onClick.AddListener( delegate { clueShowerScript.ShowClue(_prefab); } );

        return _prefab;
    }
}
