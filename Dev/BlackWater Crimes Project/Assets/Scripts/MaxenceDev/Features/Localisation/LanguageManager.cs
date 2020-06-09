using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public GameData gameData;

    public static LanguageManager instance;

    public string languageKey { get { return gameData.gameLanguage == Languages.English ? "US" : "FR"; } }
    public List<TextAsset> files = new List<TextAsset>();

    Dictionary<string, string> sentencesFR = new Dictionary<string, string>();
    Dictionary<string, string> sentencesEN = new Dictionary<string, string>();
    
    void Awake()
    {
        if (instance)
        {
            Destroy(instance);
            return;
        }
        instance = this;
        
        sentencesFR.Clear();
        sentencesEN.Clear();

        foreach (TextAsset file in files)
        {
            CreateDictionary(file);
        }
    }

    void CreateDictionary(TextAsset tsvFile)
    {
        if (tsvFile == null) return;

        string text = tsvFile.text;
        text.Replace("#", "\r\n");
        
        string[] lines = text.Split(new string[]{"\n_"}, StringSplitOptions.None);
        
        string[] keyCells = lines[0].Split(new string[]{"\t"}, StringSplitOptions.None);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cells = lines[i].Split(new string[]{"\t"}, StringSplitOptions.None);

            string mainKey = cells[0];

            sentencesFR.Add(mainKey, cells[1]);
            sentencesEN.Add(mainKey, cells[2]);
        }

        foreach (string _key in sentencesFR.Keys)
        {
            Debug.Log(_key + " : " + sentencesFR[_key]);
        }
        foreach (string _key in sentencesEN.Keys)
        {
            Debug.Log(_key + " : " + sentencesEN[_key]);
        }
    }

    public string Translate(string key)
    {
        string sentence = "";
        
        if (gameData.gameLanguage == Languages.French)
        {
            if (sentencesFR.ContainsKey(key)) sentence = sentencesFR[key];
            else Debug.Log("The Given Key was not present in the Dictionary, either the key did not match or Unity is unable to process basic information.");
        }
        else
        {
            if (sentencesEN.ContainsKey(key)) sentence = sentencesEN[key];
            else Debug.Log("The Given Key was not present in the Dictionary, either the key did not match or Unity is unable to process basic information.");
        }
        
        return sentence;
    }
}