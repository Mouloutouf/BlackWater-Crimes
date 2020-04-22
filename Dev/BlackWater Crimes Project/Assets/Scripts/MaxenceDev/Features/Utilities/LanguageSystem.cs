using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Reflection;

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

[Serializable]
public class LargeLanguageTextComponent : LargeLanguageText
{
    public Text textComponent;
}

public class LanguageSystem : MonoBehaviour
{
    public GameData gameData;

    public List<LanguageTextComponent> languageTexts = new List<LanguageTextComponent>();
    public List<LargeLanguageTextComponent> largeLanguageTexts = new List<LargeLanguageTextComponent>();

    public List<GameObject> languageUsingObjects = new List<GameObject>();

    void Start()
    {
        SetLanguage();
    }

    public void SetLanguage()
    {
        foreach (LanguageTextComponent lText in languageTexts)
        {
            lText.textComponent.text = (gameData.gameLanguage == Languages.English) ? lText.englishText : lText.frenchText;
        }

        foreach (LargeLanguageTextComponent lLText in largeLanguageTexts)
        {
            lLText.textComponent.text = (gameData.gameLanguage == Languages.English) ? lLText.englishText : lLText.frenchText;
        }

        foreach (GameObject gObj in languageUsingObjects)
        {
            MonoBehaviour script = gObj.GetComponent<MonoBehaviour>();

            FieldInfo[] fields = script.GetType().GetFields();
            string str = "";
            foreach (FieldInfo f in fields)
            {
                str += f.Name + " = " + f.GetValue(script) + "\r\n";
            }

            Debug.Log(str);
        }
    }
}
