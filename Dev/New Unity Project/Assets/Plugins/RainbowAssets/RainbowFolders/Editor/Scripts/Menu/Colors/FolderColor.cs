using System;
using UnityEngine;

namespace Borodar.RainbowFolders.Editor
{
    [Serializable]
    public class FolderColor
    {
        public FolderColorName Color;
        public Texture2D SmallIcon;
        public Texture2D LargeIcon;
        public Texture2D Background;
    }
}