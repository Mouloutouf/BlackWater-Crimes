using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceObject : ObjectData<Evidence>
{
    [Range(0, 1)] public float intelAlpha;

    private Evidence myType = new Evidence();
    
    void Start()
    {
        GetGameData();

        List<Evidence> myDataList = gameData.allEvidences[data.modeCategory.location];

        if (!instantiate) LoadDataOfType(myType, myDataList);
    }

    public override void Protocol()
    {
        base.Protocol();
    }
}
