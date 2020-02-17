using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskSceneManager : MonoBehaviour
{
    public LocationData locationData;

    public GameObject evidencePrefab;

    public GameObject contentParent;

    public Vector3 initalPos;
    public float offset;

    private float ofst = 0;

    void Start()
    {
        foreach(Evidence evidence in locationData.evidences)
        {
            Instantiation(evidence);
        }
    }

    void Update()
    {
        
    }

    void Instantiation(Evidence evidence)
    {
        GameObject gO = Instantiate(evidencePrefab) as GameObject;
        gO.transform.SetParent(contentParent.transform, false);

        gO.GetComponent<RectTransform>().anchoredPosition = new Vector3(initalPos.x, initalPos.y + ofst, 0);

        gO.GetComponent<DeskEvidenceObject>().evidence = evidence;
        //Instantiate(evidencePrefab, new Vector3(initalPos.x, initalPos.y + ofst, 0), Quaternion.identity, contentParent.transform);

        ofst += offset;
    }
}
