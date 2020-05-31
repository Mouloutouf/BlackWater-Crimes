using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "TestDataTitouan", menuName = "Player Data Scriptable")]
public class StockageVariables : SerializedScriptableObject
{
    public Dictionary<Suspects, List<Question>> questions;
    public Dictionary<Indics, (List<Report>, List<Report>)> megaReports = new Dictionary<Indics, (List<Report>, List<Report>)>();
    public Dictionary<Indics, Indic> indics;

    public List<LanguageTextComponent> languageTexts;
}
