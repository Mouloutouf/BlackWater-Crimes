using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InstantiateCharacterElements : InstantiateElements<Character>
{
    protected override List<List<Character>> GetAllElements()
    {
        List<List<Character>> mainList = new List<List<Character>>();

        List<Character> allCharacters = new List<Character>();

        foreach (Character character in gameData.characters)
        {
            allCharacters.Add(character);
        }

        mainList.Add(allCharacters);

        return mainList;
    }

    protected override bool Check(Character data)
    {
        bool check = data.unlockedData; // Rajoutez variable knownCharacter au Game Data

        return check;
    }

    void Start()
    {
        Initialize();
    }

    protected override string GetDataName(Character data)
    {
        return data.name;
    }
}
