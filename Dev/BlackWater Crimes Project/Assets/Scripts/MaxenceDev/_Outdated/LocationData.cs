using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Location Data", menuName = "Location Data Scriptable")]
public class LocationData : ScriptableObject
{
    public bool isDataContained;

    public List<Evidence> evidences;
}
