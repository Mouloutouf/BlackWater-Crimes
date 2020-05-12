using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

[Serializable]
public class ObjectData<T> : MonoBehaviour where T : Data
{
    protected GameData gameData;

    [Title("DATA")]
    public T data;

    protected bool loaded;

    [Space()]
    public bool instantiate; // Prefabs only, indicates no loading of Data after their instantiation

    [HideInInspector] public bool hasApplied; // Used by the Data Container to determine if a DataObject has applied to a List

    public void GetGameData()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
    }

    void Update()
    {
        if (!loaded)
        {
            Protocol();
        }
    }

    public virtual void Protocol()
    {
        loaded = true;
    }

    public void LoadDataOfType<_T>(List<_T> list) where _T : Data
    {
        foreach (_T _data in list)
        {
            if (_data.index == data.index)
            {
                data = _data as T;
            }
        }
    }
}

public class DataObject : ObjectData<Data>
{
    void Start()
    {
        GetGameData();

        LoadDataOfType(new List<Data>()); // Place Holder
    }

    // Update() is called in Parent Class !

    public override void Protocol()
    {
        base.Protocol();
    }
}
