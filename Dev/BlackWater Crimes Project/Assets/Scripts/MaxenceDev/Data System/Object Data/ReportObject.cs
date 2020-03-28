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

    private Report myType;

    void Start()
    {
        LoadDataOfType(myType);
    }

    public override void Protocol()
    {
        agentImage.sprite = data.agentSprite;
        agentText.text = data.agentName;

        ElementImage.sprite = data.elementSprite;
        ElementText.text = data.elementName;

        reportText.text = data.reportText;

        base.Protocol();
    }
}
