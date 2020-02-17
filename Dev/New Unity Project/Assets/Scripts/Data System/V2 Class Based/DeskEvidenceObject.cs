using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeskEvidenceObject : MonoBehaviour
{
    public Evidence evidence = new Evidence();

    private bool loaded;

    public List<GameObject> objects = new List<GameObject>();

    void Start()
    {

    }

    void Update()
    {
        if (!loaded)
        {
            Protocol();
        }
    }

    void Protocol()
    {
        objects[0].GetComponent<Image>().sprite = evidence.render2D;

        objects[1].GetComponent<Text>().text = evidence.name;

        objects[2].GetComponent<Text>().text = evidence.description;

        loaded = true;
    }
}
