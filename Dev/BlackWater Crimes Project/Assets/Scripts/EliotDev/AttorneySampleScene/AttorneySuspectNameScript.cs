using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttorneySuspectNameScript : MonoBehaviour
{
    [SerializeField] Text inputText;
    [SerializeField] string[] knownNames;
    [SerializeField] GameObject clueShower;
    List<string> knownNamesToCheck = new List<string>();

    void Start() 
    {
        for (int i = 0; i < knownNames.Length; i++)
        {
            knownNamesToCheck.Add(knownNames[i]);
            knownNamesToCheck[i] = knownNamesToCheck[i].Replace(" ", "");
            knownNamesToCheck[i] = knownNamesToCheck[i].ToLower();
        }
    }
    public void CheckName()
    {
        string name = inputText.text;
        name = name.Replace(" ", "");
        name = name.ToLower();
        if(knownNamesToCheck.Contains(name))
        {
            clueShower.SetActive(true);
            int index = knownNamesToCheck.IndexOf(name);
            if(inputText.text != knownNames[index])
            {
                GetComponent<InputField>().text = knownNames[index];
            }
            inputText.color = Color.green;
        }
        else
        {
            clueShower.SetActive(false);
            inputText.color = Color.red;
        }
    }

    public void ResetTextColor()
    {
        if(inputText.color != Color.black)
        {
            inputText.color = Color.black;
        }
    }
}
