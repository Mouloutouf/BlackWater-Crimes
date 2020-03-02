using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EditorUtility = Borodar.RainbowFolders.Editor.RainbowFoldersEditorUtility;

namespace Borodar.RainbowFolders.Editor
{
    public class FolderColorsStorage : ScriptableObject
    {
        private const string RELATIVE_PATH = "Editor/Data/FolderColorsStorage.asset";

        public List<FolderColor> ColorFolderIcons;

        //---------------------------------------------------------------------
        // Instance
        //---------------------------------------------------------------------

        private static FolderColorsStorage _instance;

        [SuppressMessage("ReSharper", "ConvertIfStatementToNullCoalescingExpression")]
        public static FolderColorsStorage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = EditorUtility.LoadFromAsset<FolderColorsStorage>(RELATIVE_PATH);

                return _instance;
            }
        }

        //---------------------------------------------------------------------
        // Public
        //---------------------------------------------------------------------

        public FolderIconPair GetIconsByColor(FolderColorName color)
        {
            var colorFolder = ColorFolderIcons.Single(x => x.Color == color);
            return new FolderIconPair { SmallIcon = colorFolder.SmallIcon, LargeIcon = colorFolder.LargeIcon };
        }

        public Texture2D GetBackgroundByColor(FolderColorName color)
        {
            var colorFolder = ColorFolderIcons.Single(x => x.Color == color);
            return colorFolder.Background;
        }
    }
}
