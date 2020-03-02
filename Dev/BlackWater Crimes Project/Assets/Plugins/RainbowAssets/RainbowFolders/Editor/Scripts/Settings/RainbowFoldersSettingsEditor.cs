using Borodar.RainbowAssets.Core.Games.Collections;
using UnityEditor;

namespace Borodar.RainbowFolders.Editor.Settings
{
    [CustomEditor(typeof (RainbowFoldersSettings))]
    public class RainbowFoldersSettingsEditor : UnityEditor.Editor
    {
        private const string PROP_NAME_FOLDERS = "Folders";

        private SerializedProperty _foldersProperty;

        protected void OnEnable()
        {
            _foldersProperty = serializedObject.FindProperty(PROP_NAME_FOLDERS);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            ReorderableListGUI.Title("Rainbow Folders");
            ReorderableListGUI.ListField(_foldersProperty);
            serializedObject.ApplyModifiedProperties();
        }

    }
}