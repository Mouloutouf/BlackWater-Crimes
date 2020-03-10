using UnityEngine;
using UnityEditor;

namespace Borodar.RainbowFolders.Editor.Settings
{
    [CustomPropertyDrawer(typeof(FolderColor))]
    public class FolderColorDrawer : PropertyDrawer
    {
        private const float PADDING = 8f;
        private const float LINE_HEIGHT = 16f;
        private const float LABELS_WIDTH = 100f;
        private const float PREVIEW_SIZE_SMALL = 16f;
        private const float PREVIEW_SIZE_LARGE = 64f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var originalPosition = position;

            var folderColor = property.FindPropertyRelative("Color");
            var smallIcon = property.FindPropertyRelative("SmallIcon");
            var largeIcon = property.FindPropertyRelative("LargeIcon");
            var background = property.FindPropertyRelative("Background");

            // Labels

            position.y += PADDING;
            position.width = LABELS_WIDTH;
            position.height = LINE_HEIGHT;

            EditorGUI.LabelField(position, "Folder Color");
            position.y += LINE_HEIGHT;
            EditorGUI.LabelField(position, "Small Icon");
            position.y += LINE_HEIGHT;
            EditorGUI.LabelField(position, "Large Icon");
            position.y += LINE_HEIGHT;
            EditorGUI.LabelField(position, "Background");

            // Values

            position.x += LABELS_WIDTH;
            position.y = originalPosition.y + PADDING;
            position.width = originalPosition.width - LABELS_WIDTH - PREVIEW_SIZE_LARGE - PADDING;

            EditorGUI.PropertyField(position, folderColor, GUIContent.none);
            position.y += LINE_HEIGHT;
            EditorGUI.PropertyField(position, smallIcon, GUIContent.none);
            position.y += LINE_HEIGHT;
            EditorGUI.PropertyField(position, largeIcon, GUIContent.none);
            position.y += LINE_HEIGHT;
            EditorGUI.PropertyField(position, background, GUIContent.none);

            // Preview

            position.x += position.width + PADDING;
            position.y = originalPosition.y;
            position.width = position.height = PREVIEW_SIZE_LARGE;
            GUI.DrawTexture(position, (Texture2D) largeIcon.objectReferenceValue ?? RainbowFoldersEditorUtility.GetDefaultFolderIcon());

            position.y += PREVIEW_SIZE_LARGE - PREVIEW_SIZE_SMALL - 4f;
            position.width = position.height = PREVIEW_SIZE_SMALL;
            GUI.DrawTexture(position, (Texture2D) smallIcon.objectReferenceValue ?? RainbowFoldersEditorUtility.GetDefaultFolderIcon());

            position.y = originalPosition.y + PREVIEW_SIZE_LARGE;
            position.width = PREVIEW_SIZE_LARGE;
            position.height = 10f;
            if (background.objectReferenceValue != null)
            GUI.DrawTexture(position, (Texture2D) background.objectReferenceValue);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return PREVIEW_SIZE_LARGE + LINE_HEIGHT;
        }
    }
}