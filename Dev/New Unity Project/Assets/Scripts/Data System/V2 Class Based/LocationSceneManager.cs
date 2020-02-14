using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSceneManager : MonoBehaviour
{
    public LocationData locationData;

    public List<GameObject> contents = new List<GameObject>();

    private List<GameObject> sceneObjects = new List<GameObject>();

    void Start()
    {
        ListCreation(); // Adds scene content objects into a list of gameObjects. Also creates the scriptable list if first time ever in scene

        Initialization();
    }

    void Update()
    {
        foreach (Transform T in contents[0].transform)
        {
            //if (T.GetComponent<EvidenceObject>().evidence.taken)
        }
    }

    void ListCreation()
    {
        foreach (GameObject content in contents)
        {
            foreach (Transform T in content.transform)
            {
                if (locationData.isDataContained == false)
                {
                    locationData.evidences.Add(T.GetComponent<EvidenceObject>().evidence);
                }

                sceneObjects.Add(T.gameObject); // add into data manager list
            }
        }

        locationData.isDataContained = true;
    }

    void Initialization()
    {
        if (locationData.isDataContained)
        {
            foreach (Evidence evidence in locationData.evidences)
            {
                int evidenceMatch = 0;

                foreach (GameObject _object in sceneObjects)
                {
                    if (evidence.name == _object.GetComponent<EvidenceObject>().evidence.name)
                    {
                        _object.GetComponent<EvidenceObject>().evidence = UpdateData(evidence, _object.GetComponent<EvidenceObject>().evidence);

                        evidenceMatch = 1;
                    }
                }

                if (evidenceMatch == 0) Debug.LogError(
                    "A GameObject of type Evidence is missing in the Scene. Are you using the right Scene ? " +
                    "If yes, Try clearing the data from the Scene's associated Scriptable Object"
                    );
            }
        }
    }

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
}
