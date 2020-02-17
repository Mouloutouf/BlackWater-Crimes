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

    public bool intelRevealed;

    public string description;
}

public class EvidenceObject : MonoBehaviour
{
    public Evidence evidence;

    private bool loaded;

    void Start()
    {

    }

    void Update()
    {
        if (!loaded)
        {
            Protocol();
        }
    }

    void Protocol()
    {
        if (evidence.taken) gameObject.SetActive(false);

        if (evidence.intelRevealed) ; // Make the 3D model showcase the intel directly
    }
}
