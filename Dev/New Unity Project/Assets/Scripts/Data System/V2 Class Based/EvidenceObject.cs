using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Evidence
{
    public string name;

    public bool taken;

    public Sprite render2D;
    public Mesh highRender3D;
    public Mesh lowRender3D;

    public bool hasIntel;

    public Sprite intel;
}

public class EvidenceObject : MonoBehaviour
{
    public Evidence evidence;
}
