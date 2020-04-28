using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InterrogateScript : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text occupationText;
    [SerializeField] Text dialogueText;
    [SerializeField] Image charaSprite;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject questionsParent;
    [SerializeField] GameData gameData;
    [SerializeField] Sprite whiteSprite;
    [SerializeField] Sprite andersonSprite;
    [SerializeField] Sprite jenkinsSprite;
    [SerializeField] Sprite morettiSprite;
    [SerializeField] Color currentQuestionColor;

    int answerIndex;
    int currentQuestion = -1;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform questionsPlaceHolder in questionsParent.transform)
        {
            questionsPlaceHolder.gameObject.SetActive(false);
        }

        switch(gameData.currentSuspect) //Update chara sprite & intro texts
        {
            case Suspects.Abigail_White:
                nameText.text = "Abigail White";
                occupationText.text = "Pimp";
                dialogueText.text = "Hello detective... Next time you want to ask me questions, I would be glad to welcome you at our place, it would be more... Comfortable.";
                charaSprite.sprite = whiteSprite;
            break;

            case Suspects.Richard_Anderson:
                nameText.text = "Richard Anderson";
                occupationText.text = "Politician";
                dialogueText.text = "I hope you have good reasons to disturb me during my busy day detective! Go ahead, what do you want? ";
                charaSprite.sprite = andersonSprite;
            break;

            case Suspects.Bob_Jenkins:
                nameText.text = "Bob Jenkins";
                occupationText.text = "Police officer";
                dialogueText.text = "I don't quite understand why you bring me here sir... Did I do something wrong while I was on duty?";
                charaSprite.sprite = jenkinsSprite;
            break;

            case Suspects.Umberto_Moretti:
                nameText.text = "Umberto Moretti";
                occupationText.text = "Handyman";
                dialogueText.text = "Well officer, non vedo, non sento, non parlo...";
                charaSprite.sprite = morettiSprite;
                
            break;
        }

        occupationText.text = occupationText.text.ToUpper();
        charaSprite.SetNativeSize();
        
        for (int i = 0; i <gameData.Questions[gameData.currentSuspect].Count; i++)
        {
            questionsParent.transform.GetChild(i).gameObject.SetActive(true);
            questionsParent.transform.GetChild(i).gameObject.GetComponentInChildren<Text>().text = gameData.Questions[gameData.currentSuspect][i].question;
        }
    }

    public void Question(int questionNumber)
    {
        if(currentQuestion == -1)
        {
            nextButton.SetActive(true);
            currentQuestion = questionNumber;

            questionsParent.transform.GetChild(currentQuestion).gameObject.GetComponentInChildren<Text>().color = currentQuestionColor;
            questionsParent.transform.GetChild(currentQuestion).gameObject.GetComponentInChildren<Text>().fontStyle = FontStyle.BoldAndItalic;
            questionsParent.transform.GetChild(currentQuestion).gameObject.GetComponent<Button>().enabled = false;

            dialogueText.text = gameData.Questions[gameData.currentSuspect][questionNumber].answers[answerIndex];
        }
    }

    public void Next()
    {
        if(answerIndex < gameData.Questions[gameData.currentSuspect][currentQuestion].answers.Length - 1)
        {
            answerIndex ++;
            dialogueText.text = gameData.Questions[gameData.currentSuspect][currentQuestion].answers[answerIndex];
        }
        else EndQuestion();
    }

    void EndQuestion()
    {
        dialogueText.text = "Anything else detective?";
        nextButton.SetActive(false);
        questionsParent.transform.GetChild(currentQuestion).gameObject.GetComponentInChildren<Text>().color = Color.grey;
        questionsParent.transform.GetChild(currentQuestion).gameObject.GetComponentInChildren<Text>().fontStyle = FontStyle.Italic;
        answerIndex = 0;
        currentQuestion = -1;
    }
}

