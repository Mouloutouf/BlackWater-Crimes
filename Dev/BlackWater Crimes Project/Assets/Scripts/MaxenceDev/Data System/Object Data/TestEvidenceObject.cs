using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ObjectData<T> : MonoBehaviour where T : Data
{
    public T data;

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
        // Here goes the code for applying the Data to the Object (OR applying the changes to the object depending on the Data)

        loaded = true;
    }

    public virtual void Check()
    {
        // Here goes the code to change the Object depending on the Data (OR applying the changes to the object depending on the Data)
    }
}

public class TestEvidenceObject : ObjectData<Evidence>
{
    public override void Protocol()
    {
        //if (evidence.intelRevealed) ; // Make the 3D model showcase the intel directly

        base.Protocol();
    }

    public override void Check()
    {
        if (data.taken) gameObject.SetActive(false);
    }
}
