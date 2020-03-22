﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public enum DataTypes
{
    Evidence,
    Note,
    Report,
    Location
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

[Serializable]
public class Report : Data
{
    public Sprite agentSprite;
    public string agentName;

    public Sprite elementSprite;
    public string elementName;

    [Title("Report Text", bold: false)]
    [HideLabel]
    [MultiLineProperty(15)]
    public string reportText;
}

[Serializable]
public class Location : Data
{
    public bool known;
    public bool visible;
    public bool accessible;

    public bool completed;

    public string locationName;
}

// Facile à sauvegarder
[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data Scriptable")]
public class GameData : ScriptableObject
{
    public bool isDataContained;

    public List<Evidence> evidences;
    
    public List<Note> notes;

    public List<Report> reports;

    public List<Location> locations;

    private Dictionary<Type, string> allTypes = new Dictionary<Type, string>
    {
        {typeof(Evidence), "evidence"},
        {typeof(Note), "note" },
        {typeof(Report), "report" },
        {typeof(Location), "location" }
    };

    public List<T> GetListOfType<T>(T _type) where T : Data
    {
        switch (allTypes[_type.GetType()])
        {
            case "evidence":
                return evidences as List<T>;
            case "note":
                return notes as List<T>;
            case "report":
                return reports as List<T>;
            case "location":
                return locations as List<T>;
            default:
                return null;
        }
    }
}
