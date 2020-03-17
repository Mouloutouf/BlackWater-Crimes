using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurrentDialingScript : MonoBehaviour
{
    [SerializeField] Text dialingText;
    [SerializeField] Text callingText;
    [SerializeField] Button eraseButton;

    List<int> currentDialing = new List<int>();
    string currentContact = "";
    string currentContactScene;

    [Serializable]
    public struct Contact
    {
        public string contactName;
        public int contactNumber;
        public string contactSceneName;
    }

    public Contact[] contacts;

    public void RecordNumber(int number)
    {
        if (currentDialing.Count < 4)
        {
            currentDialing.Add(number);
            if(currentDialing.Count == 4)
            {
                eraseButton.interactable = false;
                StartCoroutine(Call());
            }
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
        yield return new WaitForSeconds(.5f);
        CheckNumber();
    }

    void CheckNumber()
    { 
        for (int i = 0; i < contacts.Length; i++)
        {
            if (contacts[i].contactNumber.ToString() == (currentDialing[0].ToString() + currentDialing[1].ToString() + currentDialing[2].ToString() + currentDialing[3].ToString()))
            {
                currentContact = contacts[i].contactName;
                currentContactScene = contacts[i].contactSceneName;
            }
        }

        if(currentContact != "")
        {
            callingText.text = "Reaching " + currentContact;
            if(currentContactScene != "")
            {
                StartCoroutine(LaunchScene(currentContactScene));
            }
            else
            {
                StartCoroutine(WaitForReset());
            }
        }
        else
        {
            callingText.text = "Wrong number";
            StartCoroutine(WaitForReset());
        }
    }

    
    IEnumerator LaunchScene(string scene)
    {
        yield return new WaitForSeconds(1);
        AsyncOperation asyncLoad =  UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    
    IEnumerator WaitForReset()
    {
        yield return new WaitForSeconds(1);
        callingText.text = "";
        eraseButton.interactable = true;
        ResetDial();
    }

    public void ResetDial()
    {
        currentDialing.Clear();
        currentContact = "";
        dialingText.text = "_ _ _ _";
    }
}
