using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ObjectData : MonoBehaviour
{
    public Evidence evidence;

    protected bool loaded;

    void Update()
    {
        if (!loaded)
        {
            Protocol();
        }

        Check();
    }

    public virtual void Protocol()
    {
        // Here goes the code for applying the Data to the Object

        loaded = true;
    }

    public virtual void Check()
    {
        // Here goes the code to change the Object depending on the Data
    }
}

public class EvidenceObject : ObjectData
{
    public override void Protocol()
    {
        //if (evidence.intelRevealed) ; // Make the 3D model showcase the intel directly

        base.Protocol();
    }

    public override void Check()
    {
        if (evidence.taken) gameObject.SetActive(false);
    }
}
