using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class QAndAScript: MonoBehaviour
{
    [System.Serializable]
    public struct QuestionAndAnswer
    {
        [Title("Question", bold: false)]
        [HideLabel]
        [MultiLineProperty(2)]
        public string question;

        [Title("Answer", bold: false)]
        [HideLabel]
        [MultiLineProperty(4)]
        public string answer;
    }

    public QuestionAndAnswer[] whiteQAndA;
    public QuestionAndAnswer[] andersonQAndA;
    public QuestionAndAnswer[] jenkinsQAndA;
    public QuestionAndAnswer[] morettiQAndA;
}
