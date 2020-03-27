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
        gameData = GameObject.FindWithTag("Scene Manager").GetComponent<SceneManager>().gameData;
    }

    void Update()
    {
        if (!loaded)
        {
            Protocol();
        }

        Check();
    }

    public virtual void Protocol()
    {
        loaded = true;
    }

    public virtual void Check()
    {

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

public class TestEvidenceObject : ObjectData<Evidence>
{
    private Evidence myType;

    void Start()
    {
        LoadDataOfType(myType);
    }

    public override void Protocol()
    {
        base.Protocol();
    }

    public override void Check()
    {
        if (data.taken) gameObject.SetActive(false);
    }
}
