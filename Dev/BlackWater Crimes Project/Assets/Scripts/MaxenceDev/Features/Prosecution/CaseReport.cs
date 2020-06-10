using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class CaseReport : SerializedMonoBehaviour
{
    public GameData gameData;
    private Character chosen;

    public Image accusedImage;
    public float factor;
    public Localisation accusedName;
    public Localisation accusedJury;

    public Dictionary<FileCategory, Localisation> paragraphs = new Dictionary<FileCategory, Localisation>();
    public Dictionary<FileCategory, (string, string)> texts = new Dictionary<FileCategory, (string, string)>();

    public Localisation endParagraph;

    private int maxAmount = 3;
    private int minAmount = 0;

    void Start()
    {
        FinishTheCase();
    }

    void FinishTheCase()
    {
        foreach (Character character in gameData.characters)
        {
            if (character.isSuspect && character.suspect == gameData.accused.suspect)
            {
                chosen = character;
            }
        }

        SetAccused();

        int success = 0;

        foreach (Incriminate incriminate in gameData.accused.incriminates)
        {
            bool match = false;

            foreach (Incriminate _incriminate in chosen.incriminates)
            {
                if (incriminate.category == _incriminate.category && incriminate.elementKey == _incriminate.elementKey && incriminate.elementType == _incriminate.elementType)
                {
                    match = true;

                    success++;
                }
            }

            if (match) paragraphs[incriminate.category].key = texts[incriminate.category].Item1; // Good sentence

            else paragraphs[incriminate.category].key = texts[incriminate.category].Item2; // Bad sentence
        }

        endParagraph.key = success == maxAmount ? chosen.prosecutionKeys[2] : success == minAmount ? chosen.prosecutionKeys[0] : chosen.prosecutionKeys[1];
        
        SetParagraphs();
    }

    void SetAccused()
    {
        accusedImage.sprite = chosen.accusedSprite;
        accusedImage.SetNativeSize();
        RectTransform rect = accusedImage.gameObject.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.rect.width / factor, rect.rect.height / factor);

        accusedName.key = chosen.nameKey;
        accusedName.RefreshText();

        accusedJury.key = chosen.juryKey;
        accusedJury.RefreshText();
    }

    void SetParagraphs()
    {
        endParagraph.RefreshText();

        foreach (Localisation localisation in paragraphs.Values)
        {
            localisation.RefreshText();
        }
    }
}
