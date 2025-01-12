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
using System.Reflection;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using SgLib.Editor;

namespace EasyMobile.Editor
{
    public static class EM_EditorUtil
    {
        // Special constants for dedicated use in this class
        public const string AppleTangleClassName = "AppleTangle";
        public const string GooglePlayTangleClassName = "GooglePlayTangle";
        public const string ConstantsClassTemplate = "Template_Constants";
        public const string AppleTangleTemplate = "Template_AppleTangle";
        public const string GooglePlayTangleTemplate = "Template_GooglePlayTangle";
        public const string NamespaceStartPlaceholder = "__NameSpaceStart__";
        public const string NamespaceEndPlaceholder = "__NameSpaceEnd__";
        public const string ClassNamePlaceholder = "__Class__";
        public const string ClassTabPlaceholder = "__ClassTab__";
        public const string ConstantPlaceholder = "__Constant_Properties__";
        public const string IsDummyClassFieldName = "isDummyClass";

        /// <summary>
        /// Displays an editor dialog with the given title and message.
        /// </summary>
        /// <param name="title">the title.</param>
        /// <param name="message">the message.</param>
        public static void Alert(string title, string message)
        {
            EditorUtility.DisplayDialog(title, message, "OK");
        }

        /// <summary>
        /// Displays a modal dialog. ok and cancel are labels to be displayed on the dialog buttons. 
        /// If cancel is empty (the default), then only one button is displayed.
        /// </summary>
        /// <returns><c>true</c> if ok button is pressed., <c>false</c> otherwise.</returns>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="ok">Ok button label.</param>
        /// <param name="cancel">Cancel button label.</param>
        public static bool DisplayDialog(string title, string message, string ok, string cancel = "")
        {
            return EditorUtility.DisplayDialog(title, message, ok, cancel);
        }

        /// <summary>
        /// Gets the main prefab.
        /// </summary>
        /// <returns>The main prefab.</returns>
        public static GameObject GetMainPrefab()
        {
            // AssetDatabase always uses forward slashes in names and paths, even on Windows.
            return AssetDatabase.LoadAssetAtPath(EM_Constants.MainPrefabPath, typeof(GameObject)) as GameObject;
        }

        /// <summary>
        /// Adds the module to prefab.
        /// </summary>
        /// <param name="prefab">Prefab.</param>
        /// <typeparam name="Module">The 1st type parameter.</typeparam>
        public static void AddModuleToPrefab<Module>(GameObject prefab) where Module : MonoBehaviour
        {
            if (prefab != null)
            {
                Module comp = prefab.GetComponent<Module>();
                if (comp == null)
                {
                    prefab.AddComponent<Module>();
                }
            }
        }

        /// <summary>
        /// Removes the module from prefab.
        /// </summary>
        /// <param name="prefab">Prefab.</param>
        /// <typeparam name="Module">The 1st type parameter.</typeparam>
        public static void RemoveModuleFromPrefab<Module>(GameObject prefab) where Module: MonoBehaviour
        {
            if (prefab != null)
            {
                Module comp = prefab.GetComponent<Module>();
                if (comp != null)
                {
                    Component.DestroyImmediate(comp, true);
                }
            }
        }

        /// <summary>
        /// Use reflection to get all static members of the static GPGSIds class
        /// and then construct the list of ids from those members.
        /// </summary>
        /// <returns>The GPGS identifiers.</returns>
        public static Dictionary<string,string> GetGPGSIds()
        {
            Dictionary<string,string> dict = new Dictionary<string, string>();

            // Find the GPGSIds class and get all its public static members.
            string className = EM_Constants.AndroidGPGSConstantClassName;
            System.Type gpgsClass = FindClass(className);

            if (gpgsClass != null)
            {
                FieldInfo[] fields = gpgsClass.GetFields(BindingFlags.Static | BindingFlags.Public);

                foreach (var f in fields)
                {
                    dict.Add(f.Name, f.GetValue(null).ToString());
                }
            }

            return dict;
        }

        /// <summary>
        /// Use reflection to get all members of the GameServiceConstants class
        /// and then construct the list of names from those members.
        /// </summary>
        /// <returns>The game service constants.</returns>
        public static Dictionary<string,string> GetGameServiceConstants()
        {
            Dictionary<string,string> dict = new Dictionary<string, string>();

            // Find the GameServiceConstants class and get all its public static members.
            string className = EM_Constants.GameServiceConstantsClassName;
            System.Type constantsClass = FindClass(className);

            if (constantsClass != null)
            {
                FieldInfo[] fields = constantsClass.GetFields(BindingFlags.Static | BindingFlags.Public);

                foreach (var f in fields)
                {
                    dict.Add(f.Name, f.GetValue(null).ToString());
                }
            }

            return dict;
        }

        /// <summary>
        /// Use reflection to get all members of the IAPConstants class
        /// and then construct the list of names from those members.
        /// </summary>
        /// <returns>The game service constants.</returns>
        public static Dictionary<string,string> GetIAPConstants()
        {
            Dictionary<string,string> dict = new Dictionary<string, string>();

            // Find the GameServiceConstants class and get all its public static members.
            string className = EM_Constants.IAPConstantsClassName;
            System.Type constantsClass = FindClass(className);

            if (constantsClass != null)
            {
                FieldInfo[] fields = constantsClass.GetFields(BindingFlags.Static | BindingFlags.Public);

                foreach (var f in fields)
                {
                    dict.Add(f.Name, f.GetValue(null).ToString());
                }
            }

            return dict;
        }

        /// <summary>
        /// Determines if AppleTangle class exists.
        /// </summary>
        /// <returns><c>true</c>, if exists, <c>false</c> otherwise.</returns>
        public static bool AppleTangleClassExists()
        {
            System.Type appleTangle = FindClass(AppleTangleClassName, EM_ExternalPluginManager.UnityPurchasingSecurityNameSpace);

            return (appleTangle != null);
        }

        /// <summary>
        /// Determines if the AppleTangle class is not the dummy one we generated.
        /// </summary>
        /// <returns><c>true</c> if valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidAppleTangleClass()
        {
            System.Type appleTangle = FindClass(AppleTangleClassName, EM_ExternalPluginManager.UnityPurchasingSecurityNameSpace);
        
            if (appleTangle != null)
            {
                FieldInfo isDummyField = appleTangle.GetField(IsDummyClassFieldName, BindingFlags.Static | BindingFlags.NonPublic);

                if (isDummyField == null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines if GooglePlayTangle class exists.
        /// </summary>
        /// <returns><c>true</c>, if exists, <c>false</c> otherwise.</returns>
        public static bool GooglePlayTangleClassExists()
        {
            System.Type googlePlayTangle = FindClass(GooglePlayTangleClassName, EM_ExternalPluginManager.UnityPurchasingSecurityNameSpace);

            return (googlePlayTangle != null);
        }

        /// <summary>
        /// Determines if the GooglePlayTangle class is not the dummy one we generated.
        /// </summary>
        /// <returns><c>true</c> if valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidGooglePlayTangleClass()
        {
            System.Type googlePlayTangle = FindClass(GooglePlayTangleClassName, EM_ExternalPluginManager.UnityPurchasingSecurityNameSpace);
        
            if (googlePlayTangle != null)
            {
                FieldInfo isDummyField = googlePlayTangle.GetField(IsDummyClassFieldName, BindingFlags.Static | BindingFlags.NonPublic);

                if (isDummyField == null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the first class found with the specified class name and (optional) namespace and assembly name.
        /// Returns null if no class found.
        /// </summary>
        /// <returns>The class.</returns>
        /// <param name="className">Class name.</param>
        /// <param name="nameSpace">Optional namespace of the class to find.</param>
        /// <param name="assemblyName">Optional assembly name.</param>
        public static System.Type FindClass(string className, string nameSpace = null, string assemblyName = null)
        {
            Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly asm in assemblies)
            {
                // The assembly must match the given one if any.
                if (!string.IsNullOrEmpty(assemblyName) && !asm.GetName().Name.Equals(assemblyName))
                {
                    continue;
                }

                System.Type[] types = asm.GetTypes();
                foreach (System.Type t in types)
                {
                    // The namespace must match the given one if any. Note that the type may not have a namespace at all.
                    // Must be a class and of course class name must match the given one.
                    if (t.IsClass && t.Name.Equals(className) && (string.IsNullOrEmpty(nameSpace) || nameSpace.Equals(t.Namespace)))
                    {
                        return t;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Check if the given namespace exists within the current domain's assemblies.
        /// </summary>
        /// <returns><c>true</c>, if exists was namespaced, <c>false</c> otherwise.</returns>
        /// <param name="nameSpace">Name space.</param>
        /// <param name="assemblyName">Assembly name.</param>
        public static bool NamespaceExists(string nameSpace, string assemblyName = null)
        {
            Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly asm in assemblies)
            {
                // The assembly must match the given one if any.
                if (!string.IsNullOrEmpty(assemblyName) && !asm.GetName().Name.Equals(assemblyName))
                {
                    continue;
                }
                    
                System.Type[] types = asm.GetTypes();
                foreach (System.Type t in types)
                {
                    // The namespace must match the given one if any. Note that the type may not have a namespace at all.
                    // Must be a class and of course class name must match the given one.
                    if (!string.IsNullOrEmpty(t.Namespace) && t.Namespace.Equals(nameSpace))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Reads the editor template.
        /// </summary>
        /// <returns>The editor template contents.</returns>
        /// <param name="name">Name of the template in the editor directory.</param>
        public static string ReadTemplate(string name)
        {
            return FileIO.ReadFile(EM_Constants.TemplateFolder + "/" + name + ".txt");
        }

        /// <summary>
        /// Makes legal identifier from string.
        /// Returns a legal C# identifier from the given string.  The transformations are:
        ///   - spaces => underscore _
        ///   - punctuation => underscore _
        ///   - leading numbers are prefixed with underscore.
        ///   - characters other than letters or digits are left out.
        /// </summary>
        /// <returns>the id</returns>
        /// <param name="key">Key to convert to an identifier.</param>
        public static string MakeIdentifier(string key)
        {
            string s;
            string retId = string.Empty;

            if (string.IsNullOrEmpty(key))
            {
                return "_";
            }

            s = key.Trim().Replace(' ', '_');   // Spaces => Underscore _
            s = s.Replace('.', '_');    // Punctuations => Underscore _

            // Construct the identifier, ignoring all special characters like , + - * : /
            foreach (char c in s)
            {
                if (char.IsLetterOrDigit(c) || c == '_')
                {
                    retId += c;
                }
            }

            // Prefix leading numbers with underscore.
            if (char.IsDigit(retId[0]))
            {
                retId = '_' + retId;
            }

            return retId;
        }

        /// <summary>
        /// Generates a static C# class containing the string constants from the given resource keys.
        /// </summary>
        /// <param name="folder">The containing folder.</param>
        /// <param name="fullClassName">Class name with optional namespace separated by dots.</param>
        /// <param name="resourceKeys">Resource keys.</param>
        public static void GenerateConstantsClass(string destinationFolder, string fullClassName, Hashtable resourceKeys, bool alertWhenDone = false)
        {          
            string constantsValues = string.Empty;
            string[] parts = fullClassName.Split('.');
                    
            // If no folder is provided, use the default one and make sure it exists.
            if (string.IsNullOrEmpty(destinationFolder))
            {
                destinationFolder = EM_Constants.RootPath;
            }
            FileIO.EnsureFolderExists(destinationFolder);
        
            // Construct the namespace based on provided className.
            string nameSpace = string.Empty;
            for (int i = 0; i < parts.Length - 1; i++)
            {
                if (nameSpace != string.Empty)
                {
                    nameSpace += ".";
                }
        
                nameSpace += parts[i];
            }

            bool hasNameSpace = nameSpace != string.Empty;
            string classTab = hasNameSpace ? "\t" : string.Empty;
            string memberTab = hasNameSpace ? "\t\t" : "\t";
        
            foreach (DictionaryEntry entry in resourceKeys)
            {
                string key = MakeIdentifier((string)entry.Key);
                constantsValues += memberTab + "public const string " +
                key + " = \"" + entry.Value + "\";\n";
            }

            // First read the template.
            string fileBody = ReadTemplate(ConstantsClassTemplate);

            // Write the namespace start.
            if (hasNameSpace)
            {
                fileBody = fileBody.Replace(
                    NamespaceStartPlaceholder,
                    "namespace " + nameSpace + "\n{");
            }
            else
            {
                fileBody = fileBody.Replace(NamespaceStartPlaceholder, string.Empty);
            }

            // Fill in appropriate class tab.
            fileBody = fileBody.Replace(ClassTabPlaceholder, classTab);

            // Write the class name.
            string className = parts[parts.Length - 1];
            fileBody = fileBody.Replace(ClassNamePlaceholder, className);

            // Write the constants.
            fileBody = fileBody.Replace(ConstantPlaceholder, constantsValues);

            // Write the namespace end.
            if (hasNameSpace)
            {
                fileBody = fileBody.Replace(
                    NamespaceEndPlaceholder,
                    "}");
            }
            else
            {
                fileBody = fileBody.Replace(NamespaceEndPlaceholder, string.Empty);
            }
                
            // Write the file with the same name as the class.
            string filePath = destinationFolder + "/" + className + ".cs";
            FileIO.WriteFile(filePath, fileBody);
            AssetDatabase.ImportAsset(filePath);

            if (alertWhenDone)
            {
                Alert("Constants Class Generated", "Successfully created constants class at " + filePath);
            }
        }

        /// <summary>
        /// Generates the (dummy) AppleTangle class required for receipt validation code.
        /// </summary>
        /// <param name="alertWhenDone">If set to <c>true</c> alert when done.</param>
        public static void GenerateDummyAppleTangleClass(bool alertWhenDone = false)
        {
            GenerateClassFromTemplate(EM_Constants.ReceiptValidationFolder, AppleTangleClassName, AppleTangleTemplate, alertWhenDone);
        }

        /// <summary>
        /// Generates the (dummy) GooglePlayTangle class required for receipt validation code.
        /// </summary>
        /// <param name="alertWhenDone">If set to <c>true</c> alert when done.</param>
        public static void GenerateDummyGooglePlayTangleClass(bool alertWhenDone = false)
        {
            GenerateClassFromTemplate(EM_Constants.ReceiptValidationFolder, GooglePlayTangleClassName, GooglePlayTangleTemplate, alertWhenDone);
        }

        /// <summary>
        /// Generates a class from the given template.
        /// </summary>
        /// <param name="className">Class name.</param>
        /// <param name="templateName">Template name.</param>
        /// <param name="alertWhenDone">If set to <c>true</c> alert when done.</param>
        public static void GenerateClassFromTemplate(string destinationFolder, string className, string templateName, bool alertWhenDone = false)
        {
            // If no folder is provided, use the default one and make sure it exists.
            if (string.IsNullOrEmpty(destinationFolder))
            {
                destinationFolder = EM_Constants.RootPath;
            }

            // Make sure the containing folder exists.
            FileIO.EnsureFolderExists(destinationFolder);

            // Read the template.
            string fileBody = ReadTemplate(templateName);

            if (string.IsNullOrEmpty(fileBody))
            {
                Debug.LogError("Could not create class " + className + ". The template named " + templateName + " is not found or empty.");
                return;
            }

            // Just copy the whole template and write a new file with the same name as the class.
            string filePath = destinationFolder + "/" + className + ".cs";
            FileIO.WriteFile(filePath, fileBody);
            AssetDatabase.ImportAsset(filePath);

            if (alertWhenDone)
            {
                Alert("Class Generated", "Successfully created " + className + " class at " + filePath);
            }
        }

        /// <summary>
        /// Determines if a build target group is obsolete (has Obsolete attribute).
        /// </summary>
        /// <returns><c>true</c> if bsolete; otherwise, <c>false</c>.</returns>
        /// <param name="target">Target.</param>
        public static bool IsBuildTargetGroupObsolete(BuildTargetGroup target)
        {
            var type = target.GetType();
            var memberInfo = type.GetMember(target.ToString())[0];
            return System.Attribute.IsDefined(memberInfo, typeof(System.ObsoleteAttribute));
        }

        /// <summary>
        /// Finds the duplicate name in a serialized property that is an array.
        /// </summary>
        /// <returns>The duplicate name.</returns>
        /// <param name="property">Property.</param>
        /// <param name="elementNameProperty">Name property of array element.</param>
        public static string FindDuplicateNameInArrayProperty(SerializedProperty property, string elementNameProperty)
        {
            if (property.isArray)
            {
                HashSet<string> addedNames = new HashSet<string>();
                for (int i = 0; i < property.arraySize; i++)
                {
                    SerializedProperty el = property.GetArrayElementAtIndex(i);
                    string name = el.FindPropertyRelative(elementNameProperty).stringValue;

                    if (!string.IsNullOrEmpty(name))
                    {
                        if (addedNames.Contains(name))
                        {
                            return name;
                        }
                        else
                        {
                            addedNames.Add(name);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the number of scenes in Build Settings.
        /// </summary>
        /// <returns>The scene count in build settings.</returns>
        /// <param name="enabledScenesOnly">If set to <c>true</c> count enabled scenes only.</param>
        public static int GetSceneCountInBuildSettings(bool enabledScenesOnly = true)
        {
            int totalScenesInBuildSetting = EditorBuildSettings.scenes.Length;

            if (!enabledScenesOnly)
            {
                return totalScenesInBuildSetting;
            }
            else
            {
                int count = 0;
                for (int i = 0; i < totalScenesInBuildSetting; i++)
                {
                    count += EditorBuildSettings.scenes[i].enabled ? 1 : 0;
                }
                return count;
            }
        }

        /// <summary>
        /// Gets the paths of the scenes in Build Settings.
        /// </summary>
        /// <returns>The scene path in build settings.</returns>
        /// <param name="enabledScenesOnly">If set to <c>true</c> get enabled scenes only.</param>
        public static string[] GetScenePathInBuildSettings(bool enabledScenesOnly = true)
        {
            List<string> paths = new List<string>();
            for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                if (EditorBuildSettings.scenes[i].enabled || !enabledScenesOnly)
                {
                    paths.Add(EditorBuildSettings.scenes[i].path);
                }
            }
            return paths.ToArray();
        }

        /// <summary>
        /// Finds the game object with the specified name in scene. Note that this method
        /// only looks for root level objects.
        /// </summary>
        /// <returns>The first game object found in scene, or null if none was found.</returns>
        /// <param name="objectName">Object name.</param>
        /// <param name="scene">Scene.</param>
        public static GameObject FindGameObjectInScene(string objectName, Scene scene)
        {
            if (!scene.IsValid())
            {
                Debug.LogWarning("The given scene is invalid.");
                return null;
            }

            List<GameObject> roots = new List<GameObject>(scene.rootCount);
            scene.GetRootGameObjects(roots);
            GameObject go = roots.Find((GameObject g) =>
                {
                    return g.name.Equals(objectName);
                });

            return go;
        }

        /// <summary>
        /// Detemines if a root-level game object with the given name exists in the scenes specified by the scene paths.
        /// </summary>
        /// <returns><c>true</c> if a root-level game object is found; otherwise, <c>false</c>.</returns>
        /// <param name="objectName">Object name.</param>
        /// <param name="scenePaths">Scene paths.</param>
        public static bool IsGameObjectFoundInScenes(string objectName, string[] scenePaths)
        {
            bool isFound = false;   
            Scene activeScene = EditorSceneManager.GetActiveScene();

            for (int i = 0; i < scenePaths.Length; i++)
            {
                if (activeScene.path.Equals(scenePaths[i]))
                {
                    if (FindGameObjectInScene(objectName, activeScene))
                        isFound = true;
                }
                else
                {
                    Scene scene = EditorSceneManager.OpenScene(scenePaths[i], OpenSceneMode.Additive);

                    if (FindGameObjectInScene(objectName, scene))
                        isFound = true;
                    
                    EditorSceneManager.CloseScene(scene, true);
                }

                if (isFound)
                    break;
            }

            return isFound;
        }

        /// <summary>
        /// Finds the instance of the specified prefab in scene. Note that this methods only looks for
        /// root level instances.
        /// </summary>
        /// <returns>The first prefab instance found in scene, or null if none was found.</returns>
        /// <param name="prefab">Prefab.</param>
        /// <param name="scene">Scene.</param>
        public static GameObject FindPrefabInstanceInScene(GameObject prefab, Scene scene)
        {
            if (!scene.IsValid())
            {
                Debug.LogWarning("The given scene is invalid.");
                return null;
            }

            var prefabType = PrefabUtility.GetPrefabType(prefab);
            if (prefabType != PrefabType.Prefab && prefabType != PrefabType.ModelPrefab)
            {
                Debug.LogWarning("The provided object is not a valid prefab.");
                return null;
            }

            GameObject[] roots = scene.GetRootGameObjects();

            foreach (var obj in roots)
            {
                if (PrefabUtility.GetPrefabParent(obj) == prefab)
                {
                    var pType = PrefabUtility.GetPrefabType(obj);
                    if (pType == PrefabType.PrefabInstance || pType == PrefabType.ModelPrefabInstance)
                        return obj;
                }
            }

            return null;
        }

        /// <summary>
        /// Determines if a root-level instance of the given prefab found in the scenes specified by the scene paths.
        /// </summary>
        /// <returns><c>true</c> if a root-level prefab instance is found in the scenes; otherwise, <c>false</c>.</returns>
        /// <param name="prefab">Prefab.</param>
        /// <param name="scenePaths">Scene paths.</param>
        public static bool IsPrefabInstanceFoundInScenes(GameObject prefab, string[] scenePaths)
        {
            bool isFound = false;
            var prefabType = PrefabUtility.GetPrefabType(prefab);

            if (prefabType != PrefabType.Prefab && prefabType != PrefabType.ModelPrefab)
            {
                Debug.LogWarning("The provided object is not a valid prefab.");
                return isFound;
            }

            Scene activeScene = EditorSceneManager.GetActiveScene();

            for (int i = 0; i < scenePaths.Length; i++)
            {
                if (activeScene.path.Equals(scenePaths[i]))
                {
                    if (FindPrefabInstanceInScene(prefab, activeScene) != null)
                        isFound = true;
                }
                else
                {
                    Scene scene = EditorSceneManager.OpenScene(scenePaths[i], OpenSceneMode.Additive);

                    if (FindPrefabInstanceInScene(prefab, scene) != null)
                        isFound = true;

                    EditorSceneManager.CloseScene(scene, true);
                }

                if (isFound)
                    break;
            }

            return isFound;
        }

        /// <summary>
        /// Gets the associated key of the specified value in the given dictionary.
        /// </summary>
        /// <returns>The key associated with the value if found, otherwise default value of the type.</returns>
        /// <param name="dict">Dict.</param>
        /// <param name="val">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T GetKeyFromValue<T>(Dictionary<T, T> dict, T val)
        {
            foreach (KeyValuePair<T, T> entry in dict)
            {
                if (entry.Value.Equals(val))
                {
                    return entry.Key;
                }
            }

            return default(T);
        }
    }
}
