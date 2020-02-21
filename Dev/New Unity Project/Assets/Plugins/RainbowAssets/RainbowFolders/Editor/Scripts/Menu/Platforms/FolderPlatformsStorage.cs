using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EditorUtility = Borodar.RainbowFolders.Editor.RainbowFoldersEditorUtility;

namespace Borodar.RainbowFolders.Editor
{
    public class FolderPlatformsStorage : ScriptableObject
    {
        private const string RELATIVE_PATH = "Editor/Data/FolderPlatformsStorage.asset";

        public List<FolderPlatform> PlatformFolderIcons;

        //---------------------------------------------------------------------
        // Instance
        //---------------------------------------------------------------------

        private static FolderPlatformsStorage _instance;

        [SuppressMessage("ReSharper", "ConvertIfStatementToNullCoalescingExpression")]
        public static FolderPlatformsStorage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = EditorUtility.LoadFromAsset<FolderPlatformsStorage>(RELATIVE_PATH);

                return _instance;
            }
        }

        //---------------------------------------------------------------------
        // Public
        //---------------------------------------------------------------------

        public FolderIconPair GetIconsByType(FolderPlatformName platform)
        {
            var colorFolder = PlatformFolderIcons.Single(x => x.Platform == platform);
            return new FolderIconPair { SmallIcon = colorFolder.SmallIcon, LargeIcon = colorFolder.LargeIcon };
        }
    }
}
