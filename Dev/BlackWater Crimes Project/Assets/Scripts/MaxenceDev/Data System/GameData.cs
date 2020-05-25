using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public enum Locations { Docks, Brothel, Anna_House, Abigail_House, Politician_Office, Cop_Office, Mafia_HideOut, Anna_House_Secret, Brothel_HideOut }

public enum Suspects { Umberto_Moretti, Abigail_White, Richard_Anderson, Bob_Jenkins, None }

public enum Indics { Master_Thommers, Brandon_Bennington, James_Walker, Quentin_Copeland, Thomas_Maxwell, Miss_Marshall, Arnold_Steele, Standard }

public enum Emotions { Neutral, Fearful, Angry, Proud, Confident }

[Serializable]
public class ModeCategory
{
    public Locations location;
    public Suspects suspect;
}

public enum Modes
{
    Location,
    Suspect,
    Evidence
}

public class Data
{
    [Title("General")]

    public bool unlockedData;

    public int index;
}

public enum Languages { English, French }

#region Test Class
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
#endregion

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
    public bool failed;

    [Title("Agent")]

    public Sprite agentSprite;
    public string agentName;

    public Sprite signature;

    [Title("Element")]

    public Modes mode;
    [HideIf("mode", Modes.Evidence)]
    public Sprite elementSprite;
    public string elementName;
    [ShowIf("mode", Modes.Evidence)]
    public string elementDetailName;

    [Title("Report Text", bold: false)]
    [HideLabel]
    [MultiLineProperty(15)]
    public string reportText;

    [Title("Unlockable")]

    public bool giveAccess;
    [ShowIf("giveAccess")]
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
    [ShowIf("mode", Modes.Evidence)]
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
    
    public List<string> distinctiveElements;
}

[Serializable]
public class Indic
{
    [Title("Infos")]

    public string name;
    public string job;

    public Sprite image;
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

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data Scriptable")]
public class GameData : SerializedScriptableObject
{
    // Data Properties \\

    [Title("Game Settings")]

    public Languages gameLanguage = Languages.English;

    public bool vibrations = true;

    public SoundSettings soundSettings;

    [Title("DATA")]
    
    [Title("Evidences")]
    public Dictionary<Locations, List<Evidence>> allEvidences = new Dictionary<Locations, List<Evidence>>();

    [Title("Notes")]
    public List<Note> notes = new List<Note>();
    
    [Title("Reports")]
    public Dictionary<Indics, (List<Report>, List<Report>)> megaReports = new Dictionary<Indics, (List<Report>, List<Report>)>();
    [HideInInspector] public Dictionary<Indics, List<Report>> allReports = new Dictionary<Indics, List<Report>>();
    public int reportsCollected = 0;

    public bool newStuff;

    [Title("Locations")]
    public List<Location> locations = new List<Location>();
    
    [HideInInspector] public bool firstTimeInDocks = true;

    [Title("Questions")]
    public Dictionary<Suspects, List<Question>> questions = new Dictionary<Suspects, List<Question>>();
    public Suspects currentSuspect;
    public int interrogations { get; set; } = 3;

    [Title("Characters")]
    public List<Character> characters = new List<Character>();
    
    [Title("Indics")]
    public Dictionary<Indics, Indic> indics = new Dictionary<Indics, Indic>();
    public Indics currentIndic = Indics.Standard;

    [Title("DATA SAVE")]

    public SaveSystem saveSystem;

    // Data Methods \\
    
    public List<SaveData> savedData { get; set; } = new List<SaveData>();
    
    [ContextMenu("Test Object Conversion")]
    public void Test()
    {
        int myInt = 6;
        
        object myObject = myInt;
        
        Debug.Log(myObject);

        myInt++;

        Debug.Log(myObject);

        Type myType = myObject.GetType();
        Debug.Log(myType);
    }
    
    private void SetData<T>(Action _action, T _variable, string _name)
    {
        if (_action == Action.Save) SaveDataInList(_variable, _name);
        else if (_action == Action.Load) LoadDataFromList(_variable, _name);
    }

    private void SaveDataInList(object variable, string name)
    {
        savedData.Add(
            new SaveData
            {
                dataVariable = variable, dataName = name
            }
        );
    }
    
    private void LoadDataFromList<T>(T data, string name)
    {
        foreach (SaveData saveData in savedData)
        {
            if (saveData.dataName == name)
            {
                data = (T) saveData.dataVariable;
            }
        }
    }

    public void ManageData(Action action)
    {
        if (action == Action.Save) savedData.Clear();

        else if (action == Action.Load) saveSystem.LoadDataList(this);

        SetData(action, gameLanguage, nameof(gameLanguage));

        SetData(action, vibrations, nameof(vibrations));

        string soundSettingsName = nameof(soundSettings);

        SetData(action, soundSettings.musicVolume, soundSettingsName + "_" + nameof(soundSettings.musicVolume));
        SetData(action, soundSettings.soundVolume, soundSettingsName + "_" + nameof(soundSettings.soundVolume));
        SetData(action, soundSettings.voiceVolume, soundSettingsName + "_" + nameof(soundSettings.voiceVolume));

        // Add Evidences
        foreach (List<Evidence> evidenceList in allEvidences.Values)
        {
            string listName = evidenceList[0].modeCategory.location.ToString();

            foreach (Evidence evidence in evidenceList)
            {
                string evidenceName = listName + "_" + evidence.codeName;

                SetData(action, evidence.unlockedData, evidenceName + "_" + nameof(evidence.unlockedData));
                
                if (evidence.hasIntels)
                {
                    foreach (Intel intel in evidence.intels)
                    {
                        SetData(action, intel.revealed, evidenceName + "_" + intel.name + "_" + nameof(intel.revealed));
                        SetData(action, intel.intelAlpha, evidenceName + "_" + intel.name + "_" + nameof(intel.intelAlpha));
                    }
                }

                SetData(action, evidence.photographed, evidenceName + "_" + nameof(evidence.photographed));
                SetData(action, evidence.seen, evidenceName + "_" + nameof(evidence.seen));
            }
        }

        // Add Locations
        foreach (Location location in locations)
        {
            string locationName = location.locationName;

            SetData(action, location.known, locationName + "_" + nameof(location.known));
            SetData(action, location.visible, locationName + "_" + nameof(location.visible));
            SetData(action, location.accessible, locationName + "_" + nameof(location.accessible));
        }
        
        // Add Notes
        foreach (Note note in notes)
        {
            string noteName = note.name;

            SetData(action, note.text, noteName + "_" + nameof(note.text));
            SetData(action, note.date, noteName + "_" + nameof(note.date));
        }

        // Add Reports
        foreach ((List<Report>, List<Report>) bigList in megaReports.Values)
        {
            string ListName = bigList.Item1[0].agentName;

            foreach (Report report in bigList.Item1)
            {
                string reportName = report.elementName;
                if (report.mode == Modes.Evidence) reportName += ("_" + report.elementDetailName);

                SetData(action, report.unlockedData, reportName + "_" + nameof(report.unlockedData));
                SetData(action, report.unlockOrderIndex, reportName + "_" + nameof(report.unlockOrderIndex));
                SetData(action, report.seen, reportName + "_" + nameof(report.seen));
            }
        }
        SetData(action, reportsCollected, nameof(reportsCollected));
        
        // Add Questions
        foreach (Suspects suspect in questions.Keys)
        {
            string listName = suspect.ToString();

            foreach (Question question in questions[suspect])
            {
                string questionName = listName + question.index.ToString();

                if (question.unlockedByReport) SetData(action, question.unlockedData, questionName + "_" + nameof(question.unlockedData));
            }
        }
        SetData(action, interrogations, nameof(interrogations));

        if (action == Action.Save) saveSystem.SaveDataList(this);
    }

    [ContextMenu("Reset Game Data")]
    public void ResetData()
    {
        // Reset Evidences
        foreach (List<Evidence> evidenceList in allEvidences.Values)
        {
            foreach (Evidence evidence in evidenceList)
            {
                evidence.unlockedData = false;

                if (evidence.hasIntels)
                {
                    foreach (Intel intel in evidence.intels)
                    {
                        intel.revealed = false;
                        intel.intelAlpha = 0.0f;
                    }
                }

                evidence.photographed = false;

                evidence.seen = false;
            }
        }

        // Reset Locations
        foreach (Location location in locations)
        {
            if (location.myLocation != Locations.Docks)
            {
                location.known = false;
                location.visible = false;
                location.accessible = false;
            }
        }
        
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

        foreach ((List<Report>, List<Report>) bigList in megaReports.Values)
        {
            foreach (Report _report in bigList.Item1)
            {
                _report.unlockedData = false;
                _report.unlockOrderIndex = 0;
                _report.seen = false;
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

        newStuff = false;

        firstTimeInDocks = true;

        savedData.Clear();

        PlayerPrefs.DeleteAll();
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

    public Dictionary<Type, bool> dataListsContainingState { get; set; } = new Dictionary<Type, bool>
    {
        {typeof(Evidence), false },
        {typeof(Note), false },
        {typeof(Report), false },
        {typeof(Location), false }
    };

    void InitListOfType<T>(List<T> list) where T : Data
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
                return allEvidences as List<T>;
            case "note":
                return notes as List<T>;
            case "location":
                return locations as List<T>;
            default:
                return null;
        }
    }

    #endregion
} 