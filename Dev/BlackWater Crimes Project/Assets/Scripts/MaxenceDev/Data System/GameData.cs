using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public enum Locations { Docks, Brothel, Anna_House, New_House, Politician_Office, Cop_Office, Mafia_HideOut, Anna_House_Secret, Brothel_HideOut }

public enum Suspects { Umberto_Moretti, Abigail_White, Richard_Anderson, Bob_Jenkins }

public enum Indics { Master_Thommers, Brandon_Bennington, James_Walker, Quentin_Copeland, Thomas_Maxwell, Miss_Marshall, Bob_Jenkins, Arnold_Steele, Standard }

public enum Types { Brands, Crime, Clothing, Documents }

[Serializable]
public class ModeCategory
{
    public Locations location;
    public Suspects suspect;
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
    public int index;

    public bool unlockedData;

    public Data()
    {
        // set index
    }
}

public enum Languages { English, French }

[Serializable]
public class LocalisedText // Testing
{
    public bool largeText;
    public bool superText;

    private const int yes = 1;
    private int boxSize { get { return largeText ? superText ? 15 : 5 : 1; } }

    [TextArea] // Size of the text box
    public string frenchText;
    [MultiLineProperty(yes)] // Size of the text box
    public string englishText;
}

[Serializable]
public class Note : Data
{
    public string name;
    public string date;

    public string text;
}

[Serializable]
public class Intel
{
    public string name;
    public Sprite image;

    [Range(0, 1)] public float intelAlpha;

    public bool revealed;
}

[Serializable]
public class Evidence : Data
{
    [HideReferenceObjectPicker]
    public LocalisedText displayedName;

    public string codeName;

    public bool intelRevealed; // To Remove

    public bool intelSelf;
    public bool hasIntels;
    [ShowIf("hasIntels")]
    public List<Intel> intels = new List<Intel>();

    [Title("Description Text", bold: false)]
    [HideLabel]
    [MultiLineProperty(5)]
    public string description;

    public bool photographed;
    public Sprite photo;
    public bool completedPhotograph;

    [HideReferenceObjectPicker]
    public ModeCategory modeCategory;

    public bool useToUnlock;
    [ShowIf("useToUnlock")]
    public Locations unlockableLocation;
}

[Serializable]
public class Report : Data
{
    public Sprite agentSprite;
    public string agentName;

    public Sprite elementSprite;
    public string elementName;
    [ShowIf("mode", Modes.Type)]
    public string elementDetailName;

    [Title("Report Text", bold: false)]
    [HideLabel]
    [MultiLineProperty(15)]
    public string reportText;

    public Modes mode;

    public int unlockOrderIndex;
    public bool seen;
}

[Serializable]
public class Location : Data
{
    public bool known;
    public bool visible;
    public bool accessible;

    public bool completed;

    public string locationName;
    public LanguageText _locationName;

    public string locationAdress;

    [Title("Report Text", bold: false)]
    [HideLabel]
    [MultiLineProperty(5)]
    public string locationDescription;
    public LargeLanguageText _locationDescription;

    public int evidenceCollected;

    public Locations myLocation;
}

[Serializable]
public class SoundFloat
{
    public float Volume
    {
        get { return _volume; }
        set { _volume = Mathf.Clamp(value, 0, 1); }
    }
    [SerializeField, Range(0, 1)] private float _volume;
}

[Serializable]
public class SoundSettings
{
    public SoundFloat musicVolume, soundVolume, voiceVolume;
}

// Facile à sauvegarder
[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data Scriptable")]
public class GameData : SerializedScriptableObject
{
    public Dictionary<Type, bool> dataListsContainingState { get; private set; } = new Dictionary<Type, bool>
    {
        {typeof(Evidence), false },
        {typeof(Note), false },
        {typeof(Report), false },
        {typeof(Location), false }
    };

    public Languages gameLanguage = Languages.English;

    public SoundSettings soundSettings;

    [HideInInspector]
    public List<Evidence> evidences = new List<Evidence>();
    public Dictionary<Locations, List<Evidence>> allEvidences = new Dictionary<Locations, List<Evidence>>();

    public List<Note> notes = new List<Note>();

    [HideInInspector]
    public List<Report> reports = new List<Report>();
    public Dictionary<Indics, List<Report>> allReports = new Dictionary<Indics, List<Report>>();

    public int reportsCollected = 0;

    public List<Location> locations = new List<Location>();

    public GameData()
    {
        InitListOfType(evidences);
        InitListOfType(notes);
        InitListOfType(reports);
        InitListOfType(locations);
    }

    public void InitListOfType<T>(List<T> list) where T : Data
    {
        int index = 0;
        foreach (T type in list) { type.index = index; index++; }
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
}
