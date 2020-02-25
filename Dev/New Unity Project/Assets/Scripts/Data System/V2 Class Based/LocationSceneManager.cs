using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LocationSceneManager : MonoBehaviour
{
    public PlayerData data;

    public List<GameObject> contents = new List<GameObject>();

    private List<GameObject> sceneObjects = new List<GameObject>();

    private List<List<GameObject>> allContents = new List<List<GameObject>>();

    public Scene scene;

    void Start()
    {
        ListCreation(); // Adds scene content objects into a list of gameObjects. Also creates the scriptable list if first time ever in scene

        //Initialization();

        foreach (GameObject content in contents) ReplaceWithDataOfType(data.evidences[0]);
    }

    public void ListCreation()
    {
        foreach (GameObject content in contents)
        {
            foreach (Transform trsfrm in content.transform)
            {
                //trsfrm.GetComponent<ObjectData>().

                if (data.isDataContained == false)
                {
                    data.evidences.Add(trsfrm.GetComponent<ObjectData<Evidence>>().data);
                }

                sceneObjects.Add(trsfrm.gameObject); // add into data manager list
            }
        }

        data.isDataContained = true;
    }

    public T AddToList<T>(List<T> TList, T TObject) where T : Data
    {
        return TObject;
    }

    void ReplaceWithDataOfType<T>(T type) where T : Data
    {
        foreach (T _type in data.GetListOfType(type))
        {
            bool match = false;

            foreach (GameObject obj in allContents[0])
            {
                if (_type.code == obj.GetComponent<ObjectData<T>>().data.code)
                {
                    obj.GetComponent<ObjectData<T>>().data = _type;
                }
            }

            if (match == false) Debug.Log("yee object missing, yall need to instantiate thy");
        }
    }

    void Initialization()
    {
        if (data.isDataContained)
        {
            foreach (Evidence evidence in data.evidences)
            {
                int evidenceMatch = 0;

                foreach (GameObject _object in sceneObjects)
                {
                    if (evidence.name == _object.GetComponent<ObjectData<Evidence>>().data.name)
                    {
                        _object.GetComponent<ObjectData<Evidence>>().data = evidence;

                        evidenceMatch = 1;
                    }
                }

                if (evidenceMatch == 0) Debug.LogError(
                    "A GameObject containing Data type Evidence is missing in the Scene. Are you using the right Scene ? " +
                    "If yes, Try clearing the Evidence data from the Player Data Scriptable Object"
                    );
            }
        }
    }

    public void Quit()
    {
        SaveData();

        SceneManager.LoadScene(scene.name);
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

    public T GenericMethod<T>(T param) where T : Data
    {
        return param;
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
        Instantiate(newContent, oldContent.transform.position, Quaternion.identity, contents[0].transform);
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
    #endregion
}
