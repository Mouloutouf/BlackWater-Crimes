using System;
using UnityEngine;
using UnityEditor;
using KeyType = Borodar.RainbowFolders.Editor.Settings.RainbowFolder.KeyType;

namespace Borodar.RainbowFolders.Editor.Settings
{
    [CustomPropertyDrawer(typeof(RainbowFolder))]
    public class RainbowFolderDrawer : PropertyDrawer
    {
        private const float PADDING = 8f;
        private const float SPACING = 1f;
        private const float LINE_HEIGHT = 16f;
        private const float LABELS_WIDTH = 100f;
        private const float PREVIEW_SIZE_SMALL = 16f;
        private const float PREVIEW_SIZE_LARGE = 64f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var originalPosition = position;

            var folderKey = property.FindPropertyRelative("Key");
            var folderKeyType = property.FindPropertyRelative("Type");

            var smallIcon = property.FindPropertyRelative("SmallIcon");
            var largeIcon = property.FindPropertyRelative("LargeIcon");
            var iconRecursive = property.FindPropertyRelative("IsIconRecursive");

            var background = property.FindPropertyRelative("Background");
            var backgroundRecursive = property.FindPropertyRelative("IsBackgroundRecursive");

            // Labels

            position.y += PADDING;
            position.width = LABELS_WIDTH - PADDING;
            position.height = LINE_HEIGHT;

            var typeSelected = (KeyType) Enum.GetValues(typeof(KeyType)).GetValue(folderKeyType.enumValueIndex);
            folderKeyType.enumValueIndex = (int)(KeyType) EditorGUI.EnumPopup(position, typeSelected);

            position.y += LINE_HEIGHT + SPACING;
            EditorGUI.LabelField(position, "Small Icon");

            position.y += LINE_HEIGHT + SPACING;
            EditorGUI.LabelField(position, "Large Icon");

            position.y += LINE_HEIGHT + SPACING;
            EditorGUI.LabelField(position, "Recursive");

            position.y += LINE_HEIGHT + SPACING;
            EditorGUI.LabelField(position, "Background");

            position.y += LINE_HEIGHT + SPACING;
            EditorGUI.LabelField(position, "Recursive");

            // Values

            position.x += LABELS_WIDTH;
            position.y = originalPosition.y + PADDING;
            position.width = originalPosition.width - LABELS_WIDTH;
            EditorGUI.PropertyField(position, folderKey, GUIContent.none);

            position.width = originalPosition.width - LABELS_WIDTH - PREVIEW_SIZE_LARGE - PADDING;

            position.y += LINE_HEIGHT + SPACING + (EditorGUIUtility.isProSkin ? SPACING : 0f);
            EditorGUI.PropertyField(position, smallIcon, GUIContent.none);

            position.y += LINE_HEIGHT + SPACING;
            EditorGUI.PropertyField(position, largeIcon, GUIContent.none);

            position.y += LINE_HEIGHT + (EditorGUIUtility.isProSkin ?  0f : SPACING) ;
            EditorGUI.PropertyField(position, iconRecursive, GUIContent.none);

            position.y += LINE_HEIGHT + SPACING;
            EditorGUI.PropertyField(position, background, GUIContent.none);

            position.y += LINE_HEIGHT + (EditorGUIUtility.isProSkin ?  0f : SPACING) ;
            EditorGUI.PropertyField(position, backgroundRecursive, GUIContent.none);

            // Preview

            position.x += position.width + PADDING;
            position.y = originalPosition.y + LINE_HEIGHT + SPACING + 8f;
            position.width = position.height = PREVIEW_SIZE_LARGE;
            GUI.DrawTexture(position, (Texture2D) largeIcon.objectReferenceValue ?? RainbowFoldersEditorUtility.GetDefaultFolderIcon());

            position.y += PREVIEW_SIZE_LARGE - PREVIEW_SIZE_SMALL - 4f;
            position.width = position.height = PREVIEW_SIZE_SMALL;
            GUI.DrawTexture(position, (Texture2D) smallIcon.objectReferenceValue ?? RainbowFoldersEditorUtility.GetDefaultFolderIcon());

            position.y += LINE_HEIGHT + SPACING * 4f;
            position.width = PREVIEW_SIZE_LARGE;
            if (background.objectReferenceValue != null)
            GUI.DrawTexture(position, (Texture2D) background.objectReferenceValue);
            position.x += 13f;
            EditorGUI.LabelField(position, "Folder");
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return PREVIEW_SIZE_LARGE + LINE_HEIGHT * 3f + 2f;
        }
    }
}