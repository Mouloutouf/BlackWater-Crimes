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
    [SerializeField] Localisation occupationKey;
    [SerializeField] Localisation dialogueKey;

    public string endQuestionKey;

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
    
    void Start()
    {
        foreach (Character character in gameData.characters)
        {
            if (character.isSuspect && character.suspect == gameData.currentSuspect)
            {
                nameText.text = character.name;
                occupationKey.key = character.jobKey;
                dialogueKey.key = character.introPhraseKey;
                charaSprite.sprite = suspectSprites[character.suspect][Emotions.Neutral];
            }
        }
        
        occupationKey.RefreshText();
        dialogueKey.RefreshText();

        //occupationKey.text = occupationKey.text.ToUpper();
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

            dialogueKey.key = questions[currentQuestion]._answers[answerIndex].answer;
            dialogueKey.RefreshText();
            
            Emotions currentEmotion = questions[currentQuestion]._answers[answerIndex].emotion;
            charaSprite.sprite = suspectSprites[gameData.currentSuspect][currentEmotion];
        }
    }

    public void Next()
    {
        if (answerIndex < questions[currentQuestion]._answers.Count - 1)
        {
            answerIndex ++;
            dialogueKey.key = questions[currentQuestion]._answers[answerIndex].answer;
            dialogueKey.RefreshText();

            Emotions currentEmotion = questions[currentQuestion]._answers[answerIndex].emotion;
            charaSprite.sprite = suspectSprites[gameData.currentSuspect][currentEmotion];
        }
        else EndQuestion();
    }

    void EndQuestion()
    {
        dialogueKey.key = endQuestionKey;
        dialogueKey.RefreshText();

        nextButton.SetActive(false);

        questionsParent.transform.GetChild(currentQuestion).gameObject.GetComponentInChildren<Text>().color = Color.grey;
        questionsParent.transform.GetChild(currentQuestion).gameObject.GetComponentInChildren<Text>().fontStyle = FontStyle.Italic;

        answerIndex = 0;
        currentQuestion = -1;
    }
}

