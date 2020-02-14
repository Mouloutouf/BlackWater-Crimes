using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data Scriptable")]
public class PlayerData : ScriptableObject
{
    public List<Evidence> evidences;

    public void AddToList(Evidence evidence)
    {
        evidences.Add(evidence);
    }
}
