using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class QuickCallScript : SerializedMonoBehaviour
{

    [Title ("GameData References")]
    public GameData gameData;
    public Dictionary<Indics, Button> buttons = new Dictionary<Indics, Button>();


    [Title ("Texts")] //A changer avec le système de localisation ?
    public string englishAvailableText;
    public string englishUnvailableText;
    public string frenchAvailableText;
    public string frenchUnvailableText;

    string currentAvailableText;    
    string currentUnavailableText;


    [Title ("Scene References")]
    public CurrentDialingScript currentDialingScript;
    public CadranScript cadranScript;
    public Button eraseButton;
    public Animator cadranAnimator;


    void Start()
    {
        switch (gameData.gameLanguage)
        {
            case Languages.English:
                currentAvailableText = englishAvailableText;
                currentUnavailableText = englishUnvailableText;
            break;

            case Languages.French:
                currentAvailableText = frenchAvailableText;
                currentUnavailableText = frenchUnvailableText;
            break;
        }

        foreach(Indics indic in buttons.Keys)
        {
            if(gameData.indics.ContainsKey(indic)) //Check if this indic is stored in the Game Data
            {
                if(gameData.indics[indic].quickCallAvailable == true)
                {
                    buttons[indic].interactable = true; //If indic has been called once in the Game Data, the quick call button become interactable
                    buttons[indic].GetComponentInChildren<Text>().text = currentAvailableText;
                } 
                else 
                {
                    buttons[indic].interactable = false; //Otherwise, not interactable
                    buttons[indic].GetComponentInChildren<Text>().text = currentUnavailableText;
                }
            }
        }
    }

    public void QuickCall(int phoneNumber)
    {
        //Alors c'est de la magie noire mais ça permet de split le int dans une liste
        List<int> phoneDigits = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            phoneDigits.Add(phoneNumber % 10);
            phoneNumber = phoneNumber / 10;
        }

        phoneDigits.Reverse(); 

        StartCoroutine(CadranAnimation(phoneDigits));
    }

    IEnumerator CadranAnimation(List<int> digits)
    {
        currentDialingScript.ResetDial();

        for (int i = 0; i < digits.Count; i++)
        {   
            cadranAnimator.enabled = true;
            cadranScript.enabled = false;
            eraseButton.interactable = false;

            cadranAnimator.SetInteger("Digit", digits[i]);
            yield return new WaitForSeconds(1);
        }

        cadranAnimator.SetInteger("Digit", 11);
    }
}
