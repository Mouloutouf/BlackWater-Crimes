using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "TestDataTitouan", menuName = "Test Titouan Data Scriptable")]
public class StockageVariables : SerializedScriptableObject
{
    public Dictionary<Locations, List<Evidence>> evidences;

    public Dictionary<Suspects, List<Question>> questions;
    public Dictionary<Indics, (List<Report>, List<Report>)> megaReports = new Dictionary<Indics, (List<Report>, List<Report>)>();
    public List<Character> characters;
    public Dictionary<Indics, Indic> indics;
    public List<Location> locations;

    public List<LanguageTextComponent> languageTexts;
}
