using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crime Scene Data", menuName = "Crime Scene Data Scriptable")]
public class SceneData : ScriptableObject
{
    public bool isDataContained = false;

    public List<GameObject> objects = new List<GameObject>();
}
