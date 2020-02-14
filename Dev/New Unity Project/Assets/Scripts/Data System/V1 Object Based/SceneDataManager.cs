using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataManager : MonoBehaviour
{
    public SceneData data;

    public List<GameObject> contents = new List<GameObject>();

    private List<GameObject> objects = new List<GameObject>();

    void Start()
    {
        ListCreation(); // Adds scene content objects into a list of gameObjects. Also creates the scriptable list if first time ever in scene

        Initialization();
    }

    void Update()
    {
        
    }

    void ListCreation()
    {
        foreach (GameObject content in contents)
        {
            foreach (Transform T in content.transform)
            {
                if (data.isDataContained == false)
                {
                    data.initialObjects.Add((GameObject)Resources.Load(T.name, typeof(GameObject))); // add into scriptable data list
                    data.objects = data.initialObjects;
                }
                
                objects.Add(T.gameObject); // add into data manager list
            }
        }

        data.isDataContained = true;
    }

    void Initialization()
    {
        if (data.isDataContained)
        {
            List<Reference> dataRefs = new List<Reference>();
            foreach (GameObject obj in data.objects) dataRefs.Add(obj.GetComponent<Reference>());

            List<Reference> contentRefs = new List<Reference>();
            foreach (GameObject gObj in objects) contentRefs.Add(gObj.GetComponent<Reference>());

            foreach (Reference dRef in dataRefs)
            {
                int index = 1;

                foreach (Reference cRef in contentRefs)
                {
                    if (dRef.referencePrefix == cRef.referencePrefix)
                    {
                        if (dRef.referenceSuffix != cRef.referenceSuffix)
                        {
                            Replace(cRef.gameObject, dRef.gameObject);
                        }

                        index = 0;
                        //(is obj == gObj ? return : data.objects.Remove(obj));
                    }

                    else
                    {
                        index = 2;

                        foreach (Reference refD in dataRefs)
                        {
                            if (cRef.referencePrefix == refD.referencePrefix) index = 0;
                        }
                    }

                    if (index == 2) Remove(cRef.gameObject);
                }

                if (index == 1) Add(dRef.gameObject);
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
}
