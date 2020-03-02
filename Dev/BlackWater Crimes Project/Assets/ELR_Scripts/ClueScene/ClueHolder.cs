using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum ClueHolderSize
{
    Small, Medium, Large, Other
}

public class ClueHolder : MonoBehaviour
{
    public ClueHolderSize size;
    public Vector3 specificZoomPosition;

    public bool hasSpecificRotation;
    public Vector3 specificZoomRotation;
    public Quaternion specificZoomRotationQuaternion;

    public bool blockHorizontalRotation;
    public bool blockVerticalRotation;
}
