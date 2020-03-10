using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DataTypes
{
    Evidence,
    Note
}

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
    
    public bool hasIntel;
    public Sprite intel;
    public bool intelRevealed;

    public string description;

    public bool photographed;
    public Sprite photo;
}

// Facile à sauvegarder
[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data Scriptable")]
public class GameData : ScriptableObject
{
    public bool isDataContained;

    public List<Evidence> evidences;
    
    public List<Note> notes;

    private Dictionary<Type, string> allTypes = new Dictionary<Type, string>
    {
        {typeof(Evidence), "evidence"},
        {typeof(Note), "note" }
    };

    public List<T> GetListOfType<T>(T _type) where T : Data
    {
        switch (allTypes[_type.GetType()])
        {
            case "evidence":
                return evidences as List<T>;
            case "note":
                return notes as List<T>;
            default:
                return null;
        }
    }
}
