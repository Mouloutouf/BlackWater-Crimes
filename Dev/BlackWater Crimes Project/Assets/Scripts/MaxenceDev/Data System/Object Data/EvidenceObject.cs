using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceObject : ObjectData<Evidence>
{
    [Range(0, 1)] public float intelAlpha;

    public override void Protocol()
    {
        base.Protocol();
    }
}
