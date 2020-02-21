using System;
using UnityEngine;

namespace Borodar.RainbowFolders.Editor.Settings
{
    [Serializable]
    public class RainbowFolder
    {
        public KeyType Type;
        public string Key;

        public Texture2D SmallIcon;
        public Texture2D LargeIcon;
        public bool IsIconRecursive;
        public bool IsIconCustom;

        public Texture2D Background;
        public bool IsBackgroundRecursive;
        public bool IsBackgroundCustom;

        //---------------------------------------------------------------------
        // Ctors
        //---------------------------------------------------------------------

        public RainbowFolder(RainbowFolder value)
        {
            Type = value.Type;
            Key = value.Key;

            SmallIcon = value.SmallIcon;
            LargeIcon = value.LargeIcon;
            IsIconRecursive = value.IsIconRecursive;
            IsIconCustom = value.IsIconCustom;

            Background = value.Background;
            IsBackgroundRecursive = value.IsBackgroundRecursive;
            IsBackgroundCustom = value.IsBackgroundCustom;
        }

        public RainbowFolder(KeyType type, string key)
        {
            Type = type;
            Key = key;
        }

        public RainbowFolder(KeyType type, string key, FolderIconPair icons)
        {
            Type = type;
            Key = key;
            SmallIcon = icons.SmallIcon;
            LargeIcon = icons.LargeIcon;
        }

        public RainbowFolder(KeyType type, string key, Texture2D background)
        {
            Type = type;
            Key = key;
            Background = background;
        }

        //---------------------------------------------------------------------
        // Public
        //---------------------------------------------------------------------

        public void CopyFrom(RainbowFolder target)
        {
            Type = target.Type;
            Key = target.Key;

            SmallIcon = target.SmallIcon;
            LargeIcon = target.LargeIcon;
            IsIconRecursive = target.IsIconRecursive;
            IsIconCustom = target.IsIconCustom;

            Background = target.Background;
            IsBackgroundRecursive = target.IsBackgroundRecursive;
            IsBackgroundCustom = target.IsBackgroundCustom;
        }

        public bool HasAtLeastOneIcon()
        {
            return SmallIcon != null || LargeIcon != null || Background != null;
        }

        //---------------------------------------------------------------------
        // Nested
        //---------------------------------------------------------------------

        public enum KeyType
        {
            Name,
            Path
        }
    }
}