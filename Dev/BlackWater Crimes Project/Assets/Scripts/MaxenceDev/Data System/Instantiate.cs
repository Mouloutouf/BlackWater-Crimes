using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiationProcess<T> : MonoBehaviour where T : Data
{
    protected T type;

    protected GameData gameData;

    public GameObject prefab;
    
    public void GetGameData()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
    }

    public void InstantiateDataOfType<T>(T type, List<T> list) where T : Data
    {
        foreach (T _data in list)
        {
            if (_data.unlockedData)
            {
                GameObject instance = Instantiation();
                instance.GetComponent<ObjectData<T>>().data = _data;
            }
        }
    }

    public virtual GameObject Instantiation()
    {
        return new GameObject(); // PlaceHolder
    }
}

public class Instantiate : InstantiationProcess<Evidence>
{
    public Vector3 initalPos;

    public float offset;
    private float ofst = 0;

    void Start()
    {
        GetGameData();

        foreach (List<Evidence> _list in gameData.allEvidences.Values)
        {
            InstantiateDataOfType(type, _list);
        }
        
    }

    public override GameObject Instantiation()
    {

        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(gameObject.transform, false);

        _prefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(initalPos.x, initalPos.y + ofst, 0);
        
        ofst += offset;

        return _prefab;
    }
}
