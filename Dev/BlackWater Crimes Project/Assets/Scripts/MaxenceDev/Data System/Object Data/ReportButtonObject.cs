using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportButtonObject : ObjectData<Report>
{
    public Text agentText;
    public Text elementText;

    public GameObject associatedReport { get; set; }

    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        agentText.text = data.agentName;
        elementText.text = data.elementName;

        base.Protocol();
    }
}
