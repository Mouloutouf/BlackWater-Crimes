using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportObject : ObjectData<Report>
{
    public GameData _gameData;

    public Image agentImage;
    public Localisation agentKey;

    public float agentFactor;

    public Image ElementImage;
    public Localisation elementKey;
    
    public Localisation reportKey;

    public Image signatureImage;
    
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

        agentKey.key = data.agentKey;
        agentKey.RefreshText();

        ElementImage.sprite = data.elementSprite;
        elementKey.key = data.elementKey;
        elementKey.RefreshText();

        reportKey.key = data.reportKey;
        reportKey.RefreshText();

        if (signatureImage != null) signatureImage.sprite = data.signature;
        
        base.Protocol();
    }

    public void SetData()
    {
        // Set Questions
        
        foreach (List<Question> questionList in _gameData.questions.Values)
        {
            foreach (Question question in questionList)
            {
                if (question.reportKey == data.reportKey)
                {
                    if (question.mode == Modes.Evidence && question.otherKey == data.detailKey)
                    {
                        question.unlockedData = true;
                    }

                    question.unlockedData = true;
                }
            }
        }

        // Set Location Known

        if (data.giveAccess)
        {
            foreach (Location location in _gameData.locations)
            {
                if (location.myLocation == data.locationToAccess)
                {
                    location.known = true;
                    location.accessible = true;
                }
            }
        }
    }
}
