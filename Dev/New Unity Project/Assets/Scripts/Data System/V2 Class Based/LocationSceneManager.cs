using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class Content
{
    public GameObject _object;

    public Type type;
    public bool instantiate;
}

public class LocationSceneManager : MonoBehaviour
{
    public PlayerData data;

    public List<Content> contents = new List<Content>();
    
    private List<GameObject> sceneObjects = new List<GameObject>();

    private List<List<GameObject>> allContents = new List<List<GameObject>>();

    void Start()
    {
        //if (data.isDataContained == false)
        
        for (int i = 0; i < contents.Count; i++)
        {
            if (contents[i].instantiate == false) LoadDataOfType(data.evidences[0], i);

            else InstantiateDataOfType(data.evidences[0], i);
        }
    }

    public void List()
    {
        int reference = -1;

        for (int i = 0; i < contents.Count; i++)
        {
            allContents.Add(new List<GameObject>());
            reference++;

            foreach (Transform trsfrm in contents[i]._object.transform)
            {
                allContents[reference].Add(trsfrm.gameObject); // add into data manager list

                if (data.isDataContained == false)
                {
                    //AddToDataListOfType(trsfrm.GetComponent<ObjectData<T>>().GetType(), trsfrm);
                }
            }
        }

        data.isDataContained = true;
    }

    void AddToDataListOfType<T>(T type, Transform transform) where T : Data
    {
        data.GetListOfType(type).Add(transform.GetComponent<ObjectData<T>>().data);
    }

    void LoadDataOfType<T>(T type, int index) where T : Data
    {
        foreach (T _type in data.GetListOfType(type))
        {
            bool isObjectInScene = false;

            foreach (Transform transform in contents[index]._object.transform)
            {
                if (_type.code == transform.gameObject.GetComponent<ObjectData<T>>().data.code)
                {
                    isObjectInScene = true;
                    transform.gameObject.GetComponent<ObjectData<T>>().data = _type;
                }
            }

            if (isObjectInScene == false)
            {
                Debug.Log("yee object missing, yall need to instantiate thy");
                //InstantiateDataOfType(type, reference);
            }
        }
    }

    void InstantiateDataOfType<T>(T type, int index) where T : Data
    {
        foreach (T _type in data.GetListOfType(type))
        {
            GameObject instance = contents[index]._object.GetComponent<InstantiationProcess>().Instantiation();

            instance.GetComponent<ObjectData<T>>().data = _type;
        }
    }

    #region useless methods
    public void Add(GameObject content)
    {
        Debug.Log("Added : " + content);
        //Instantiate(content, new Vector3(2, 3, 0), Quaternion.identity, contents[0].transform);
    }

    public void Remove(GameObject content)
    {
        Debug.Log("Removed : " + content);
        Destroy(content);
    }

    public void Replace(GameObject oldContent, GameObject newContent)
    {
        Debug.Log("Replaced : " + oldContent + " with " + newContent);
        Instantiate(newContent, oldContent.transform.position, Quaternion.identity, contents[0]._object.transform);
        Destroy(oldContent);
    }

    public Evidence UpdateData(Evidence evidence1, Evidence evidence2)
    {
        evidence2 = evidence1;

        return evidence2;
    }

    public void AddObject(Evidence evidence)
    {

    }

    public void Quit()
    {
        SaveData();

        SceneManager.LoadScene("yes");
    }

    void SaveData()
    {
        for (int i = 0; i < data.evidences.Count; i++)
        {
            foreach (GameObject _object in sceneObjects)
            {
                if (data.evidences[i].name == _object.GetComponent<ObjectData<Evidence>>().data.name)
                {
                    data.evidences[i] = _object.GetComponent<ObjectData<Evidence>>().data;
                }
            }
        }
    }
    #endregion
}
