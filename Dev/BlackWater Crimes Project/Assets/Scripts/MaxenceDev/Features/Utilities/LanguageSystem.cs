using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[Serializable]
public class LanguageText
{
    public string frenchText;
    public string englishText;
}

[Serializable]
public class LargeLanguageText
{
    [LabelWidth(105)] [MultiLineProperty(3)]
    public string frenchText;
    [LabelWidth(105)] [MultiLineProperty(3)]
    public string englishText;
}

[Serializable]
public class LanguageTextComponent : LanguageText
{
    public Text textComponent;
}

public class LanguageSystem : MonoBehaviour
{
    public GameData gameData;

    public List<LanguageTextComponent> languageTexts = new List<LanguageTextComponent>();

    void Start()
    {
        foreach (LanguageTextComponent lText in languageTexts)
        {
            lText.textComponent.text = (gameData.gameLanguage == Languages.English) ? lText.englishText : lText.frenchText;
        }
    }
}
