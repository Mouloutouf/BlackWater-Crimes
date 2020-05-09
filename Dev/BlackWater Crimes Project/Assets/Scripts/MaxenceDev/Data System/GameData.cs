using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public enum Locations { Docks, Brothel, Anna_House, New_House, Politician_Office, Cop_Office, Mafia_HideOut, Anna_House_Secret, Brothel_HideOut }

public enum Suspects { Umberto_Moretti, Abigail_White, Richard_Anderson, Bob_Jenkins, None }

public enum Indics { Master_Thommers, Brandon_Bennington, James_Walker, Quentin_Copeland, Thomas_Maxwell, Miss_Marshall, Bob_Jenkins, Arnold_Steele, Standard }

public enum Types { Brands, Crime, Clothing, Documents }

public enum Emotions { Neutral, Fearful, Angry, Proud, Confident }

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
    [Title("General")]

    public bool unlockedData;

    public int index;

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
    [Title("Status")]

    public bool toEdit;

    [Title("Informations")]

    public string name;
    public string date;

    public string text;
}

[Serializable]
public class Intel
{
    public string name;
    
    public float intelAlpha { get; set; }

    [HideInInspector] public bool revealed; // get; set; autoproperty
}

[Serializable]
public class Evidence : Data
{
    public string codeName;
    
    [Title("Intels")]
    
    public bool intelSelf;
    public bool hasIntels;
    [ShowIf("hasIntels")]
    public List<Intel> intels = new List<Intel>();
    
    [Title("Unlock")]
    
    public bool useToUnlock;
    [ShowIf("useToUnlock")]
    public Locations unlockableLocation;

    public bool photographed { get; set; }
    [HideInInspector] public Sprite photo; // get; set; autoproperty
    public bool completedPhotograph { get; set; }

    [Title("Text")]

    public bool hasText;
    [ShowIf("hasText")]
    [Title("Description Text", bold: false, HorizontalLine = false)]
    [HideLabel]
    [MultiLineProperty(5)]
    public string descriptionText;
    
    [HideReferenceObjectPicker]
    [Title("Categories")]
    [HideLabel]
    public ModeCategory modeCategory;

    [Title("Status")]

    public bool seen;
}

[Serializable]
public class Report : Data
{
    [Title("Agent")]

    public Sprite agentSprite;
    public string agentName;

    public Sprite signature;

    [Title("Element")]

    public Modes mode;
    [HideIf("mode", Modes.Type)]
    public Sprite elementSprite;
    public string elementName;
    [ShowIf("mode", Modes.Type)]
    public string elementDetailName;

    [Title("Report Text", bold: false)]
    [HideLabel]
    [MultiLineProperty(15)]
    public string reportText;

    [Title("Unlockable")]

    public bool giveAccess;
    public Locations locationToAccess;

    [Title("Status")]

    public int unlockOrderIndex;
    public bool seen;
}

[Serializable]
public class Location : Data
{
    [Title("Status")]

    public bool known;
    public bool visible;
    public bool accessible;

    public bool completed;

    [Title("Location")]

    public Locations myLocation;
    public string locationName;
    public string locationAdress;

    public Sprite locationArtwork;
    public Sprite locationCroppedImage;

    [Title("Description", bold: false)]
    [HideLabel]
    [MultiLineProperty(5)]
    public string locationDescription;
    
    public int evidenceCollected { get; set; }
}

[Serializable]
public class Answer
{
    [HideLabel]
    [MultiLineProperty(4)]
    public string answer;

    public Emotions emotion;
}

[Serializable]
public class Question : Data
{
    [Title("Question", bold: false)]
    [HideLabel]
    [MultiLineProperty(2)]
    public string question;
    
    public List<Answer> _answers;

    [Title("Report")]

    public bool unlockedByReport;

    [ShowIf("unlockedByReport")]
    public Modes mode;
    [ShowIf("unlockedByReport")]
    public string reportName;
    [ShowIf("mode", Modes.Type)]
    public string otherName;
}

[Serializable]
public class Character : Data
{
    [Title("Information")]

    public string name;
    public Sprite sprite;

    [Title("", horizontalLine: false)]
    public bool isSuspect;
    [ShowIf("isSuspect")]
    public Suspects suspect;

    [Title("Distinctions")]

    public string distinctiveCategory;
    public string distinctiveElement;
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

    public bool vibrations = true;

    public SoundSettings soundSettings;

    [HideInInspector]
    public List<Evidence> evidences = new List<Evidence>();
    public Dictionary<Locations, List<Evidence>> allEvidences = new Dictionary<Locations, List<Evidence>>();

    public List<Note> notes = new List<Note>();

    [HideInInspector]
    public List<Report> reports = new List<Report>();
    public Dictionary<Indics, List<Report>> allReports = new Dictionary<Indics, List<Report>>();

    public int reportsCollected = 0;

    public bool newStuff;

    public List<Location> locations = new List<Location>();

    public Dictionary<Suspects, List<Question>> questions = new Dictionary<Suspects, List<Question>>();

    public Suspects currentSuspect { get; set; }
    public int interrogations { get; set; } = 3;

    public List<Character> characters = new List<Character>();

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

    [ContextMenu("Reset Game Data")]
    public void ResetData()
    {
        // Reset Evidences
        foreach (List<Evidence> evidenceList in allEvidences.Values)
        {
            evidenceList.Clear();
        }

        // Reset Locations
        locations.Clear();

        // Reset Notes
        notes.Clear();

        // Reset Reports
        foreach (List<Report> reportList in allReports.Values)
        {
            foreach (Report report in reportList)
            {
                report.unlockedData = false;
                report.unlockOrderIndex = 0;
                report.seen = false;
            }
        }
        reportsCollected = 0;

        // Reset Questions
        foreach (List<Question> questionList in questions.Values)
        {
            foreach (Question question in questionList)
            {
                if (question.unlockedByReport) question.unlockedData = false;
            }
        }
        interrogations = 3;
    }
}

#region Test

/* Test Language \\
    [Title("Language")]

    public bool useLanguage;
    [ShowIf("useLanguage")]
    [HideReferenceObjectPicker]
    public LocalisedText displayedName;

    //public LanguageText _locationName;
    */

#endregion
