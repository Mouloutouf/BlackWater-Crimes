using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSceneManager : MonoBehaviour
{
    public PlayerData data;

    public List<GameObject> contents = new List<GameObject>();

    private List<GameObject> sceneObjects = new List<GameObject>();

    public Scene scene;

    void Start()
    {
        ListCreation(); // Adds scene content objects into a list of gameObjects. Also creates the scriptable list if first time ever in scene

        Initialization();
    }

    void ListCreation()
    {
        foreach (GameObject content in contents)
        {
            foreach (Transform T in content.transform)
            {
                if (data.isDataContained == false)
                {
                    data.evidences.Add(T.GetComponent<ObjectData>().evidence);
                }

                sceneObjects.Add(T.gameObject); // add into data manager list
            }
        }

        data.isDataContained = true;
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
                    if (evidence.name == _object.GetComponent<ObjectData>().evidence.name)
                    {
                        _object.GetComponent<ObjectData>().evidence = evidence;

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
                if (data.evidences[i].name == _object.GetComponent<ObjectData>().evidence.name)
                {
                    data.evidences[i] = _object.GetComponent<ObjectData>().evidence;
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
