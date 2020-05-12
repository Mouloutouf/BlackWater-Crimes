﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportObject : ObjectData<Report>
{
    public Image agentImage;
    public Text agentText;

    public float agentFactor;

    public Image ElementImage;
    public Text ElementText;
    
    public Text reportText;

    public Image signatureImage;

    public ElementHolder holder;
    
    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        agentImage.sprite = data.agentSprite;
        agentImage.SetNativeSize();
        RectTransform rect = agentImage.gameObject.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.rect.width / agentFactor, rect.rect.height / agentFactor);

        agentText.text = data.agentName;

        ElementImage.sprite = data.elementSprite;
        ElementText.text = data.elementName;

        reportText.text = data.reportText;

        if (signatureImage != null) signatureImage.sprite = data.signature;

        if (holder != null) holder.seen = data.seen;

        base.Protocol();
    }

    public void SetData(Report report)
    {
        // Set Questions
        
        foreach (List<Question> questionList in gameData.questions.Values) 
        {
            foreach (Question question in questionList)
            {
                if (question.reportName == data.elementName)
                {
                    if (question.mode == Modes.Evidence && question.otherName == data.elementDetailName)
                    {
                        question.unlockedData = true;
                    }

                    question.unlockedData = true;
                }
            }
        }

        // Set Location Known

        if (report.giveAccess)
        {
            foreach (Location location in gameData.locations)
            {
                if (location.myLocation == report.locationToAccess) location.known = true;
            }
        }
    }
}
