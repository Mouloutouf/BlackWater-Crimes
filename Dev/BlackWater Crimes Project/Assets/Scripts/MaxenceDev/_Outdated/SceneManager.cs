using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum DataTypes
{
    Evidence,
    Note,
    Report,
    Location
}

// Content Class
// Utilisation dans Unity ---> La Scène est définie par plusieurs Game Objects 'Content' / 'Contenu' qui contiennent plusieurs objets enfants d'un certain Data Type.
// Exemple : Game Object Content 'Scene Evidences', qui contient l'ensemble des Game Objects 'Evidence' / 'Indice' de la Scène en enfants
// Utilisation dans Script ---> Permet de référencer directement l'objet 'Content' en question et de définir d'autres variables utiles quant au Load de Data
[Serializable]
public class Content
{
    public GameObject contentObject;

    //instantiate var
    //Résumé : Est-ce que le 'Content' doit instantier ses enfants ou sont-ils déjà présents dans la Scène
    public bool instantiate;

    // enumType var
    // Résumé : Le Data Type des enfants de 'Content'
    public DataTypes dataType;
}

// Load for all objects
public class SceneManager : MonoBehaviour
{
    public GameData gameData;

    public List<Content> contents = new List<Content>();

    private List<List<GameObject>> allContents = new List<List<GameObject>>();

    public Dictionary<DataTypes, int> dataTypes = new Dictionary<DataTypes, int>
    {
        {DataTypes.Evidence, 0},
        {DataTypes.Note, 1},
        {DataTypes.Report, 2},
        {DataTypes.Location, 3}
    };

    void Start()
    {
        for (int i = 0; i < contents.Count; i++)
        {
            if (!contents[i].instantiate)
                LoadDataOfType(gameData.evidences[Locations.Docks][0], i); //GetDataType(contents[i].dataType)
            else
                InstantiateDataOfType(gameData.evidences[Locations.Docks][0], i); //GetDataType(contents[i].dataType)
        }

        object type = GetType();
    }
    
    void AddToDataListOfType<T>(T type, Transform transform) where T : Data
    {
        gameData.GetListOfType(type).Add(transform.GetComponent<ObjectData<T>>().data);
    }

    public void LoadDataOfType<T>(T type, int index) where T : Data
    {
        foreach (T _type in gameData.GetListOfType(type))
        {
            foreach (Transform transform in contents[index].contentObject.transform)
            {
                if (_type.index == transform.gameObject.GetComponent<ObjectData<T>>().data.index)
                {
                    transform.gameObject.GetComponent<ObjectData<T>>().data = _type;
                }
            }
        }
    }

    public void InstantiateDataOfType<T>(T type, int index) where T : Data
    {
        foreach (T _type in gameData.GetListOfType(type))
        {
            GameObject prefab = contents[index].contentObject.GetComponent<InstantiationProcess<T>>().prefab;
            GameObject instance = contents[index].contentObject.GetComponent<InstantiationProcess<T>>().Instantiation(prefab);
            instance.GetComponent<ObjectData<T>>().data = _type;
        }
    }

    public Data GetDataType(DataTypes enumType)
    {
        switch (dataTypes[enumType])
        {
            case 0:
                return new Evidence();
            case 1:
                return new Note();
            case 2:
                return new Report();
            case 3:
                return new Location();
            default:
                return null;
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
        Instantiate(newContent, oldContent.transform.position, Quaternion.identity, contents[0].contentObject.transform);
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

    /*
    public void List()
    {
        int reference = -1;

        for (int i = 0; i < contents.Count; i++)
        {
            allContents.Add(new List<GameObject>());
            reference++;

            foreach (Transform trsfrm in contents[i].contentObject.transform)
            {
                allContents[reference].Add(trsfrm.gameObject); // add into data manager list

                if (gameData.isDataContained == false)
                {
                    //AddToDataListOfType(trsfrm.GetComponent<ObjectData<T>>().GetType(), trsfrm);
                }
            }
        }

        gameData.isDataContained = true;
    }
    */
    #endregion
}
