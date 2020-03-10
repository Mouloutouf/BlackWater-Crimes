using Borodar.RainbowFolders.Editor.Settings;
using UnityEditor;
using UnityEngine;

namespace Borodar.RainbowFolders.Editor
{
    public static class RainbowFoldersBackgroundsMenu
    {
        private const string MENU_COLORIZE = "Colors/";
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
            // Separator
            menu.AddSeparator(string.Empty);
            // Custom
            menu.AddItem(SELECT_CUSTOM,  false, SelectCustomCallback, folder);
            // None
            menu.AddItem(SELECT_NONE,  false, SelectNoneCallback, folder);

            menu.DropDown(position);
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

        // Custom
        private static void SelectCustomCallback(object folder)
        { SelectCustom(folder as RainbowFolder); }

        // None
        private static void SelectNoneCallback(object folder)
        { SelectNone(folder as RainbowFolder); }

        //---------------------------------------------------------------------
        // Helpers
        //---------------------------------------------------------------------

        private static void Colorize(FolderColorName color, RainbowFolder folder)
        {
            folder.Background = FolderColorsStorage.Instance.GetBackgroundByColor(color);
            folder.IsBackgroundCustom = false;
        }

        private static void SelectCustom(RainbowFolder folder)
        {
            folder.IsBackgroundCustom = true;
        }

        private static void SelectNone(RainbowFolder folder)
        {
            folder.Background = null;
            folder.IsBackgroundRecursive = false;
            folder.IsBackgroundCustom = false;
        }
    }
}