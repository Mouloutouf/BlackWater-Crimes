using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

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
    public string intelKey; // useless for now
    
    public float intelAlpha { get; set; }

    [HideInInspector] public bool revealed; // get; set; autoproperty
}

[Serializable]
public class Evidence : Data
{
    public string codeName;
    
    public string nameKey;
    
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
    [HideInInspector] public Sprite photo;
    public string photoPath;
    public bool completedPhotograph { get; set; }

    [Title("Text")]

    public bool hasText;
    [ShowIf("hasText")]
    [Title("Description Text", bold: false, HorizontalLine = false)]
    [HideLabel]
    [MultiLineProperty(5)]
    public string descriptionText;

    public string textKey;
    
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

    public string elementKey;
    public string reportKey;

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

    public string nameKey;
    public string descriptionKey;
    public string addressKey;
}

[Serializable]
public class Answer
{
    [HideLabel]
    [MultiLineProperty(4)]
    public string answer;
    public string answerKey;

    public Emotions emotion;
}

[Serializable]
public class Question : Data
{
    [Title("Question", bold: false)]
    [HideLabel]
    [MultiLineProperty(2)]
    public string question;
    public string questionKey;
    
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
    [ShowIf("isSuspect")]
    public string jobKey;
    [ShowIf("isSuspect")]
    public string introPhraseKey;

    [Title("Distinctions")]
    
    public List<string> distinctiveElements;
}

[Serializable]
public class Indic
{
    [Title("Infos")]

    public string name;
    public string jobKey;

    public Sprite image;

    public bool quickCallAvailable;
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
    public Dictionary<Locations, List<Evidence>> evidences = new Dictionary<Locations, List<Evidence>>();

    [Title("Notes")]
    public List<Note> notes = new List<Note>();
    
    [Title("Reports")]
    public Dictionary<Indics, (List<Report>, List<Report>)> reports = new Dictionary<Indics, (List<Report>, List<Report>)>();
    [HideInInspector] public Dictionary<Indics, List<Report>> allReports = new Dictionary<Indics, List<Report>>();
    public int reportsCollected = 0;

    public bool newStuff;

    [Title("Locations")]
    public List<Location> locations = new List<Location>();
    
    [Title("Questions")]
    public Dictionary<Suspects, List<Question>> questions = new Dictionary<Suspects, List<Question>>();
    public Suspects currentSuspect;
    public int interrogations = 3;

    [Title("Characters")]
    public List<Character> characters = new List<Character>();
    
    [Title("Indics")]
    public Dictionary<Indics, Indic> indics = new Dictionary<Indics, Indic>();
    public Indics currentIndic = Indics.Standard;
    
    [HideInInspector] public bool firstTimeInGame = true;
    public bool firstTimeInTuto = true; // Tutorial Docks

    [Title("DATA SAVE")]

    public SaveSystem saveSystem;

    // Data Methods \\
    
    public List<SaveData> savedData = new List<SaveData>();
    
    [ContextMenu("Test Object Conversion")]
    public void Test()
    {
        int myInt = 6;
        
        object myObject = myInt;
        
        Debug.Log(myObject);

        myInt++;

        Debug.Log(myObject);

        Type myType = myObject.GetType();
        object otherObject = new object();
        Debug.Log(myType);
    }
    
    private T SetData<T>(Action _action, T _variable, string _name)
    {
        if (_action == Action.Save) _variable = SaveDataInList(_variable, _name);
        else if (_action == Action.Load) _variable = LoadDataFromList(_variable, _name);

        return _variable;
    }

    private T SaveDataInList<T>(T data, string name)
    {
        savedData.Add(
            new SaveData
            {
                dataVariable = data, dataName = name, dataType = data.GetType()
            }
        );

        //Debug.Log(name + " " + data + " was added to saved data list.");

        return data;
    }
    
    private T LoadDataFromList<T>(T data, string name)
    {
        foreach (SaveData saveData in savedData)
        {
            if (saveData.dataName == name)
            {
                data = (T) saveData.dataVariable;
            }
        }

        Debug.Log(name + " " + data + " was loaded from the saved data list");

        return data;
    }

    public void ManageData(Action action)
    {
        if (action == Action.Save)
        {
            savedData.Clear();

            ManageSavedData(Action.Save);

            saveSystem.SaveDataInPrefs(this);
        }

        else if (action  == Action.Load)
        {
            savedData.Clear();

            ManageSavedData(Action.Save);

            saveSystem.LoadDataFromPrefs(this);

            ManageSavedData(Action.Load);
        }
    }

    public void ManageSavedData(Action action)
    {
        firstTimeInGame = SetData(action, firstTimeInGame, nameof(firstTimeInGame));
        firstTimeInTuto = SetData(action, firstTimeInTuto, nameof(firstTimeInTuto));

        gameLanguage = SetData(action, gameLanguage, nameof(gameLanguage));

        vibrations = SetData(action, vibrations, nameof(vibrations));

        string soundSettingsName = nameof(soundSettings);

        soundSettings.musicVolume.Volume = SetData(action, soundSettings.musicVolume.Volume, soundSettingsName + "_" + nameof(soundSettings.musicVolume));
        soundSettings.soundVolume.Volume = SetData(action, soundSettings.soundVolume.Volume, soundSettingsName + "_" + nameof(soundSettings.soundVolume));
        soundSettings.voiceVolume.Volume = SetData(action, soundSettings.voiceVolume.Volume, soundSettingsName + "_" + nameof(soundSettings.voiceVolume));

        // Add Evidences
        foreach (Locations location in evidences.Keys)
        {
            string listName = location.ToString();

            foreach (Evidence evidence in evidences[location])
            {
                string evidenceName = listName + "_" + evidence.codeName;

                evidence.unlockedData = SetData(action, evidence.unlockedData, evidenceName + "_" + nameof(evidence.unlockedData));
                
                if (evidence.hasIntels)
                {
                    foreach (Intel intel in evidence.intels)
                    {
                        intel.revealed = SetData(action, intel.revealed, evidenceName + "_" + intel.name + "_" + nameof(intel.revealed));
                        intel.intelAlpha = SetData(action, intel.intelAlpha, evidenceName + "_" + intel.name + "_" + nameof(intel.intelAlpha));
                    }
                }

                evidence.photoPath = SetData(action, evidence.photoPath, evidenceName + "_" + nameof(evidence.photoPath));
                if (action == Action.Load) evidence.photo = EvidenceInteraction.CreateSprite(evidence.photoPath);

                evidence.photographed = SetData(action, evidence.photographed, evidenceName + "_" + nameof(evidence.photographed));
                evidence.seen = SetData(action, evidence.seen, evidenceName + "_" + nameof(evidence.seen));
            }
        }

        // Add Locations
        foreach (Location location in locations)
        {
            string locationName = location.locationName;

            location.known = SetData(action, location.known, locationName + "_" + nameof(location.known));
            location.visible = SetData(action, location.visible, locationName + "_" + nameof(location.visible));
            location.accessible = SetData(action, location.accessible, locationName + "_" + nameof(location.accessible));
        }
        
        // Add Notes
        foreach (Note note in notes)
        {
            string noteName = note.name;

            note.text = SetData(action, note.text, noteName + "_" + nameof(note.text));
            note.date = SetData(action, note.date, noteName + "_" + nameof(note.date));
        }

        // Add Reports
        foreach (Indics indic in reports.Keys)
        {
            string listName = indic.ToString();

            foreach (Report report in reports[indic].Item1)
            {
                string reportName = listName + "_" + report.elementName;
                if (report.mode == Modes.Evidence) reportName += ("_" + report.elementDetailName);

                report.unlockedData = SetData(action, report.unlockedData, reportName + "_" + nameof(report.unlockedData));
                report.unlockOrderIndex = SetData(action, report.unlockOrderIndex, reportName + "_" + nameof(report.unlockOrderIndex));
                report.seen = SetData(action, report.seen, reportName + "_" + nameof(report.seen));
            }
        }
        reportsCollected = SetData(action, reportsCollected, nameof(reportsCollected));
        
        // Add Questions
        foreach (Suspects suspect in questions.Keys)
        {
            string listName = suspect.ToString();

            foreach (Question question in questions[suspect])
            {
                string questionName = listName + "_question n°" + question.index.ToString();

                if (question.unlockedByReport) question.unlockedData = SetData(action, question.unlockedData, questionName + "_" + nameof(question.unlockedData));
            }
        }
        interrogations = SetData(action, interrogations, nameof(interrogations));

        foreach (Indic indic in indics.Values)
        {
            indic.quickCallAvailable = SetData(action, indic.quickCallAvailable, indic.name + "_" + nameof(indic.quickCallAvailable));
        }

        firstTimeInTuto = SetData(action, firstTimeInTuto, nameof(firstTimeInTuto));
    }

    [ContextMenu("Reset To Build State")]
    public void ResetPrefs()
    {
        savedData.Clear();

        PlayerPrefs.DeleteAll();

        ResetData();

        PlayerPrefs.SetInt(nameof(firstTimeInGame), 1);
    }

    [ContextMenu("Reset Game Data")]
    public void ResetData()
    {
        ResetParameters();
        
        // Reset Evidences
        foreach (Locations location in evidences.Keys)
        {
            foreach (Evidence evidence in evidences[location])
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
                
                evidence.photoPath = "Assets/Graphs/Saved_Photos/" + evidence.codeName.Replace(" ", "") + ".png";

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
        foreach (Indics indic in reports.Keys)
        {
            foreach (Report _report in reports[indic].Item1)
            {
                _report.unlockedData = false;
                _report.unlockOrderIndex = 0;
                _report.seen = false;
            }
        }
        reportsCollected = 0;

        // Reset Questions
        foreach (Suspects suspect in questions.Keys)
        {
            foreach (Question question in questions[suspect])
            {
                if (question.unlockedByReport) question.unlockedData = false;
            }
        }
        interrogations = 3;

        foreach (Indic indic in indics.Values)
        {
            indic.quickCallAvailable = false;
        }

        newStuff = false;
    }

    public void ResetParameters()
    {
        gameLanguage = Languages.English;

        vibrations = true;

        soundSettings.musicVolume.Volume = 0.5f;
        soundSettings.soundVolume.Volume = 0.5f;
        soundSettings.voiceVolume.Volume = 0.5f;
    }

    #region Test

    /*
    [HideInInspector] public Sprite photo { get { return EvidenceInteraction.CreateSprite(photoPath); } set => photo = value; }
    public string photoPath
    {
        get
        {
            string name = codeName.Replace(" ", "");

            bool isWindowsBuild = Application.platform == RuntimePlatform.WindowsPlayer;
            bool isAndroidBuild = Application.platform == RuntimePlatform.Android;
            bool isUnityEditor = Application.platform == RuntimePlatform.WindowsEditor || (Application.platform == RuntimePlatform.Android && EditorApplication.isPlaying);

            string windowsBuildPath = Application.persistentDataPath + name + ".png";
            string androidBuildPath = Application.persistentDataPath + "/" + name + ".png";
            string unityEditorPath = "Assets/Graphs/Sprites/Screenshots/" + name + ".png";

            return (isUnityEditor ? unityEditorPath : isWindowsBuild ? windowsBuildPath : isAndroidBuild ? androidBuildPath : "");
        }
    }
    */
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
                return evidences as List<T>;
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