using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadHunterCheck : Checker
{
    [SerializeField] InputField inputField;
    [SerializeField] Dropdown dropdown;
    
    public override void SendEvent()
    {
        GetCheckedElements();

        if (CheckName()) Send();
    }

    public override void GetCheckedElements()
    {
        foreach (Character character in gameData.characters)
        {
            if (inputField.text == character.name)
            {
                checkedName = character.nameKey;

                checkedImage = character.sprite;
            }
        }
    }

    public override bool Check(Indics _indic, Report _report)
    {
        bool check = true;

        string detailText = dropdown.GetComponentInChildren<Text>().text;

        bool match = false;
        foreach (Character character in gameData.characters)
        {
            foreach (string detail in character.distinctiveElements)
            {
                if (detailText == detail) match = true;
            }
        }
        if (!match) check = false;

        if (_indic != this.indic) check = false;

        if (_report.elementKey != checkedName) check = false;

        if (_report.index == 0) check = false;
        
        return check;
    }

    bool CheckName()
    {
        bool check = false;

        foreach (Character character in gameData.characters)
        {
            if (checkedName == character.nameKey)
            {
                check = true;
            }
        }

        return check;
    }
}
