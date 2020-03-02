using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : MonoBehaviour
{
    public bool fingerprint;
    public bool fingerprintDiscovered;
    [Range(0,1)] public float fingerprintAlpha;
    public bool photographed = false;
}
