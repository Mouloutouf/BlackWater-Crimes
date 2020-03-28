using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ObjectData<T> : MonoBehaviour where T : Data
{
    private GameData gameData;

    public T data;

    protected bool loaded;

    void Awake()
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

    public void LoadDataOfType<_T>(_T type) where _T : Data
    {
        foreach (_T _type in gameData.GetListOfType(type))
        {
            if (_type.index == data.index)
            {
                data = _type as T;
            }
        }
    }

    public void AddDataToList<_T>(_T type) where _T : Data
    {
        gameData.GetListOfType(type).Add(data as _T);
    }
}

public class DataObject : ObjectData<Data>
{
    private Data myType;

    void Start()
    {
        LoadDataOfType(myType);
    }

    // Update() is called in Parent Class !

    public override void Protocol()
    {
        base.Protocol();
    }
}
