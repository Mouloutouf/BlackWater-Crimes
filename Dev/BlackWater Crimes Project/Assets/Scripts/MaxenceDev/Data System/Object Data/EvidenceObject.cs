using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceObject : ObjectData<Evidence>
{
    [Range(0, 1)] public float intelAlpha;

    private Evidence myType;

    void Start()
    {
        if (!instantiate) LoadDataOfType(myType);
    }

    public override void Protocol()
    {
        base.Protocol();
    }
}
