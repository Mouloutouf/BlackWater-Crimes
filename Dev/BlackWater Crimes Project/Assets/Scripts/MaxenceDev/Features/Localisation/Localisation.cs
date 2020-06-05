using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Localisation : MonoBehaviour
{
    public Text textComponent;
    public string key;

    public void Start()
    {
        RefreshText();
    }

    public virtual void RefreshText()
    {
        if (string.IsNullOrEmpty(key)) return;

        string displayText = LanguageManager.instance.Translate(key);

        textComponent.text = displayText;
    }
}