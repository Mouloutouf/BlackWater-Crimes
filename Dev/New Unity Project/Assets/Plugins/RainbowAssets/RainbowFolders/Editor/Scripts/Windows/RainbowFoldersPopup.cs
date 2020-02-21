using System.Collections.Generic;
using System.IO;
using Borodar.RainbowCore.Editor;
using Borodar.RainbowFolders.Editor.Settings;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using KeyType = Borodar.RainbowFolders.Editor.Settings.RainbowFolder.KeyType;

namespace Borodar.RainbowFolders.Editor
{
    public class RainbowFoldersPopup : DraggablePopupWindow
    {
        private const float PADDING = 8f;
        private const float SPACING = 1f;
        private const float LINE_HEIGHT = 16f;
        private const float LABELS_WIDTH = 85f;
        private const float PREVIEW_SIZE_SMALL = 16f;
        private const float PREVIEW_SIZE_LARGE = 64f;
        private const float BUTTON_WIDTH = 55f;
        private const float BUTTON_WIDTH_SMALL = 16f;

        private const float WINDOW_WIDTH = 325f;
        private const float WINDOW_HEIGHT = 140f;

        private static readonly Vector2 WINDOW_SIZE = new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);

        private static Rect _windowRect;
        private static Rect _backgroundRect;

        private List<string> _paths;
        private RainbowFoldersSettings _settings;
        private RainbowFolder[] _existingFolders;
        private RainbowFolder _currentFolder;

        //---------------------------------------------------------------------
        // Public
        //---------------------------------------------------------------------

        public static RainbowFoldersPopup GetDraggableWindow()
        {
            return GetDraggableWindow<RainbowFoldersPopup>();
        }

        public void ShowWithParams(Vector2 inPosition, List<string> paths, int pathIndex)
        {
            _paths = paths;
            _settings = RainbowFoldersSettings.Instance;

            var size = paths.Count;
            _existingFolders = new RainbowFolder[size];
            _currentFolder = new RainbowFolder(KeyType.Path, paths[pathIndex]);

            for (var i = 0; i < size; i++)
                _existingFolders[i] = _settings.GetFolderByPath(paths[i]);

            if (_existingFolders[pathIndex] != null)
                _currentFolder.CopyFrom(_existingFolders[pathIndex]);

            // Resize

            var customIconHeight = (_currentFolder.IsIconCustom) ? LINE_HEIGHT * 2f : 0f;
            var customBackgroundHeight = (_currentFolder.IsBackgroundCustom) ? LINE_HEIGHT : 0f;

            var rect = new Rect(inPosition, WINDOW_SIZE)
            {
                height = WINDOW_HEIGHT + customIconHeight + customBackgroundHeight
            };

            _windowRect = new Rect(Vector2.zero, rect.size);
            _backgroundRect = new Rect(Vector2.one, rect.size - new Vector2(2f, 2f));

            Show<RainbowFoldersPopup>(rect);
        }

        //---------------------------------------------------------------------
        // Messages
        //---------------------------------------------------------------------

        public override void OnGUI()
        {
            base.OnGUI();
            ChangeWindowSize(_currentFolder.IsIconCustom, _currentFolder.IsBackgroundCustom);
            var rect = _windowRect;

            // Background

            var borderColor = EditorGUIUtility.isProSkin ? new Color(0.13f, 0.13f, 0.13f) : new Color(0.51f, 0.51f, 0.51f);           
            EditorGUI.DrawRect(_windowRect, borderColor);

            var backgroundColor = EditorGUIUtility.isProSkin ? new Color(0.18f, 0.18f, 0.18f) : new Color(0.83f, 0.83f, 0.83f);
            EditorGUI.DrawRect(_backgroundRect, backgroundColor);

            // Labels

            rect.x += 0.5f * PADDING;
            rect.y += PADDING;
            rect.width = LABELS_WIDTH - PADDING;
            rect.height = LINE_HEIGHT;

            _currentFolder.Type = (KeyType)EditorGUI.EnumPopup(rect, _currentFolder.Type);

            rect.y += LINE_HEIGHT + 6f;
            EditorGUI.LabelField(rect, "Icon");

            if (_currentFolder.IsIconCustom)
            {
                rect.y += LINE_HEIGHT + 4f;
                EditorGUI.LabelField(rect, "x16", EditorStyles.miniLabel);
                rect.y += LINE_HEIGHT + 2f;
                EditorGUI.LabelField(rect, "x64", EditorStyles.miniLabel);
            }

            rect.y += LINE_HEIGHT + 2f;
            EditorGUI.LabelField(rect, "Recursive", EditorStyles.miniLabel);

            rect.y += LINE_HEIGHT + SPACING * 6f;
            EditorGUI.LabelField(rect, "Background");

            if (_currentFolder.IsBackgroundCustom)
            {
                rect.y += LINE_HEIGHT + 4f;
                EditorGUI.LabelField(rect, "x16", EditorStyles.miniLabel);
            }

            rect.y += LINE_HEIGHT + 2f;
            EditorGUI.LabelField(rect, "Recursive", EditorStyles.miniLabel);

            // Values

            rect.x += LABELS_WIDTH;
            rect.y = _windowRect.y + PADDING;
            rect.width = _windowRect.width - LABELS_WIDTH - PADDING;

            GUI.enabled = false;
            if (_paths.Count == 1)
                _currentFolder.Key = (_currentFolder.Type == KeyType.Path) ? _paths[0] : Path.GetFileName(_paths[0]);
            else
                _currentFolder.Key = "---";
            EditorGUI.TextField(rect, GUIContent.none, _currentFolder.Key);
            GUI.enabled = true;

            
            rect.width -= PREVIEW_SIZE_LARGE + PADDING;
            rect.y += LINE_HEIGHT + SPACING * 4f + SPACING;
            DrawIconPopupMenu(rect, _currentFolder);

            if (_currentFolder.IsIconCustom)
            {
                rect.y += LINE_HEIGHT + 4f + SPACING;
                _currentFolder.SmallIcon = (Texture2D) EditorGUI.ObjectField(rect, _currentFolder.SmallIcon, typeof(Texture2D), false);

                rect.y += LINE_HEIGHT + SPACING;
                _currentFolder.LargeIcon = (Texture2D) EditorGUI.ObjectField(rect, _currentFolder.LargeIcon, typeof(Texture2D), false);
            }

            rect.y += LINE_HEIGHT + 2f;
            _currentFolder.IsIconRecursive = EditorGUI.Toggle(rect, _currentFolder.IsIconRecursive);


            rect.y += LINE_HEIGHT + SPACING * 6f;
            DrawBackgroundPopupMenu(rect, _currentFolder);

            if (_currentFolder.IsBackgroundCustom)
            {
                rect.y += LINE_HEIGHT + 4f + SPACING;
                _currentFolder.Background = (Texture2D) EditorGUI.ObjectField(rect, _currentFolder.Background, typeof(Texture2D), false);
            }

            rect.y += LINE_HEIGHT + 2f;
            _currentFolder.IsBackgroundRecursive = EditorGUI.Toggle(rect, _currentFolder.IsBackgroundRecursive);

            // Preview

            rect.x += rect.width + PADDING;
            rect.y = _windowRect.y + LINE_HEIGHT + 4f;
            rect.width = rect.height = PREVIEW_SIZE_LARGE;
            GUI.DrawTexture(rect, RainbowFoldersEditorUtility.GetDefaultFolderIcon());
            if (_currentFolder.LargeIcon) GUI.DrawTexture(rect, _currentFolder.LargeIcon);

            rect.y += PREVIEW_SIZE_LARGE - PREVIEW_SIZE_SMALL - 4f;
            rect.width = rect.height = PREVIEW_SIZE_SMALL;
            GUI.DrawTexture(rect, RainbowFoldersEditorUtility.GetDefaultFolderIcon());
            if (_currentFolder.SmallIcon) GUI.DrawTexture(rect, _currentFolder.SmallIcon);

            rect.y += LINE_HEIGHT + SPACING * 3f;
            rect.width = PREVIEW_SIZE_LARGE;
            if (_currentFolder.Background != null)
            GUI.DrawTexture(rect, _currentFolder.Background);
            rect.x += 13f;
            EditorGUI.LabelField(rect, "Folder");

            // Buttons

            rect.x = PADDING;
            rect.y = position.height - LINE_HEIGHT - 0.75f * PADDING;
            rect.width = BUTTON_WIDTH_SMALL;
            ButtonSettings(rect);

            rect.x += BUTTON_WIDTH_SMALL + 0.75f * PADDING;
            ButtonDefault(rect);

            rect.x = WINDOW_WIDTH - 2f * (BUTTON_WIDTH + PADDING);
            rect.width = BUTTON_WIDTH;
            ButtonCancel(rect);

            rect.x = WINDOW_WIDTH - BUTTON_WIDTH - PADDING;
            ButtonApply(rect);
        }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private void ChangeWindowSize(bool hasCustomIcon, bool hasCustomBackground)
        {
            var rect = position;
            var customIconHeight = (hasCustomIcon) ? LINE_HEIGHT * 2f + 6f : 0f;
            var customBackgroundHeight = (hasCustomBackground) ? LINE_HEIGHT + 4f : 0f;

            var resizeHeight = WINDOW_HEIGHT + customIconHeight + customBackgroundHeight;
            if (resizeHeight == rect.height) return;

            rect.height = resizeHeight;
            position = rect;

            _windowRect.height = rect.height;
            _backgroundRect.height = rect.height - 2f;
        }

        private static void DrawIconPopupMenu(Rect rect, RainbowFolder folder)
        {
            string menuName;
            if (folder.IsIconCustom)
            {
                menuName = "Custom";
            }
            else
            {
                menuName = folder.SmallIcon != null ? folder.SmallIcon.name : "None";
            }
            if (GUI.Button(rect, new GUIContent(menuName), "popup"))
            {
                RainbowFoldersIconsMenu.ShowDropDown(rect, folder);
            }
        }

        private static void DrawBackgroundPopupMenu(Rect rect, RainbowFolder folder)
        {
            string menuName;
            if (folder.IsBackgroundCustom)
            {
                menuName = "Custom";
            }
            else
            {
                menuName = folder.Background != null ? folder.Background.name : "None";
            }
            if (GUI.Button(rect, new GUIContent(menuName), "popup"))
            {
                RainbowFoldersBackgroundsMenu.ShowDropDown(rect, folder);
            }
        }

        private void ButtonSettings(Rect rect)
        {
            var icon = RainbowFoldersEditorUtility.GetSettingsButtonIcon();
            if (!GUI.Button(rect, new GUIContent(icon, "Settings"), GUIStyle.none)) return;
            Selection.activeObject = _settings;
            Close();
        }

        private void ButtonDefault(Rect rect)
        {
            var icon = RainbowFoldersEditorUtility.GetDeleteButtonIcon();
            if (!GUI.Button(rect, new GUIContent(icon, "Revert to Default"), GUIStyle.none)) return;

            _currentFolder.SmallIcon = null;
            _currentFolder.LargeIcon = null;
            _currentFolder.IsIconRecursive = false;
            _currentFolder.IsIconCustom = false;

            _currentFolder.Background = null;
            _currentFolder.IsBackgroundRecursive = false;
            _currentFolder.IsBackgroundCustom = false;
        }

        private void ButtonCancel(Rect rect)
        {
            if (!GUI.Button(rect, "Cancel")) return;
            Close();
        }

        private void ButtonApply(Rect rect)
        {
            if (!GUI.Button(rect, "Apply")) return;

            for (var i = 0; i < _existingFolders.Length; i++)
            {
                _currentFolder.Key = (_currentFolder.Type == KeyType.Path)
                    ? _paths[i]
                    : Path.GetFileName(_paths[i]);

                _settings.UpdateFolder(_existingFolders[i], _currentFolder);
            }
            Close();
        }
    }
}