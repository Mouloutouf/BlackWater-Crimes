using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class InstantiationProcess : MonoBehaviour
{
    public GameObject prefab;

    public virtual GameObject Instantiation()
    {
        GameObject prefabObject = Instantiate(prefab) as GameObject;

        return prefabObject;
    }
}

public class DeskInstantiationProcess : InstantiationProcess
{
    public Vector3 initalPos;

    public float offset;
    private float ofst = 0;

    public override GameObject Instantiation()
    {
        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(gameObject.transform, false);

        _prefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(initalPos.x, initalPos.y + ofst, 0);
        
        ofst += offset;

        return _prefab;
    }
}
