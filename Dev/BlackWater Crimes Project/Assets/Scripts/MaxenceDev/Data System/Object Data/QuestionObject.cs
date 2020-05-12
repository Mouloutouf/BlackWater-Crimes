using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionObject : ObjectData<Question>
{
    void Start()
    {
        GetGameData();
    }

    public override void Protocol()
    {
        base.Protocol();
    }
}
