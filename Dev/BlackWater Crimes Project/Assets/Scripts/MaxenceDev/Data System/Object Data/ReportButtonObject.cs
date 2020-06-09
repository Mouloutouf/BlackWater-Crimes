using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportButtonObject : ObjectData<Report>
{
    public Localisation agentKey;
    public Localisation elementKey;
    
    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        agentKey.key = data.agentKey;
        agentKey.RefreshText();
        elementKey.key = data.elementKey;
        elementKey.RefreshText();

        base.Protocol();
    }
}
