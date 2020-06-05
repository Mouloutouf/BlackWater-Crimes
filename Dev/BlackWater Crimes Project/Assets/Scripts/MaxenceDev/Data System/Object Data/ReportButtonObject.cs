using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportButtonObject : ObjectData<Report>
{
    public Text agentText;
    public Localisation elementKey;

    public GameObject associatedReport { get; set; }

    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        agentText.text = data.agentName;
        elementKey.key = data.elementKey;
        elementKey.RefreshText();

        base.Protocol();
    }
}
