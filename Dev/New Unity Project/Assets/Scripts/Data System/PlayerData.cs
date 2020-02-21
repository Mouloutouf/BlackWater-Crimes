using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Data
{
    public string code;
}

[Serializable]
public class Note : Data
{
    public string name;
    public string date;

    public string text;
}

[Serializable]
public class Evidence : Data
{
    public string name;

    public bool taken;

    public Sprite render2D;
    public Mesh highRender3D;
    public Mesh lowRender3D;

    public bool hasIntel;

    public Sprite intel;

    public bool intelRevealed;

    public string description;
}

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data Scriptable")]
public class PlayerData : ScriptableObject
{
    public bool isDataContained;

    public List<Evidence> evidences;

    public List<Note> notes;
}
