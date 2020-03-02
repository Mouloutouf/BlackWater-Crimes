using Borodar.RainbowFolders.Editor.Settings;
using UnityEditor;
using UnityEngine;

namespace Borodar.RainbowFolders.Editor
{
    public static class RainbowFoldersIconsMenu
    {
        private const string MENU_COLORIZE = "Colors/";
        private const string MENU_TAG = "Tags/";
        private const string MENU_TYPE = "Types/";
        private const string MENU_PLATFORM = "Platforms/";
        private const string MENU_CUSTOM = "Custom";
        private const string MENU_NONE = "None";

        // Colors
        private static readonly GUIContent COLOR_RED = new GUIContent(MENU_COLORIZE + "Red");
        private static readonly GUIContent COLOR_VERMILION = new GUIContent(MENU_COLORIZE + "Vermilion");
        private static readonly GUIContent COLOR_ORANGE = new GUIContent(MENU_COLORIZE + "Orange");
        private static readonly GUIContent COLOR_AMBER = new GUIContent(MENU_COLORIZE + "Amber");
        private static readonly GUIContent COLOR_YELLOW = new GUIContent(MENU_COLORIZE + "Yellow");
        private static readonly GUIContent COLOR_LIME = new GUIContent(MENU_COLORIZE + "Lime");
        private static readonly GUIContent COLOR_CHARTREUSE = new GUIContent(MENU_COLORIZE + "Chartreuse");
        private static readonly GUIContent COLOR_HARLEQUIN = new GUIContent(MENU_COLORIZE + "Harlequin");
        private static readonly GUIContent COLOR_GREEN = new GUIContent(MENU_COLORIZE + "Green");
        private static readonly GUIContent COLOR_EMERALD = new GUIContent(MENU_COLORIZE + "Emerald");
        private static readonly GUIContent COLOR_SPRING_GREEN = new GUIContent(MENU_COLORIZE + "Spring-green");
        private static readonly GUIContent COLOR_AQUAMARINE = new GUIContent(MENU_COLORIZE + "Aquamarine");
        private static readonly GUIContent COLOR_CYAN = new GUIContent(MENU_COLORIZE + "Cyan");
        private static readonly GUIContent COLOR_SKY_BLUE = new GUIContent(MENU_COLORIZE + "Sky-blue");
        private static readonly GUIContent COLOR_AZURE = new GUIContent(MENU_COLORIZE + "Azure");
        private static readonly GUIContent COLOR_CERULEAN = new GUIContent(MENU_COLORIZE + "Cerulean");
        private static readonly GUIContent COLOR_BLUE = new GUIContent(MENU_COLORIZE + "Blue");
        private static readonly GUIContent COLOR_INDIGO = new GUIContent(MENU_COLORIZE + "Indigo");
        private static readonly GUIContent COLOR_VIOLET = new GUIContent(MENU_COLORIZE + "Violet");
        private static readonly GUIContent COLOR_PURPLE = new GUIContent(MENU_COLORIZE + "Purple");
        private static readonly GUIContent COLOR_MAGENTA = new GUIContent(MENU_COLORIZE + "Magenta");
        private static readonly GUIContent COLOR_FUCHSIA = new GUIContent(MENU_COLORIZE + "Fuchsia");
        private static readonly GUIContent COLOR_ROSE = new GUIContent(MENU_COLORIZE + "Rose");
        private static readonly GUIContent COLOR_CRIMSON = new GUIContent(MENU_COLORIZE + "Crimson");

        // Tags
        private static readonly GUIContent TAG_RED = new GUIContent(MENU_TAG + "Red");
        private static readonly GUIContent TAG_VERMILION = new GUIContent(MENU_TAG + "Vermilion");
        private static readonly GUIContent TAG_ORANGE = new GUIContent(MENU_TAG + "Orange");
        private static readonly GUIContent TAG_AMBER = new GUIContent(MENU_TAG + "Amber");
        private static readonly GUIContent TAG_YELLOW = new GUIContent(MENU_TAG + "Yellow");
        private static readonly GUIContent TAG_LIME = new GUIContent(MENU_TAG + "Lime");
        private static readonly GUIContent TAG_CHARTREUSE = new GUIContent(MENU_TAG + "Chartreuse");
        private static readonly GUIContent TAG_HARLEQUIN = new GUIContent(MENU_TAG + "Harlequin");
        private static readonly GUIContent TAG_GREEN = new GUIContent(MENU_TAG + "Green");
        private static readonly GUIContent TAG_EMERALD = new GUIContent(MENU_TAG + "Emerald");
        private static readonly GUIContent TAG_SPRING_GREEN = new GUIContent(MENU_TAG + "Spring-green");
        private static readonly GUIContent TAG_AQUAMARINE = new GUIContent(MENU_TAG + "Aquamarine");
        private static readonly GUIContent TAG_CYAN = new GUIContent(MENU_TAG + "Cyan");
        private static readonly GUIContent TAG_SKY_BLUE = new GUIContent(MENU_TAG + "Sky-blue");
        private static readonly GUIContent TAG_AZURE = new GUIContent(MENU_TAG + "Azure");
        private static readonly GUIContent TAG_CERULEAN = new GUIContent(MENU_TAG + "Cerulean");
        private static readonly GUIContent TAG_BLUE = new GUIContent(MENU_TAG + "Blue");
        private static readonly GUIContent TAG_INDIGO = new GUIContent(MENU_TAG + "Indigo");
        private static readonly GUIContent TAG_VIOLET = new GUIContent(MENU_TAG + "Violet");
        private static readonly GUIContent TAG_PURPLE = new GUIContent(MENU_TAG + "Purple");
        private static readonly GUIContent TAG_MAGENTA = new GUIContent(MENU_TAG + "Magenta");
        private static readonly GUIContent TAG_FUCHSIA = new GUIContent(MENU_TAG + "Fuchsia");
        private static readonly GUIContent TAG_ROSE = new GUIContent(MENU_TAG + "Rose");
        private static readonly GUIContent TAG_CRIMSON = new GUIContent(MENU_TAG + "Crimson");

        // Types
        private static readonly GUIContent TYPE_ANIMATIONS = new GUIContent(MENU_TYPE + "Animations");
        private static readonly GUIContent TYPE_AUDIO = new GUIContent(MENU_TYPE + "Audio");
        private static readonly GUIContent TYPE_PROJECT = new GUIContent(MENU_TYPE + "Project");
        private static readonly GUIContent TYPE_EDITOR = new GUIContent(MENU_TYPE + "Editor");
        private static readonly GUIContent TYPE_EXTENSIONS = new GUIContent(MENU_TYPE + "Extensions");
        private static readonly GUIContent TYPE_FLARES = new GUIContent(MENU_TYPE + "Flares");
        private static readonly GUIContent TYPE_FONTS = new GUIContent(MENU_TYPE + "Fonts");
        private static readonly GUIContent TYPE_MATERIALS = new GUIContent(MENU_TYPE + "Materials");
        private static readonly GUIContent TYPE_MESHES = new GUIContent(MENU_TYPE + "Meshes");
        private static readonly GUIContent TYPE_PHYSICS = new GUIContent(MENU_TYPE + "Physics");
        private static readonly GUIContent TYPE_PLUGINS = new GUIContent(MENU_TYPE + "Plugins");
        private static readonly GUIContent TYPE_PREFABS = new GUIContent(MENU_TYPE + "Prefabs");
        private static readonly GUIContent TYPE_RAINBOW = new GUIContent(MENU_TYPE + "Rainbow");
        private static readonly GUIContent TYPE_RESOURCES = new GUIContent(MENU_TYPE + "Resources");
        private static readonly GUIContent TYPE_SCENES = new GUIContent(MENU_TYPE + "Scenes");
        private static readonly GUIContent TYPE_SCRIPTS = new GUIContent(MENU_TYPE + "Scripts");
        private static readonly GUIContent TYPE_SHADERS = new GUIContent(MENU_TYPE + "Shaders");
        private static readonly GUIContent TYPE_TERRAINS = new GUIContent(MENU_TYPE + "Terrains");
        private static readonly GUIContent TYPE_TEXTURES = new GUIContent(MENU_TYPE + "Textures");

        // Platforms
        private static readonly GUIContent PLATFORM_ANDROID = new GUIContent(MENU_PLATFORM + "Android");
        private static readonly GUIContent PLATFORM_IOS = new GUIContent(MENU_PLATFORM + "iOS");
        private static readonly GUIContent PLATFORM_MAC = new GUIContent(MENU_PLATFORM + "Mac");
        private static readonly GUIContent PLATFORM_WEBGL = new GUIContent(MENU_PLATFORM + "WebGL");
        private static readonly GUIContent PLATFORM_WINDOWS = new GUIContent(MENU_PLATFORM + "Windows");

        // Custom
        private static readonly GUIContent SELECT_CUSTOM = new GUIContent(MENU_CUSTOM);
        // None
        private static readonly GUIContent SELECT_NONE = new GUIContent(MENU_NONE);

        //---------------------------------------------------------------------
        // Public
        //---------------------------------------------------------------------

        public static void ShowDropDown(Rect position, RainbowFolder folder)
        {
            var menu = new GenericMenu();

            // Colors
            menu.AddItem(COLOR_RED,           false, RedCallback,          folder);
            menu.AddItem(COLOR_VERMILION,     false, VermilionCallback,    folder);
            menu.AddItem(COLOR_ORANGE,        false, OrangeCallback,       folder);
            menu.AddItem(COLOR_AMBER,         false, AmberCallback,        folder);
            menu.AddItem(COLOR_YELLOW,        false, YellowCallback,       folder);
            menu.AddItem(COLOR_LIME,          false, LimeCallback,         folder);
            menu.AddItem(COLOR_CHARTREUSE,    false, ChartreuseCallback,   folder);
            menu.AddItem(COLOR_HARLEQUIN,     false, HarlequinCallback,    folder);
            menu.AddSeparator(MENU_COLORIZE);
            menu.AddItem(COLOR_GREEN,         false, GreenCallback,        folder);
            menu.AddItem(COLOR_EMERALD,       false, EmeraldCallback,      folder);
            menu.AddItem(COLOR_SPRING_GREEN,  false, SpringGreenCallback,  folder);
            menu.AddItem(COLOR_AQUAMARINE,    false, AquamarineCallback,   folder);
            menu.AddItem(COLOR_CYAN,          false, CyanCallback,         folder);
            menu.AddItem(COLOR_SKY_BLUE,      false, SkyBlueCallback,      folder);
            menu.AddItem(COLOR_AZURE,         false, AzureCallback,        folder);
            menu.AddItem(COLOR_CERULEAN,      false, CeruleanCallback,     folder);
            menu.AddSeparator(MENU_COLORIZE);
            menu.AddItem(COLOR_BLUE,          false, BlueCallback,         folder);
            menu.AddItem(COLOR_INDIGO,        false, IndigoCallback,       folder);
            menu.AddItem(COLOR_VIOLET,        false, VioletCallback,       folder);
            menu.AddItem(COLOR_PURPLE,        false, PurpleCallback,       folder);
            menu.AddItem(COLOR_MAGENTA,       false, MagentaCallback,      folder);
            menu.AddItem(COLOR_FUCHSIA,       false, FuchsiaCallback,      folder);
            menu.AddItem(COLOR_ROSE,          false, RoseCallback,         folder);
            menu.AddItem(COLOR_CRIMSON,       false, CrimsonCallback,      folder);

            // Tags
            menu.AddItem(TAG_RED,           false, TagRedCallback,          folder);
            menu.AddItem(TAG_VERMILION,     false, TagVermilionCallback,    folder);
            menu.AddItem(TAG_ORANGE,        false, TagOrangeCallback,       folder);
            menu.AddItem(TAG_AMBER,         false, TagAmberCallback,        folder);
            menu.AddItem(TAG_YELLOW,        false, TagYellowCallback,       folder);
            menu.AddItem(TAG_LIME,          false, TagLimeCallback,         folder);
            menu.AddItem(TAG_CHARTREUSE,    false, TagChartreuseCallback,   folder);
            menu.AddItem(TAG_HARLEQUIN,     false, TagHarlequinCallback,    folder);
            menu.AddSeparator(MENU_TAG);
            menu.AddItem(TAG_GREEN,         false, TagGreenCallback,        folder);
            menu.AddItem(TAG_EMERALD,       false, TagEmeraldCallback,      folder);
            menu.AddItem(TAG_SPRING_GREEN,  false, TagSpringGreenCallback,  folder);
            menu.AddItem(TAG_AQUAMARINE,    false, TagAquamarineCallback,   folder);
            menu.AddItem(TAG_CYAN,          false, TagCyanCallback,         folder);
            menu.AddItem(TAG_SKY_BLUE,      false, TagSkyBlueCallback,      folder);
            menu.AddItem(TAG_AZURE,         false, TagAzureCallback,        folder);
            menu.AddItem(TAG_CERULEAN,      false, TagCeruleanCallback,     folder);
            menu.AddSeparator(MENU_TAG);
            menu.AddItem(TAG_BLUE,          false, TagBlueCallback,         folder);
            menu.AddItem(TAG_INDIGO,        false, TagIndigoCallback,       folder);
            menu.AddItem(TAG_VIOLET,        false, TagVioletCallback,       folder);
            menu.AddItem(TAG_PURPLE,        false, TagPurpleCallback,       folder);
            menu.AddItem(TAG_MAGENTA,       false, TagMagentaCallback,      folder);
            menu.AddItem(TAG_FUCHSIA,       false, TagFuchsiaCallback,      folder);
            menu.AddItem(TAG_ROSE,          false, TagRoseCallback,         folder);
            menu.AddItem(TAG_CRIMSON,       false, TagCrimsonCallback,      folder);

            //Types
            menu.AddItem(TYPE_ANIMATIONS, false, AnimationsCallback, folder);
            menu.AddItem(TYPE_AUDIO,      false, AudioCallback,      folder);
            menu.AddItem(TYPE_EDITOR,     false, EditorCallback,     folder);
            menu.AddItem(TYPE_EXTENSIONS, false, ExtensionsCallback, folder);
            menu.AddItem(TYPE_FLARES,     false, FlaresCallback,     folder);
            menu.AddItem(TYPE_FONTS,      false, FontsCallback,      folder);
            menu.AddItem(TYPE_MATERIALS,  false, MaterialsCallback,  folder);
            menu.AddItem(TYPE_MESHES,     false, MeshesCallback,     folder);
            menu.AddItem(TYPE_PLUGINS,    false, PluginsCallback,    folder);
            menu.AddItem(TYPE_PHYSICS,    false, PhysicsCallback,    folder);
            menu.AddItem(TYPE_PREFABS,    false, PrefabsCallback,    folder);
            menu.AddItem(TYPE_PROJECT,    false, ProjectCallback,    folder);
            menu.AddItem(TYPE_RAINBOW,    false, RainbowCallback,    folder);
            menu.AddItem(TYPE_RESOURCES,  false, ResourcesCallback,  folder);
            menu.AddItem(TYPE_SCENES,     false, ScenesCallback,     folder);
            menu.AddItem(TYPE_SCRIPTS,    false, ScriptsCallback,    folder);
            menu.AddItem(TYPE_SHADERS,    false, ShadersCallback,    folder);
            menu.AddItem(TYPE_TERRAINS,   false, TerrainsCallback,   folder);
            menu.AddItem(TYPE_TEXTURES,   false, TexturesCallback,   folder);

            //Platfroms
            menu.AddItem(PLATFORM_ANDROID, false, AndroidCallback, folder);
            menu.AddItem(PLATFORM_IOS, false, IosCallback, folder);
            menu.AddItem(PLATFORM_MAC, false, MacCallback, folder);
            menu.AddItem(PLATFORM_WEBGL, false, WebGLCallback, folder);
            menu.AddItem(PLATFORM_WINDOWS, false, WindowsCallback, folder);

            // Separator
            menu.AddSeparator(string.Empty);
            // Custom
            menu.AddItem(SELECT_CUSTOM,  false, SelectCustomCallback, folder);
            // None
            menu.AddItem(SELECT_NONE,  false, SelectNoneCallback, folder);

            menu.DropDown(position);
        }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private static void Colorize(FolderColorName color, RainbowFolder folder)
        {
            var icons = FolderColorsStorage.Instance.GetIconsByColor(color);
            folder.SmallIcon = icons.SmallIcon;
            folder.LargeIcon = icons.LargeIcon;
            folder.IsIconCustom = false;
        }

        private static void AssignTag(FolderTagName tag, RainbowFolder folder)
        {
            var icons = FolderTagsStorage.Instance.GetIconsByTag(tag);
            folder.SmallIcon = icons.SmallIcon;
            folder.LargeIcon = icons.LargeIcon;
            folder.IsIconCustom = false;
        }

        private static void AssingType(FolderTypeName type, RainbowFolder folder)
        {
            var icons = FolderTypesStorage.Instance.GetIconsByType(type);
            folder.SmallIcon = icons.SmallIcon;
            folder.LargeIcon = icons.LargeIcon;
            folder.IsIconCustom = false;
        }

        private static void AssingPlatform(FolderPlatformName platform, RainbowFolder folder)
        {
            var icons = FolderPlatformsStorage.Instance.GetIconsByType(platform);
            folder.SmallIcon = icons.SmallIcon;
            folder.LargeIcon = icons.LargeIcon;
            folder.IsIconCustom = false;
        }

        private static void SelectCustom(RainbowFolder folder)
        {
            folder.IsIconCustom = true;
        }

        private static void SelectNone(RainbowFolder folder)
        {
            folder.SmallIcon = null;
            folder.LargeIcon = null;
            folder.IsIconRecursive = false;
            folder.IsIconCustom = false;
        }

        //---------------------------------------------------------------------
        // Callbacks
        //---------------------------------------------------------------------

        // Colors

        private static void RedCallback(object folder)
        { Colorize(FolderColorName.Red, folder as RainbowFolder); }

        private static void VermilionCallback(object folder)
        { Colorize(FolderColorName.Vermilion, folder as RainbowFolder); }

        private static void OrangeCallback(object folder)
        { Colorize(FolderColorName.Orange, folder as RainbowFolder); }

        private static void AmberCallback(object folder)
        { Colorize(FolderColorName.Amber, folder as RainbowFolder); }

        private static void YellowCallback(object folder)
        { Colorize(FolderColorName.Yellow, folder as RainbowFolder); }

        private static void LimeCallback(object folder)
        { Colorize(FolderColorName.Lime, folder as RainbowFolder); }

        private static void ChartreuseCallback(object folder)
        { Colorize(FolderColorName.Chartreuse, folder as RainbowFolder); }

        private static void HarlequinCallback(object folder)
        { Colorize(FolderColorName.Harlequin, folder as RainbowFolder); }

        private static void GreenCallback(object folder)
        { Colorize(FolderColorName.Green, folder as RainbowFolder); }

        private static void EmeraldCallback(object folder)
        { Colorize(FolderColorName.Emerald, folder as RainbowFolder); }

        private static void SpringGreenCallback(object folder)
        { Colorize(FolderColorName.SpringGreen, folder as RainbowFolder); }

        private static void AquamarineCallback(object folder)
        { Colorize(FolderColorName.Aquamarine, folder as RainbowFolder); }

        private static void CyanCallback(object folder)
        { Colorize(FolderColorName.Cyan, folder as RainbowFolder); }

        private static void SkyBlueCallback(object folder)
        { Colorize(FolderColorName.SkyBlue, folder as RainbowFolder); }

        private static void AzureCallback(object folder)
        { Colorize(FolderColorName.Azure, folder as RainbowFolder); }

        private static void CeruleanCallback(object folder)
        { Colorize(FolderColorName.Cerulean, folder as RainbowFolder); }

        private static void BlueCallback(object folder)
        { Colorize(FolderColorName.Blue, folder as RainbowFolder); }

        private static void IndigoCallback(object folder)
        { Colorize(FolderColorName.Indigo, folder as RainbowFolder); }

        private static void VioletCallback(object folder)
        { Colorize(FolderColorName.Violet, folder as RainbowFolder); }

        private static void PurpleCallback(object folder)
        { Colorize(FolderColorName.Purple, folder as RainbowFolder); }

        private static void MagentaCallback(object folder)
        { Colorize(FolderColorName.Magenta, folder as RainbowFolder); }

        private static void FuchsiaCallback(object folder)
        { Colorize(FolderColorName.Fuchsia, folder as RainbowFolder); }

        private static void RoseCallback(object folder)
        { Colorize(FolderColorName.Rose, folder as RainbowFolder); }

        private static void CrimsonCallback(object folder)
        { Colorize(FolderColorName.Crimson, folder as RainbowFolder); }

        // Tags

        private static void TagRedCallback(object folder)
        { AssignTag(FolderTagName.Red, folder as RainbowFolder); }

        private static void TagVermilionCallback(object folder)
        { AssignTag(FolderTagName.Vermilion, folder as RainbowFolder); }

        private static void TagOrangeCallback(object folder)
        { AssignTag(FolderTagName.Orange, folder as RainbowFolder); }

        private static void TagAmberCallback(object folder)
        { AssignTag(FolderTagName.Amber, folder as RainbowFolder); }

        private static void TagYellowCallback(object folder)
        { AssignTag(FolderTagName.Yellow, folder as RainbowFolder); }

        private static void TagLimeCallback(object folder)
        { AssignTag(FolderTagName.Lime, folder as RainbowFolder); }

        private static void TagChartreuseCallback(object folder)
        { AssignTag(FolderTagName.Chartreuse, folder as RainbowFolder); }

        private static void TagHarlequinCallback(object folder)
        { AssignTag(FolderTagName.Harlequin, folder as RainbowFolder); }

        private static void TagGreenCallback(object folder)
        { AssignTag(FolderTagName.Green, folder as RainbowFolder); }

        private static void TagEmeraldCallback(object folder)
        { AssignTag(FolderTagName.Emerald, folder as RainbowFolder); }

        private static void TagSpringGreenCallback(object folder)
        { AssignTag(FolderTagName.SpringGreen, folder as RainbowFolder); }

        private static void TagAquamarineCallback(object folder)
        { AssignTag(FolderTagName.Aquamarine, folder as RainbowFolder); }

        private static void TagCyanCallback(object folder)
        { AssignTag(FolderTagName.Cyan, folder as RainbowFolder); }

        private static void TagSkyBlueCallback(object folder)
        { AssignTag(FolderTagName.SkyBlue, folder as RainbowFolder); }

        private static void TagAzureCallback(object folder)
        { AssignTag(FolderTagName.Azure, folder as RainbowFolder); }

        private static void TagCeruleanCallback(object folder)
        { AssignTag(FolderTagName.Cerulean, folder as RainbowFolder); }

        private static void TagBlueCallback(object folder)
        { AssignTag(FolderTagName.Blue, folder as RainbowFolder); }

        private static void TagIndigoCallback(object folder)
        { AssignTag(FolderTagName.Indigo, folder as RainbowFolder); }

        private static void TagVioletCallback(object folder)
        { AssignTag(FolderTagName.Violet, folder as RainbowFolder); }

        private static void TagPurpleCallback(object folder)
        { AssignTag(FolderTagName.Purple, folder as RainbowFolder); }

        private static void TagMagentaCallback(object folder)
        { AssignTag(FolderTagName.Magenta, folder as RainbowFolder); }

        private static void TagFuchsiaCallback(object folder)
        { AssignTag(FolderTagName.Fuchsia, folder as RainbowFolder); }

        private static void TagRoseCallback(object folder)
        { AssignTag(FolderTagName.Rose, folder as RainbowFolder); }

        private static void TagCrimsonCallback(object folder)
        { AssignTag(FolderTagName.Crimson, folder as RainbowFolder); }

        // Types

        private static void AnimationsCallback(object folder)
        { AssingType(FolderTypeName.Animations, folder as RainbowFolder); }

        private static void PhysicsCallback(object folder)
        { AssingType(FolderTypeName.Physics, folder as RainbowFolder); }

        private static void PrefabsCallback(object folder)
        { AssingType(FolderTypeName.Prefabs, folder as RainbowFolder); }

        private static void ScenesCallback(object folder)
        { AssingType(FolderTypeName.Scenes, folder as RainbowFolder); }

        private static void ScriptsCallback(object folder)
        { AssingType(FolderTypeName.Scripts, folder as RainbowFolder); }

        private static void ExtensionsCallback(object folder)
        { AssingType(FolderTypeName.Extensions, folder as RainbowFolder); }

        private static void FlaresCallback(object folder)
        { AssingType(FolderTypeName.Flares, folder as RainbowFolder); }

        private static void PluginsCallback(object folder)
        { AssingType(FolderTypeName.Plugins, folder as RainbowFolder); }

        private static void TexturesCallback(object folder)
        { AssingType(FolderTypeName.Textures, folder as RainbowFolder); }

        private static void MaterialsCallback(object folder)
        { AssingType(FolderTypeName.Materials, folder as RainbowFolder); }

        private static void AudioCallback(object folder)
        { AssingType(FolderTypeName.Audio, folder as RainbowFolder); }

        private static void ProjectCallback(object folder)
        { AssingType(FolderTypeName.Project, folder as RainbowFolder); }

        private static void FontsCallback(object folder)
        { AssingType(FolderTypeName.Fonts, folder as RainbowFolder); }

        private static void EditorCallback(object folder)
        { AssingType(FolderTypeName.Editor, folder as RainbowFolder); }

        private static void ResourcesCallback(object folder)
        { AssingType(FolderTypeName.Resources, folder as RainbowFolder); }

        private static void ShadersCallback(object folder)
        { AssingType(FolderTypeName.Shaders, folder as RainbowFolder); }

        private static void TerrainsCallback(object folder)
        { AssingType(FolderTypeName.Terrains, folder as RainbowFolder); }

        private static void MeshesCallback(object folder)
        { AssingType(FolderTypeName.Meshes, folder as RainbowFolder); }

        private static void RainbowCallback(object folder)
        { AssingType(FolderTypeName.Rainbow, folder as RainbowFolder); }

        // Platfroms

        private static void AndroidCallback(object folder)
        { AssingPlatform(FolderPlatformName.Android, folder as RainbowFolder); }

        private static void IosCallback(object folder)
        { AssingPlatform(FolderPlatformName.iOS, folder as RainbowFolder); }

        private static void MacCallback(object folder)
        { AssingPlatform(FolderPlatformName.Mac, folder as RainbowFolder); }

        private static void WebGLCallback(object folder)
        { AssingPlatform(FolderPlatformName.WebGL, folder as RainbowFolder); }

        private static void WindowsCallback(object folder)
        { AssingPlatform(FolderPlatformName.Windows, folder as RainbowFolder); }

        // Custom
        private static void SelectCustomCallback(object folder)
        { SelectCustom(folder as RainbowFolder); }

        // None
        private static void SelectNoneCallback(object folder)
        { SelectNone(folder as RainbowFolder); }
    }
}