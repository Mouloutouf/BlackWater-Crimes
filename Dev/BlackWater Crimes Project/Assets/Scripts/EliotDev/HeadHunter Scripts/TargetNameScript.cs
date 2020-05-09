﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetNameScript : MonoBehaviour
{
    [SerializeField] Text inputText;
    [SerializeField] string[] knownNames;
    List<string> knownNamesToCheck = new List<string>();
    [SerializeField] Color baseColor;
    [SerializeField] Color correctColor;
    [SerializeField] Color incorrectColor;
    [SerializeField] GameObject detail;
    private Button validateButton;

    void Start() 
    {
        validateButton = GameObject.Find("Validate Button").GetComponent<Button>();

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
            detail.SetActive(true);
            validateButton.interactable = true;
            validateButton.gameObject.GetComponentInChildren<Text>().text = "Validate";
            int index = knownNamesToCheck.IndexOf(name);
            if(inputText.text != knownNames[index])
            {
                GetComponent<InputField>().text = knownNames[index];
            }
            inputText.color = correctColor;
        }
        else
        {
            detail.SetActive(false);
            validateButton.interactable = false;
            validateButton.gameObject.GetComponentInChildren<Text>().text = "Missing elements";
            inputText.color = incorrectColor;
        }
    }

    public void ResetTextColor()
    {
        if(inputText.color != baseColor)
        {
            inputText.color = baseColor;
        }
    }
}
