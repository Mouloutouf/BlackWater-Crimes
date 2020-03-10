using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EditorUtility = Borodar.RainbowFolders.Editor.RainbowFoldersEditorUtility;

namespace Borodar.RainbowFolders.Editor
{
    public class FolderTagsStorage : ScriptableObject
    {
        private const string RELATIVE_PATH = "Editor/Data/FolderTagsStorage.asset";

        public List<FolderTag> ColorFolderTags;

        //---------------------------------------------------------------------
        // Instance
        //---------------------------------------------------------------------

        private static FolderTagsStorage _instance;

        [SuppressMessage("ReSharper", "ConvertIfStatementToNullCoalescingExpression")]
        public static FolderTagsStorage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = EditorUtility.LoadFromAsset<FolderTagsStorage>(RELATIVE_PATH);

                return _instance;
            }
        }

        //---------------------------------------------------------------------
        // Public
        //---------------------------------------------------------------------

        public FolderIconPair GetIconsByTag(FolderTagName tag)
        {
            var taggedFolder = ColorFolderTags.Single(x => x.Tag == tag);
            return new FolderIconPair { SmallIcon = taggedFolder.SmallIcon, LargeIcon = taggedFolder.LargeIcon };
        }
    }
}
