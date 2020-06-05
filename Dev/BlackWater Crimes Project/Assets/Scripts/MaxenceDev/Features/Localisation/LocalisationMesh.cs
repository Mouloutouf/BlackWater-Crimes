using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalisationMesh : Localisation
{
    public TextMesh textMeshComponent;

    public override void RefreshText()
    {
        if (string.IsNullOrEmpty(key)) return;

        string displayText = LanguageManager.instance.Translate(key);

        textMeshComponent.text = displayText;
    }
}
