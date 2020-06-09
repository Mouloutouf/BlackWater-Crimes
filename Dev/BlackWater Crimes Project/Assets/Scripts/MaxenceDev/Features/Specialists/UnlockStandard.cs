using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockStandard : MonoBehaviour
{
    public GameData gameData;

    public Text inputText;
    public Localisation secretKey;
    
    public List<string> knownNames = new List<string>();
    private List<string> knownNamesToCheck = new List<string>();

    [SerializeField] Color baseColor;
    [SerializeField] Color correctColor;
    [SerializeField] Color incorrectColor;

    public Button validateButton;
    public string secretTextKey;

    public string validateKey;
    public string missingKey;

    void Start() 
    {
        for (int i = 0; i < knownNames.Count; i++)
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

        if (knownNamesToCheck.Contains(name))
        {
            validateButton.interactable = true;
            validateButton.gameObject.GetComponentInChildren<Localisation>().key = validateKey;
            validateButton.gameObject.GetComponentInChildren<Localisation>().RefreshText();

            int index = knownNamesToCheck.IndexOf(name);

            if (inputText.text != knownNames[index])
            {
                GetComponent<InputField>().text = knownNames[index];
            }

            inputText.color = correctColor;
        }
        else
        {
            validateButton.interactable = false;
            validateButton.gameObject.GetComponentInChildren<Localisation>().key = missingKey;
            validateButton.gameObject.GetComponentInChildren<Localisation>().RefreshText();

            inputText.color = incorrectColor;
        }
    }

    public void UnlockAdress()
    {
        inputText.text = null;

        secretKey.key = secretTextKey;
        secretKey.RefreshText();
        
        foreach (Location location in gameData.locations) if (location.myLocation == Locations.Brothel_HideOut)
        {
            location.known = true;
            location.accessible = true;
        }
    }

    public void ResetTextColor()
    {
        if (inputText.color != baseColor)
        {
            inputText.color = baseColor;
        }
    }
}
