using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(ClueHolder))]
public class ClueHolderCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ClueHolder clueHolder = (ClueHolder)target;

        clueHolder.size = (ClueHolderSize)EditorGUILayout.EnumPopup("Size", clueHolder.size);
        if (clueHolder.size == ClueHolderSize.Other)
        {
            clueHolder.specificZoomPosition = EditorGUILayout.Vector3Field("Specific Zoom Position", clueHolder.specificZoomPosition);
        }

        clueHolder.hasSpecificRotation = GUILayout.Toggle(clueHolder.hasSpecificRotation, "Has Specific Rotation");
        if (clueHolder.hasSpecificRotation == true)
        {
            clueHolder.specificZoomRotation = EditorGUILayout.Vector3Field("Specific Zoom Rotation", clueHolder.specificZoomRotation);
            clueHolder.specificZoomRotationQuaternion = Quaternion.Euler(clueHolder.specificZoomRotation);
        }

        clueHolder.blockHorizontalRotation = GUILayout.Toggle(clueHolder.blockHorizontalRotation, "Block Horizontal Rotation");
        clueHolder.blockVerticalRotation = GUILayout.Toggle(clueHolder.blockVerticalRotation, "Block Vertical Rotation");
    }
}
