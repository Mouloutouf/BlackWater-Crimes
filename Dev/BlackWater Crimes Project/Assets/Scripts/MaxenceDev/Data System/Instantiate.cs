using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class InstantiationProcess<T> : MonoBehaviour where T : Data
{
    protected T type;

    protected GameData gameData;

    [Title("Prefabs")]

    public GameObject prefab;
    
    public void GetGameData()
    {
        gameData = GameObject.Find("Data Container").GetComponent<DataContainer>().gameData;
    }

    public void InstantiateDataOfType<_T>(_T type, List<_T> list) where _T : Data
    {
        foreach (_T _data in list)
        {
            if (_data.unlockedData)
            {
                GameObject instance = Instantiation(this.prefab);
                instance.GetComponent<ObjectData<_T>>().data = _data;
            }
        }
    }

    public void InstantiateDataOfType<_T>(_T type, List<_T> list, GameObject prefab) where _T : Data
    {
        foreach (_T _data in list)
        {
            if (_data.unlockedData)
            {
                GameObject instance = Instantiation(prefab);
                instance.GetComponent<ObjectData<_T>>().data = _data;
            }
        }
    }

    public virtual GameObject Instantiation(GameObject prefab)
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

    public override GameObject Instantiation(GameObject prefab)
    {

        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(gameObject.transform, false);

        _prefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(initalPos.x, initalPos.y + ofst, 0);
        
        ofst += offset;

        return _prefab;
    }
}
