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
        Send();
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

    public override bool Check(Indics _indic, Report _report)
    {
        bool check = true;

        if (_indic != this.indic) check = false;

        if (_report.elementName != checkedName) check = false;

        if (_report.index == 0) check = false;

        foreach (Character character in gameData.characters)
        {
            if (inputField.text == character.name)
            {
                string detailText = dropdown.GetComponentInChildren<Text>().text;

                foreach (string detail in character.distinctiveElements)
                {
                    if (detailText == detail) check = true;

                    else check = false;
                }
            }

            else check = false;
        }

        return check;
    }
}
