/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;
using UnityEditor.Graphs;

namespace EasyMobile.Editor
{
    public static class EM_MenuManager
    {

        #region Menu items

        [MenuItem("Window/" + EM_Constants.ProductName + "/Settings", false, 1)]
        public static void MenuOpenSettings()
        {
            EM_Settings instance = EM_Settings.LoadSettingsAsset();

            if (instance == null)
            {
                instance = EM_BuiltinObjectCreator.CreateEMSettingsAsset();
            }

            Selection.activeObject = instance;
        }

        [MenuItem("Window/" + EM_Constants.ProductName + "/Create EasyMobile.prefab", false, 2)]
        public static void MenuCreateMainPrefab()
        {
            EM_BuiltinObjectCreator.CreateEasyMobilePrefab(true);
            EM_Manager.CheckModules();
        }

        [MenuItem("Window/" + EM_Constants.ProductName + "/Import Play Services Resolver", false, 4)]
        public static void MenuReimportNativePackage()
        {
            EM_Manager.ImportPlayServicesResolver(true);
        }

        [MenuItem("Window/" + EM_Constants.ProductName + "/Install PlayMaker Actions", false, 3)]
        public static void MenuInstallPlayMakerActions()
        {
            EM_Manager.InstallPlayMakerActions(true);
        }

        [MenuItem("Window/" + EM_Constants.ProductName + "/Documentation", false, 5)]
        public static void OpenDocumentation()
        {
            Application.OpenURL(EM_Constants.DocumentationURL);
        }

        [MenuItem("Window/" + EM_Constants.ProductName + "/About", false, 6)]
        public static void About()
        {
            EditorWindow.GetWindow<EM_About>(true);
        }

        #endregion

        #region Context menu items

        [MenuItem("GameObject/" + EM_Constants.ProductName + "/EasyMobile Instance", false, 10)]
        public static void CreateEasyMobilePrefabInstance(MenuCommand menuCommand)
        {
            GameObject prefab = EM_EditorUtil.GetMainPrefab();
        
            if (prefab == null)
                prefab = EM_BuiltinObjectCreator.CreateEasyMobilePrefab();
        
            // Stop if another instance already exists as a root object in this scene
            GameObject existingInstance = EM_EditorUtil.FindPrefabInstanceInScene(prefab, EditorSceneManager.GetActiveScene());
            if (existingInstance != null)
            {
                Selection.activeObject = existingInstance;
                return;
            }
        
            // Instantiate an EasyMobile prefab at scene root (parentless) because it's a singleton
            GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            AddGameObjectToScene(go);
        }

        [MenuItem("GameObject/" + EM_Constants.ProductName + "/Clip Player", false, 10)]
        public static void CreateClipPlayer(MenuCommand menuCommand)
        {
            GameObject go = EM_BuiltinObjectCreator.CreateClipPlayer(menuCommand.context as GameObject);
            AddGameObjectToScene(go);
        }

        [MenuItem("GameObject/" + EM_Constants.ProductName + "/Clip Player (UI)", false, 10)]
        public static void CreateClipPlayerUI(MenuCommand menuCommand)
        {
            GameObject go = EM_BuiltinObjectCreator.CreateClipPlayerUI(menuCommand.context as GameObject);
            AddGameObjectToScene(go);
        }

        #endregion

        #region Private Stuff

        // Register undo action for the game object and make it active selection.
        private static void AddGameObjectToScene(GameObject go)
        {
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }

        #endregion
    }
}
