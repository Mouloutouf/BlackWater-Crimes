using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class InterrogateScript : SerializedMonoBehaviour
{
    [Title("Suspect Display")]

    [SerializeField] Text nameText;
    [SerializeField] Text occupationText;
    [SerializeField] Text dialogueText;

    [SerializeField] Image charaSprite;

    [Title("Characters", horizontalLine: false)]
    public Dictionary<Suspects, Dictionary<Emotions, Sprite>> suspectSprites =  new Dictionary<Suspects, Dictionary<Emotions, Sprite>>();

    [Title("")]

    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject questionsParent;

    [SerializeField] GameData gameData;
    
    [SerializeField] Color currentQuestionColor;

    int answerIndex;
    int currentQuestion = -1;

    List<Question> questions = new List<Question>();

    [Header("Débugging")]
    [SerializeField] bool isDebuggingInterrogatory;
    [SerializeField] Suspects debuggingSuspect;

    void Start()
    {
        if (isDebuggingInterrogatory)
        {
            gameData.currentSuspect = debuggingSuspect;
        }

        switch (gameData.currentSuspect) //Update chara sprite & intro texts
        {
            case Suspects.Abigail_White:
                nameText.text = "Abigail White";
                occupationText.text = "Pimp";
                dialogueText.text = "Hello detective... Next time you want to ask me questions, I would be glad to welcome you at our place, it would be more... Comfortable.";
                charaSprite.sprite = suspectSprites[Suspects.Abigail_White][Emotions.Neutral];
                break;

            case Suspects.Richard_Anderson:
                nameText.text = "Richard Anderson";
                occupationText.text = "Politician";
                dialogueText.text = "I hope you have good reasons to disturb me during my busy day detective! Go ahead, what do you want? ";
                charaSprite.sprite = suspectSprites[Suspects.Richard_Anderson][Emotions.Neutral];
                break;

            case Suspects.Bob_Jenkins:
                nameText.text = "Bob Jenkins";
                occupationText.text = "Police officer";
                dialogueText.text = "I don't quite understand why you bring me here sir... Did I do something wrong while I was on duty?";
                charaSprite.sprite = suspectSprites[Suspects.Bob_Jenkins][Emotions.Neutral];
                break;

            case Suspects.Umberto_Moretti:
                nameText.text = "Umberto Moretti";
                occupationText.text = "Handyman";
                dialogueText.text = "Well officer, non vedo, non sento, non parlo...";
                charaSprite.sprite = suspectSprites[Suspects.Umberto_Moretti][Emotions.Neutral];
                break;
        }

        occupationText.text = occupationText.text.ToUpper();
        charaSprite.SetNativeSize();
        
        foreach (Question question in gameData.questions[gameData.currentSuspect]) if (question.unlockedData) questions.Add(question);

        for (int i = 0; i < questions.Count; i++) // Set the Associated Questions Texts
        {
            questionsParent.transform.GetChild(i).gameObject.GetComponentInChildren<Text>().text = questions[i].question;
        }
    }

    public void Question(int questionNumber)
    {
        if (currentQuestion == -1)
        {
            nextButton.SetActive(true);
            currentQuestion = questionNumber;

            questionsParent.transform.GetChild(currentQuestion).gameObject.GetComponentInChildren<Text>().color = currentQuestionColor;
            questionsParent.transform.GetChild(currentQuestion).gameObject.GetComponentInChildren<Text>().fontStyle = FontStyle.BoldAndItalic;
            questionsParent.transform.GetChild(currentQuestion).gameObject.GetComponent<Button>().enabled = false;

            dialogueText.text = questions[currentQuestion]._answers[answerIndex].answer;
            
            Emotions currentEmotion = questions[currentQuestion]._answers[answerIndex].emotion;
            charaSprite.sprite = suspectSprites[gameData.currentSuspect][currentEmotion];
        }
    }

    public void Next()
    {
        if (answerIndex < questions[currentQuestion]._answers.Count - 1)
        {
            answerIndex ++;
            dialogueText.text = questions[currentQuestion]._answers[answerIndex].answer;

            Emotions currentEmotion = questions[currentQuestion]._answers[answerIndex].emotion;
            charaSprite.sprite = suspectSprites[gameData.currentSuspect][currentEmotion];
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

