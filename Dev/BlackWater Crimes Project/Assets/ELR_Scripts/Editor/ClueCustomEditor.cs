using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Clue))]
public class ClueCustomEditor : Editor
{
    override public void OnInspectorGUI()
    {
        Clue clue = (Clue)target;

        clue.fingerprint = GUILayout.Toggle(clue.fingerprint, "Is A Fingerprint");
    }
}


