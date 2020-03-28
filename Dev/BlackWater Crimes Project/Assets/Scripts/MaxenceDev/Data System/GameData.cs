using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public enum Locations { Docks, Whorehouse, MayorHouse }

public enum Characters { Jack, Anna, Oliver }

public enum Types { Organic, Ballistic, Other }

[Serializable]
public class ModeCategory
{
    public Locations crimeScene;
    public Characters suspect;
    public Types type;
}

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
    public int index;

    public Data()
    {
        // set index
    }
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

    public ModeCategory modeCategory;
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
    [Title("Report Text", bold: false)]
    [HideLabel]
    [MultiLineProperty(5)]
    public string locationDescription;
    public int evidenceCollected;
}

// Facile à sauvegarder
[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data Scriptable")]
public class GameData : ScriptableObject
{
    public Dictionary<string, bool> dataListsContainingState;

    public List<Evidence> evidences = new List<Evidence>();
    
    public List<Note> notes = new List<Note>();

    public List<Report> reports = new List<Report>();

    public List<Location> locations = new List<Location>();

    public GameData()
    {
        InitListOfType(evidences);
        InitListOfType(notes);
        InitListOfType(reports);
        InitListOfType(locations);
    }

    private Dictionary<Type, string> allTypes = new Dictionary<Type, string>
    {
        {typeof(Evidence), "evidence"},
        {typeof(Note), "note" },
        {typeof(Report), "report" },
        {typeof(Location), "location" }
    };

    public List<T> GetListOfType<T>(T _type) where T : Data
    {
        switch (allTypes[typeof(T)])
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

    public void InitListOfType<T>(List<T> list) where T : Data
    {
        int index = 0;
        foreach (T type in list) { type.index = index; index++; }
    }
}
