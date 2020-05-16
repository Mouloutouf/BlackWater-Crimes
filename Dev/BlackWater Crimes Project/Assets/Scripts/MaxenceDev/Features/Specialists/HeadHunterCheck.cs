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
                checkedName = character.name;

                checkedImage = character.sprite;
            }
        }
    }
    
    bool CheckName()
    {
        bool check = false;

        foreach (Character character in gameData.characters)
        {
            if (checkedName == character.name)
            {
                string detailText = dropdown.GetComponentInChildren<Text>().text;

                foreach (string detail in character.distinctiveElements)
                {
                    if (detailText == detail) check = true;
                }
            }
        }

        return check;
    }
}
