/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System.Collections;

namespace EasyMobile.Editor
{
    public static class EM_Constants
    {
        // Product name
        public const string ProductName = "Easy Mobile";
        public const string Copyright = "© 2017-2018 SgLib Games LLC. All Rights Reserved.";

        // Current version
        public const string versionString = "1.1.5p2";
        public const int versionInt = 0x01152;

        // Folder
        public const string RootPath = "Assets/EasyMobile";
        public const string EditorFolder = RootPath + "/Editor";
        public const string TemplateFolder = EditorFolder + "/Templates";
        public const string GeneratedFolder = RootPath + "/Generated";
        public const string MainPrefabFolder = RootPath;
        public const string MaterialsFolder = RootPath + "/Materials";
        public const string PackagesFolder = RootPath + "/Packages";
        public const string SkinFolder = RootPath + "/GUISkins";
        public const string SkinTextureFolder = SkinFolder + "/Textures";
        public const string ResourcesFolder = RootPath + "/Resources";
        public const string ScriptsFolder = RootPath + "/Scripts";
        public const string ReceiptValidationFolder = "Assets/Plugins/UnityPurchasing/generated";

        // Asset and stuff
        public const string SettingsAssetName = "EM_Settings";
        public const string SettingsAssetExtension = ".asset";
        public const string SettingsAssetPath = ResourcesFolder + "/EM_Settings.asset";
        public const string MainPrefabName = "EasyMobile";
        public const string PrefabExtension = ".prefab";
        public const string MainPrefabPath = MainPrefabFolder + "/EasyMobile.prefab";
        public const string PluginSettingsFilePath = RootPath + "/EasyMobileSettings.txt";
        public const string ClipPlayerMaterialPath = MaterialsFolder + "/ClipPlayerMat.mat";

        // UnityPackages
        public const string PlayServicersResolverPackagePath = PackagesFolder + "/play-services-resolver.unitypackage";
        public const string PlayMakerActionsPackagePath = PackagesFolder + "/PlayMakerActions.unitypackage";

        // Generated class names
        public const string RootNameSpace = "EasyMobile";
        public const string AndroidGPGSConstantClassName = "EM_GPGSIds";
        public const string GameServiceConstantsClassName = "EM_GameServiceConstants";
        public const string IAPConstantsClassName = "EM_IAPConstants";

        // URLs
        public const string DocumentationURL = "https://sglibgames.gitbooks.io/easy-mobile-user-guide/content/";
        public const string SupportEmail = "sglib.games@gmail.com";

        // Common symbols
        public const string NoneSymbol = "[None]";
        public const string DeleteSymbol = "-";
        public const string UpSymbol = "↑";
        public const string DownSymbol = "↓";

        // ProjectSettings keys
        public const string PSK_EMVersionString = "VERSION";
        public const string PSK_EMVersionInt = "VERSION_INT";
        public const string PSK_ImportedPlayServicesResolver = "IMPORTED_PLAY_SERVICES_RESOLVER";
    }
}

