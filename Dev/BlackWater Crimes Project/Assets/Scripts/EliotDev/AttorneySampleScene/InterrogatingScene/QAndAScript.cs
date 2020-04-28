using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class QAndAScript: SerializedMonoBehaviour
{
    [System.Serializable]
    public struct QuestionAndAnswer
    {
        [Title("Question", bold: false)]
        [HideLabel]
        [MultiLineProperty(2)]
        public string question;

        [Title("Answer", bold: false)]
        [MultiLineProperty(4)]
        public string[] answers;
    }

    public Dictionary<Suspects, List<QuestionAndAnswer>> QuestionsAndAnswers = new Dictionary<Suspects, List<QuestionAndAnswer>>();
}
