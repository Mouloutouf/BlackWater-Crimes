using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentDialingScript : MonoBehaviour
{
    [SerializeField] Text dialingText;
    [SerializeField] Text callingText;

    List<int> currentDialing = new List<int>();

    [Serializable]
    public struct Contact
    {
        public string contactName;
        public int contactNumber;
    }

    public Contact[] contacts;

    public void RecordNumber(int number)
    {
        if (currentDialing.Count < 4)
        {
            currentDialing.Add(number);
            if(currentDialing.Count == 4)
            {
                StartCoroutine(Call());
            }
        }
        else
        {
            currentDialing.Clear();
            currentDialing.Add(number);
        }
        UpdateText();
    }

    void UpdateText()
    {
        if (currentDialing.Count == 1)
        {
            dialingText.text = currentDialing[0].ToString() + " _ _ _";
        }
        else if (currentDialing.Count == 2)
        {
            dialingText.text = currentDialing[0].ToString() + " " + currentDialing[1].ToString() + " _ _";
        }
        else if (currentDialing.Count == 3)
        {
            dialingText.text = currentDialing[0].ToString() + " " + currentDialing[1].ToString() + " " + currentDialing[2].ToString() + " _";
        }
        else if (currentDialing.Count == 4)
        {
            dialingText.text = currentDialing[0].ToString() + " " + currentDialing[1].ToString() + " " + currentDialing[2].ToString() + " " + currentDialing[3].ToString();
        }
    }

    IEnumerator Call()
    {
        callingText.text = "Calling";
        yield return new WaitForSeconds(.5f);
        callingText.text += ".";
        yield return new WaitForSeconds(.5f);
        callingText.text += ".";
        yield return new WaitForSeconds(.5f);
        callingText.text += ".";
        CheckNumber();
    }

    void CheckNumber()
    {

    }
}
