using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Borodar.RainbowFolders.Editor.Settings;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using ProjectWindowItemCallback = UnityEditor.EditorApplication.ProjectWindowItemCallback;

namespace Borodar.RainbowFolders.Editor
{
    /*
    * This script allows you to set custom icons for folders in project browser.
    * Recommended icon sizes - small: 16x16 px, large: 64x64 px;
    */

    [InitializeOnLoad]
    public class RainbowFoldersBrowserIcons
    {
        private const float SMALL_ICON_SIZE = 16f;
        private const float LARGE_ICON_SIZE = 64f;

        private static Func<bool> _isCollabEnabled;
        private static Func<bool> _isVcsEnabled;

        #if UNITY_2017_1_OR_NEWER
            private static CollabItemCallback _drawCollabOverlay;
        #else
            private static ProjectWindowItemCallback _drawCollabOverlay;
        #endif

        private static ProjectWindowItemCallback _drawVcsOverlay;
        private static bool _multiSelection;

        private static GUIStyle _itemBgStyle;

        //---------------------------------------------------------------------
        // Ctors
        //---------------------------------------------------------------------

        static RainbowFoldersBrowserIcons()
        {
            EditorApplication.projectWindowItemOnGUI += ReplaceFolderIcon;
            EditorApplication.projectWindowItemOnGUI += DrawEditIcon;
            EditorApplication.projectWindowItemOnGUI += ShowWelcomeWindow;

            var assembly = typeof(EditorApplication).Assembly;
            InitCollabDelegates(assembly);
            InitVcsDelegates(assembly);
        }

        //---------------------------------------------------------------------
        // Properties
        //---------------------------------------------------------------------

        public static GUIStyle ItemBgStyle
        {
            get { return _itemBgStyle ?? (_itemBgStyle = new GUIStyle("ProjectBrowserIconAreaBg")); }
        }

        //---------------------------------------------------------------------
        // Delegates
        //---------------------------------------------------------------------

        #if UNITY_2017_1_OR_NEWER
        public delegate void CollabItemCallback(Rect iconRect, string guid, bool isListMode);
        #endif

        private static void ReplaceFolderIcon(string guid, Rect rect)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (!AssetDatabase.IsValidFolder(path)) return;

            var setting = RainbowFoldersSettings.Instance;
            if (setting == null) return;
            var folder = RainbowFoldersSettings.Instance.GetFolderByPath(path, true);
            if (folder == null) return;

            var isSmall = IsIconSmall(rect);

            // Backround
            var backgroundTex = folder.Background;
            if (backgroundTex != null)
            {
                var backgroundRect = GetBackgroundRect(rect, isSmall);
                DrawCustomBackground(backgroundRect, backgroundTex);
            }

            // Icon
            var iconTex = isSmall ? folder.SmallIcon : folder.LargeIcon;
            if (iconTex != null)
            {
                var iconRect = GetIconRect(rect, isSmall);
                DrawCustomIcon(guid, iconRect, iconTex, isSmall);
            }
        }

        private static void DrawEditIcon(string guid, Rect rect)
        {
            if ((Event.current.modifiers & RainbowFoldersPreferences.ModifierKey) == EventModifiers.None)
            {
                _multiSelection = false;
                return;
            }

            var isSmall = IsIconSmall(rect);
            var iconRect = GetIconRect(rect, isSmall);
            var isMouseOver = rect.Contains(Event.current.mousePosition);
            _multiSelection = (IsSelected(guid)) ? isMouseOver || _multiSelection : !isMouseOver && _multiSelection;

            // if mouse is not over current folder icon or selected group
            if (!isMouseOver && (!IsSelected(guid) || !_multiSelection)) return;

            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (!AssetDatabase.IsValidFolder(path)) return;

            var editIcon = RainbowFoldersEditorUtility.GetEditFolderIcon(isSmall, EditorGUIUtility.isProSkin);
            DrawCustomIcon(guid, iconRect, editIcon, isSmall);

            if (GUI.Button(rect, GUIContent.none, GUIStyle.none))
            {
                ShowPopupWindow(iconRect, path);
            }

            EditorApplication.RepaintProjectWindow();
        }

        private static void ShowWelcomeWindow(string guid, Rect rect)
        {
            if (EditorPrefs.GetBool(RainbowFoldersWelcome.PREF_KEY))
            {
                // ReSharper disable once DelegateSubtraction
                EditorApplication.projectWindowItemOnGUI -= ShowWelcomeWindow;
                return;
            }

            RainbowFoldersWelcome.ShowWindow();
            EditorPrefs.SetBool(RainbowFoldersWelcome.PREF_KEY, true);

        }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private static void InitVcsDelegates(Assembly assembly)
        {
            try
            {
                _isVcsEnabled = () => Provider.isActive;

                var vcsHookType = assembly.GetType("UnityEditorInternal.VersionControl.ProjectHooks");
                var vcsHook = vcsHookType.GetMethod("OnProjectWindowItem", BindingFlags.Static | BindingFlags.Public);
                _drawVcsOverlay = (ProjectWindowItemCallback) Delegate.CreateDelegate(typeof(ProjectWindowItemCallback), vcsHook);
            }
            catch (SystemException ex)
            {
                if (!(ex is NullReferenceException) && !(ex is ArgumentNullException)) throw;
                _isVcsEnabled = () => false;

                #if RAINBOW_FOLDERS_DEVEL
                    Debug.LogException(ex);
                #endif
            }
        }

        private static void InitCollabDelegates(Assembly assembly)
        {
            try
            {
                var collabAccessType = assembly.GetType("UnityEditor.Web.CollabAccess");
                var collabAccessInstance = collabAccessType.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public).GetValue(null, null);
                var collabAccessMethod = collabAccessInstance.GetType().GetMethod("IsServiceEnabled", BindingFlags.Instance | BindingFlags.Public);
                _isCollabEnabled = (Func<bool>) Delegate.CreateDelegate(typeof(Func<bool>), collabAccessInstance, collabAccessMethod);

                var collabHookType = assembly.GetType("UnityEditor.Collaboration.CollabProjectHook");

                #if UNITY_2017_1_OR_NEWER
                    var collabHook = collabHookType.GetMethod("OnProjectWindowIconOverlay", BindingFlags.Static | BindingFlags.Public);
                    _drawCollabOverlay = (CollabItemCallback) Delegate.CreateDelegate(typeof(CollabItemCallback), collabHook);
                #else
                    var collabHook = collabHookType.GetMethod("OnProjectWindowItemIconOverlay", BindingFlags.Static | BindingFlags.Public);
                    _drawCollabOverlay = (ProjectWindowItemCallback) Delegate.CreateDelegate(typeof(ProjectWindowItemCallback), collabHook);
                #endif
            }
            catch (SystemException ex)
            {
                if (!(ex is NullReferenceException) && !(ex is ArgumentNullException)) throw;
                _isCollabEnabled = () => false;

                #if RAINBOW_FOLDERS_DEVEL
                    Debug.LogException(ex);
                #endif
            }
        }

        private static void ShowPopupWindow(Rect rect, string path)
        {
            var window = RainbowFoldersPopup.GetDraggableWindow();
            var position = GUIUtility.GUIToScreenPoint(rect.position + new Vector2(0, rect.height + 2));

            if (_multiSelection)
            {
                // ReSharper disable once RedundantTypeArgumentsOfMethod
                var paths = Selection.assetGUIDs
                    .Select<string, string>(AssetDatabase.GUIDToAssetPath)
                    .Where(AssetDatabase.IsValidFolder).ToList();

                var index = paths.IndexOf(path);
                window.ShowWithParams(position, paths, index);
            }
            else
            {
                window.ShowWithParams(position, new List<string> {path}, 0);
            }
        }

        private static void DrawCustomIcon(string guid, Rect rect, Texture texture, bool isSmall)
        {
            var iconRect = rect;
            if (iconRect.width > LARGE_ICON_SIZE)
            {
                // center the icon if it is zoomed
                var offset = (iconRect.width - LARGE_ICON_SIZE) / 2f;
                iconRect = new Rect(iconRect.x + offset, iconRect.y + offset, LARGE_ICON_SIZE, LARGE_ICON_SIZE);
            }
            else
            {
                // unity shifted small icons a bit in 5.5
                #if UNITY_5_5
                if (isSmall) rect = iconRect = new Rect(iconRect.x + 3, iconRect.y, iconRect.width, iconRect.height);
                #endif
            }

            if (_isCollabEnabled())
            {
                var background = RainbowFoldersEditorUtility.GetCollabBackground(isSmall, EditorGUIUtility.isProSkin);

                GUI.Box(rect, string.Empty, ItemBgStyle);
                GUI.DrawTexture(iconRect, background);
                GUI.DrawTexture(iconRect, texture);

                #if UNITY_2017_1_OR_NEWER
                    _drawCollabOverlay(rect, guid, isSmall);
                #else
                    _drawCollabOverlay(guid, rect);
                #endif

            }
            else if (_isVcsEnabled())
            {
                var background = RainbowFoldersEditorUtility.GetCollabBackground(isSmall, EditorGUIUtility.isProSkin);
                iconRect = (!isSmall) ? iconRect : new Rect(iconRect.x + 7, iconRect.y, iconRect.width, iconRect.height);

                GUI.Box(rect, string.Empty, ItemBgStyle);
                GUI.DrawTexture(iconRect, background);
                GUI.DrawTexture(iconRect, texture);
                _drawVcsOverlay(guid, rect);
            }
            else
            {
                GUI.DrawTexture(iconRect, texture);
            }
        }

        private static void DrawCustomBackground(Rect rect, Texture texture)
        {
            GUI.DrawTexture(rect, texture);
        }

        private static bool IsIconSmall(Rect rect)
        {
            return rect.width > rect.height;
        }

        private static Rect GetIconRect(Rect rect, bool isSmall)
        {
            if (isSmall)
                rect.width = rect.height;
            else
                rect.height = rect.width;

            return rect;
        }

        private static Rect GetBackgroundRect(Rect rect, bool isSmall)
        {
            if (isSmall)
            {
                rect.x += SMALL_ICON_SIZE + 1f;
                rect.width -= SMALL_ICON_SIZE + 1f;
            }
            else
            {
                rect.y += rect.width;
                rect.height -= rect.width;
            }

            return rect;
        }

        private static bool IsSelected(string guid)
        {
            return Selection.assetGUIDs.Contains(guid);
        }
    }
}
