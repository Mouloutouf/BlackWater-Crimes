using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

public enum ClueHolderSize
{
    Small, Medium, Large, Other
}

public class ClueHolder : MonoBehaviour
{
    public ClueHolderSize size;
    [ShowIf("size", ClueHolderSize.Other)]
    public Vector3 specificZoomPosition;

    public bool hasSpecificRotation;
    [ShowIf("hasSpecificRotation")]
    public Vector3 specificZoomRotation;
    [HideInInspector] public Quaternion specificZoomRotationQuaternion;

    public bool blockHorizontalRotation;
    public bool blockVerticalRotation;
}
