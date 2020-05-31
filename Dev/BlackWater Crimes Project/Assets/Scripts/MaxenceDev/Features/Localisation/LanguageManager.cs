using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public GameData gameData;

    public static LanguageManager instance;

    public string languageKey { get { return gameData.gameLanguage == Languages.English ? "US" : "FR"; } }
    public TextAsset testFile;

    Dictionary<string, string> allSentences = new Dictionary<string, string>();
    
    void Awake()
    {
        if (instance)
        {
            Destroy(instance);
            return;
        }
        instance = this;
        
        CreateDictionary(testFile);
    }

    void CreateDictionary(TextAsset tsvFile)
    {
        allSentences.Clear();

        if (tsvFile == null) return;

        string text = tsvFile.text;
        
        string[] lines = text.Split(new string[]{"\n"}, StringSplitOptions.None);
        
        string[] keyCells = lines[0].Split(new string[]{"\t"}, StringSplitOptions.None);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(new string[]{"\t"}, StringSplitOptions.None);

            string mainKey = cells[0];
            for (int j = 1; j < cells.Length; j++)
            {
                allSentences.Add(mainKey + "_" + keyCells[j], cells[j]);
            }
        }
    }

    public string Translate(string key)
    {
        string fullKey = key + "_" + languageKey;
        
        string sentence = "";

        foreach (string _key in allSentences.Keys) Debug.Log(_key); // Debug Line
        Debug.Log(fullKey);
        if (allSentences.ContainsKey(fullKey)) sentence = allSentences[fullKey];
        else Debug.Log("The Given Key was not present in the Dictionary, either the key did not match or Unity is unable to process basic information.");

        Debug.Log(allSentences.Count);

        return sentence;
    }
}