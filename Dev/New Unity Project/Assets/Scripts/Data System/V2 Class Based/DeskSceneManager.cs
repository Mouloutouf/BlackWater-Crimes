﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeskSceneManager : MonoBehaviour
{
    public PlayerData data;

    public GameObject evidencePrefab;

    public GameObject contentParent;

    public Vector3 initalPos;
    public float offset;

    private float ofst = 0;

    public List<GameObject> sceneObjects;

    public Scene scene;

    void Start()
    {
        foreach (Evidence evidence in data.evidences)
        {
            Instantiation(evidence);
        }
    }

    void Instantiation(Evidence evidence)
    {
        GameObject gO = Instantiate(evidencePrefab) as GameObject;
        gO.transform.SetParent(contentParent.transform, false);

        gO.GetComponent<RectTransform>().anchoredPosition = new Vector3(initalPos.x, initalPos.y + ofst, 0);

        gO.GetComponent<DeskEvidenceObject>().data = evidence;
        sceneObjects.Add(gO);

        ofst += offset;
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
                if (data.evidences[i].name == _object.GetComponent<DeskEvidenceObject>().data.name)
                {
                    data.evidences[i] = _object.GetComponent<DeskEvidenceObject>().data;
                }
            }
        }
    }
}
