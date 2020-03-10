using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EditorUtility = Borodar.RainbowFolders.Editor.RainbowFoldersEditorUtility;

namespace Borodar.RainbowFolders.Editor
{
    public class FolderTypesStorage : ScriptableObject
    {
        private const string RELATIVE_PATH = "Editor/Data/FolderTypesStorage.asset";

        public List<FolderType> TypeFolderIcons;

        //---------------------------------------------------------------------
        // Instance
        //---------------------------------------------------------------------

        private static FolderTypesStorage _instance;

        [SuppressMessage("ReSharper", "ConvertIfStatementToNullCoalescingExpression")]
        public static FolderTypesStorage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = EditorUtility.LoadFromAsset<FolderTypesStorage>(RELATIVE_PATH);

                return _instance;
            }
        }

        //---------------------------------------------------------------------
        // Public
        //---------------------------------------------------------------------

        public FolderIconPair GetIconsByType(FolderTypeName type)
        {
            var colorFolder = TypeFolderIcons.Single(x => x.Type == type);
            return new FolderIconPair { SmallIcon = colorFolder.SmallIcon, LargeIcon = colorFolder.LargeIcon };
        }
    }
}
