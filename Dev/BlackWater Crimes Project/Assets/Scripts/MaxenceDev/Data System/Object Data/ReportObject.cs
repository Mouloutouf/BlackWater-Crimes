using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportObject : ObjectData<Report>
{
    public Image agentImage;
    public Text agentText;

    public Image ElementImage;
    public Text ElementText;

    public Text reportText;

    public ElementHolder holder;

    private Report myType = new Report();
    
    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        agentImage.sprite = data.agentSprite;
        agentText.text = data.agentName;

        ElementImage.sprite = data.elementSprite;
        ElementText.text = data.elementName;

        reportText.text = data.reportText;

        if (holder != null) holder.seen = data.seen;

        base.Protocol();
    }

    public void SetQuestion()
    {
        foreach (List<Question> questionList in gameData.questions.Values) 
        {
            foreach (Question question in questionList)
            {
                if (question.reportName == data.elementName)
                {
                    if (question.mode == Modes.Type && question.otherName == data.elementDetailName)
                    {
                        question.unlockedData = true;
                    }

                    question.unlockedData = true;
                }
            }
        }
    }
}
