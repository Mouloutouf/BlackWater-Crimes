using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiationProcess<T> : MonoBehaviour where T : Data
{
    T type;

    private GameData gameData;

    public GameObject prefab;

    void Start()
    {
        gameData = GameObject.FindWithTag("Scene Manager").GetComponent<SceneManager>().gameData;

        //T type = typeof(T) as T;
        //Debug.Log(type.GetType());
        int index = 0;

        foreach (T _type in gameData.GetListOfType(this.type))
        {
            GameObject instance = Instantiation();
            instance.GetComponent<ObjectData<T>>().data = _type;
            index++;
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

    // Start is already called in Parent Class !

    public override GameObject Instantiation()
    {
        GameObject _prefab = Instantiate(prefab) as GameObject;
        _prefab.transform.SetParent(gameObject.transform, false);

        _prefab.GetComponent<RectTransform>().anchoredPosition = new Vector3(initalPos.x, initalPos.y + ofst, 0);
        
        ofst += offset;

        return _prefab;
    }
}
