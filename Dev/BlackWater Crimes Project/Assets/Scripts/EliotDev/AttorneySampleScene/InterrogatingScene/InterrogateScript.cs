using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InterrogateScript : MonoBehaviour
{
    [SerializeField] Suspect suspect;
    [SerializeField] Text nameText;
    [SerializeField] Text occupationText;
    [SerializeField] Text dialogueText;
    [SerializeField] Image charaSprite;
    [SerializeField] GameObject[] questionsPlaceHolders;
    [SerializeField] QAndAScript script;
    [SerializeField] Sprite whiteSprite;
    [SerializeField] Sprite andersonSprite;
    [SerializeField] Sprite jenkinsSprite;
    [SerializeField] Sprite morettiSprite;
    bool spriteNeedsToBeUpdated = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject questionsPlaceHolder in questionsPlaceHolders)
        {
            questionsPlaceHolder.SetActive(false);
        }

        switch(suspect)
        {
            case Suspect.White:
                nameText.text = "Abigail White";
                occupationText.text = "Pimp";
                occupationText.text = occupationText.text.ToUpper();
                dialogueText.text = "Hello detective... Next time you want to ask me questions, I would be glad to welcome you at our place, it would be more... Comfortable.";
                charaSprite.sprite = whiteSprite;
                charaSprite.SetNativeSize();

                for (int i = 0; i < script.whiteQAndA.Length; i++)
                {
                    questionsPlaceHolders[i].SetActive(true);
                    questionsPlaceHolders[i].GetComponentInChildren<Text>().text = script.whiteQAndA[i].question;
                }
            return;

            case Suspect.Anderson:
                nameText.text = "Richard Anderson";
                occupationText.text = "Politician";
                occupationText.text = occupationText.text.ToUpper();
                dialogueText.text = "I hope you have good reasons to disturb me during my busy day detective! Go ahead, what do you want? ";
                charaSprite.sprite = andersonSprite;
                charaSprite.SetNativeSize();

                for (int i = 0; i < script.andersonQAndA.Length; i++)
                {
                    questionsPlaceHolders[i].SetActive(true);
                    questionsPlaceHolders[i].GetComponentInChildren<Text>().text = script.andersonQAndA[i].question;
                }
            return;

            case Suspect.Jenkins:
                nameText.text = "Bob Jenkins";
                occupationText.text = "Police officer";
                occupationText.text = occupationText.text.ToUpper();
                dialogueText.text = "I don't quite understand why you bring me here sir... Did I do something wrong while I was on duty?";
                charaSprite.sprite = jenkinsSprite;
                charaSprite.SetNativeSize();

                for (int i = 0; i < script.jenkinsQAndA.Length; i++)
                {
                    questionsPlaceHolders[i].SetActive(true);
                    questionsPlaceHolders[i].GetComponentInChildren<Text>().text = script.jenkinsQAndA[i].question;
                }
            return;

            case Suspect.Moretti:
                nameText.text = "Umberto Moretti";
                occupationText.text = "Handyman";
                occupationText.text = occupationText.text.ToUpper();
                dialogueText.text = "Well officer, non vedo, non sento, non parlo...";
                charaSprite.sprite = morettiSprite;
                charaSprite.SetNativeSize();

                for (int i = 0; i < script.morettiQAndA.Length; i++)
                {
                    questionsPlaceHolders[i].SetActive(true);
                    questionsPlaceHolders[i].GetComponentInChildren<Text>().text = script.morettiQAndA[i].question;
                }
            return;

        }
    }

    public void Question(int questionNumber)
    {
        questionsPlaceHolders[questionNumber].GetComponentInChildren<Text>().color = Color.grey;
        questionsPlaceHolders[questionNumber].GetComponent<EventTrigger>().enabled = false;
        
        switch(suspect)
        {
            case Suspect.White:
                dialogueText.text = script.whiteQAndA[questionNumber].answer;
            return;

            case Suspect.Anderson:
                dialogueText.text = script.andersonQAndA[questionNumber].answer;
            return;

            case Suspect.Jenkins:
                dialogueText.text = script.jenkinsQAndA[questionNumber].answer;
            return;

            case Suspect.Moretti:
                dialogueText.text = script.morettiQAndA[questionNumber].answer;
            return;
        }
    }
}

public enum Suspect
{
    White, Anderson, Jenkins, Moretti 
}
