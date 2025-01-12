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
using System.Collections;

namespace EasyMobile.Editor
{
    // Partial editor class for In-App Purchasing module.
    public partial class EM_SettingsEditor
    {
        const string IAPModuleLabel = "IN-APP PURCHASING";
        const string IAPModuleIntro = "In-App Purchasing module leverages Unity IAP to help you quickly setup and sell digital goods in your game " +
                                      "and provide support for most popular app stores including the iOS App Store, Mac App Store, Google Play and Windows Store.";
        const string UnityIAPEnableInstruction = "Unity In-App Purchasing service is required. Please go to Window > Services to enable it.";
        const string IAPConstantGenerationIntro = "Generate the static class " + EM_Constants.RootNameSpace + "." + EM_Constants.IAPConstantsClassName + " that contains the constants of product names." +
                                                  " Remember to regenerate if you make changes to these names.";

        const string IAPProduct_NameProperty = "_name";
        const string IAPProduct_TypeProperty = "_type";
        const string IAPProduct_IdProperty = "_id";
        const string IAPProduct_PriceProperty = "_price";
        const string IAPProduct_DescriptionProperty = "_description";
        const string IAPProduct_StoreSpecificIdsProperty = "_storeSpecificIds";
        const string StoreSpecificIds_StoreProperty = "store";
        const string StoreSpecificIds_IdProperty = "id";
        const string IAPProduct_IsEditingAdvanced = "_isEditingAdvanced";

        GUIContent IAPProduct_NameContent = new GUIContent("Name", "Product name can be used when making purchases");
        GUIContent IAPProduct_TypeContent = new GUIContent("Type", "Product type");
        GUIContent IAPProduct_IdContent = new GUIContent("Id", "Unified product Id, this Id will be used for stores that don't have a specific Id provided in Store Specific Ids array");
        GUIContent IAPProduct_PriceContent = new GUIContent("Price", "Product price string for displaying purpose");
        GUIContent IAPProduct_DescriptionContent = new GUIContent("Description", "Product description for displaying purpose");
        GUIContent IAPProduct_StoreSpecificIdsContent = new GUIContent("Store-Specific Ids", "Product Id that is specific to a certain store (and will override the unified Id for that store)");

        #if EM_UIAP
        static bool isIAPProductsFoldout = false;
        #endif

        void IAPModuleGUI()
        {
            EditorGUILayout.BeginVertical(EM_GUIStyleManager.GetCustomStyle("Module Box"));

            EditorGUI.BeginChangeCheck();
            isIAPModuleEnable.boolValue = EM_EditorGUI.ModuleToggle(isIAPModuleEnable.boolValue, IAPModuleLabel);
            if (EditorGUI.EndChangeCheck())
            {
                GameObject prefab = EM_EditorUtil.GetMainPrefab();

                if (!isIAPModuleEnable.boolValue)
                {
                    EM_Manager.DisableIAPModule(prefab);
                }
                else
                {
                    EM_Manager.EnableIAPModule(prefab);
                }
            }

            // Now draw the GUI.
            if (!isIAPModuleEnable.boolValue)
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox(IAPModuleIntro, MessageType.Info);
            }
            else
            {
                #if !EM_UIAP
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox(UnityIAPEnableInstruction, MessageType.Error);
                #else
                // Select target Android store, like using the Window > Unity IAP > Android > Target ... menu item.
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("[ANDROID] TARGET STORE", EditorStyles.boldLabel);

                EditorGUI.BeginChangeCheck();
                IAPProperties.targetAndroidStore.property.enumValueIndex = EditorGUILayout.Popup(
                    IAPProperties.targetAndroidStore.content.text, 
                    IAPProperties.targetAndroidStore.property.enumValueIndex,
                    IAPProperties.targetAndroidStore.property.enumDisplayNames
                );
                if (EditorGUI.EndChangeCheck())
                {
                    SetTargetAndroidStore((IAPAndroidStore)IAPProperties.targetAndroidStore.property.enumValueIndex);
                }
                    
                // Receipt validation
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("RECEIPT VALIDATION", EditorStyles.boldLabel);
                EditorGUILayout.HelpBox("Unity IAP offers local receipt validation for extra security. Apple stores and Google Play store only.", MessageType.None);

                // iOS store.
                EditorGUI.BeginDisabledGroup(!isAppleTangleValid);
                IAPProperties.validateAppleReceipt.property.boolValue = EditorGUILayout.Toggle(IAPProperties.validateAppleReceipt.content, IAPProperties.validateAppleReceipt.property.boolValue);
                EditorGUI.EndDisabledGroup();

                // Always disable the option if AppleTangle is not valid.
                if (!isAppleTangleValid)
                {
                    IAPProperties.validateAppleReceipt.property.boolValue = false;
                }

                // Google Play store.
                bool isTargetingGooglePlay = IAPProperties.targetAndroidStore.property.enumValueIndex == (int)IAPAndroidStore.GooglePlay;
                EditorGUI.BeginDisabledGroup(!isGooglePlayTangleValid);
                IAPProperties.validateGooglePlayReceipt.property.boolValue = EditorGUILayout.Toggle(IAPProperties.validateGooglePlayReceipt.content, IAPProperties.validateGooglePlayReceipt.property.boolValue);
                EditorGUI.EndDisabledGroup();

                // Always disable the option if GooglePlayTangle is not valid.
                if (!isGooglePlayTangleValid)
                {
                    IAPProperties.validateGooglePlayReceipt.property.boolValue = false;
                }

                if (!isAppleTangleValid || (!isGooglePlayTangleValid && isTargetingGooglePlay))
                {
                    string rvMsg = "Please go to Window > Unity IAP > IAP Receipt Validation Obfuscator and create obfuscated secrets to enable receipt validation for Apple stores and Google Play store.";                          

                    if (!isAppleTangleValid)
                    {
                        rvMsg += " Note that you don't need to provide a Google Play public key if you're only targeting Apple stores.";
                    }
                    else
                    {
                        rvMsg = rvMsg.Replace("Apple stores and ", "");
                    }

                    if (isGooglePlayTangleValid || !isTargetingGooglePlay)
                    {
                        rvMsg = rvMsg.Replace(" and Google Play store", "");
                    }

                    EditorGUILayout.HelpBox(rvMsg, MessageType.Warning);
                }
                    
                // Product list
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("PRODUCTS", EditorStyles.boldLabel);

                EMProperty products = IAPProperties.products;

                if (products.property.arraySize > 0)
                {
                    EditorGUI.indentLevel++;
                    isIAPProductsFoldout = EditorGUILayout.Foldout(isIAPProductsFoldout, products.property.arraySize + " " + products.content.text);
                    EditorGUI.indentLevel--;

                    if (isIAPProductsFoldout)
                    {
                        // Index of the element on which buttons are clicked.
                        int deleteIndex = -1;
                        int moveUpIndex = -1;
                        int moveDownIndex = -1;

                        for (int i = 0; i < products.property.arraySize; i++)
                        {
                            SerializedProperty element = products.property.GetArrayElementAtIndex(i);
                            bool isDeleted = false;
                            bool isMovedUp = false;
                            bool isMovedDown = false;

                            EditorGUILayout.Space();
                            DrawIAPProduct(
                                element,
                                ref isDeleted,
                                ref isMovedUp,
                                ref isMovedDown,
                                i > 0,
                                i < products.property.arraySize - 1
                            );

                            if (isDeleted)
                                deleteIndex = i;
                            if (isMovedUp)
                                moveUpIndex = i;
                            if (isMovedDown)
                                moveDownIndex = i;
                        }

                        // Delete.
                        if (deleteIndex >= 0)
                        {
                            products.property.DeleteArrayElementAtIndex(deleteIndex);
                        }

                        // Move up.
                        if (moveUpIndex > 0)
                        {
                            products.property.MoveArrayElement(moveUpIndex, moveUpIndex - 1);
                        }

                        // Move down.
                        if (moveDownIndex >= 0 && moveDownIndex < products.property.arraySize - 1)
                        {
                            products.property.MoveArrayElement(moveDownIndex, moveDownIndex + 1);
                        }

                        // Detect duplicate product names.
                        string duplicateName = EM_EditorUtil.FindDuplicateNameInArrayProperty(products.property, IAPProduct_NameProperty);
                        if (!string.IsNullOrEmpty(duplicateName))
                        {
                            EditorGUILayout.Space();
                            EditorGUILayout.HelpBox("Found duplicate name of \"" + duplicateName + "\".", MessageType.Warning);
                        }
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("No products added.", MessageType.None);
                }

                EditorGUILayout.Space();
                if (GUILayout.Button("Add New Product", GUILayout.Height(EM_GUIStyleManager.buttonHeight)))
                {
                    // Add new IAP product.
                    AddNewProduct(products.property);

                    // Open the foldout if it's closed.
                    isIAPProductsFoldout = true;
                }

                // Constant generation.
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("CONSTANTS CLASS GENERATION", EditorStyles.boldLabel);
                EditorGUILayout.HelpBox(IAPConstantGenerationIntro, MessageType.None);

                EditorGUILayout.Space();
                if (GUILayout.Button("Generate Constants Class", GUILayout.Height(EM_GUIStyleManager.buttonHeight)))
                {
                    GenerateIAPConstants();
                }
                #endif
            }

            EditorGUILayout.EndVertical();
        }

        #if EM_UIAP
        void SetTargetAndroidStore(IAPAndroidStore store)
        { 
            UnityEngine.Purchasing.AppStore androidStore = IAPManager.GetAppStore(store);
            UnityEditor.Purchasing.UnityPurchasingEditor.TargetAndroidStore(androidStore); 
        }
        #endif

        // Generate a static class containing constants of IAP product names.
        void GenerateIAPConstants()
        {           
            // First create a hashtable containing all the names to be stored as constants.
            SerializedProperty productsProp = IAPProperties.products.property;

            // First check if there're duplicate names.
            string duplicateLdbName = EM_EditorUtil.FindDuplicateNameInArrayProperty(productsProp, IAPProduct_NameProperty);
            if (!string.IsNullOrEmpty(duplicateLdbName))
            {
                EM_EditorUtil.Alert("Error: Duplicate Names", "Found duplicate product name of \"" + duplicateLdbName + "\".");
                return;
            }

            // Proceed with adding resource keys.
            Hashtable resourceKeys = new Hashtable();

            // Add the product names.
            for (int i = 0; i < productsProp.arraySize; i++)
            {
                SerializedProperty element = productsProp.GetArrayElementAtIndex(i);
                string name = element.FindPropertyRelative(IAPProduct_NameProperty).stringValue;

                // Ignore all items with an empty name.
                if (!string.IsNullOrEmpty(name))
                {
                    string key = "Product_" + name;
                    resourceKeys.Add(key, name);
                }
            }

            if (resourceKeys.Count > 0)
            {
                // Now build the class.
                EM_EditorUtil.GenerateConstantsClass(
                    EM_Constants.GeneratedFolder,
                    EM_Constants.RootNameSpace + "." + EM_Constants.IAPConstantsClassName,
                    resourceKeys,
                    true
                );
            }
            else
            {
                EM_EditorUtil.Alert("Constants Class Generation", "Please fill in required information for all products.");
            }
        }

        // Draw leaderboard or achievement item.
        void DrawIAPProduct(SerializedProperty property, ref bool isDeleteButton, ref bool isMoveUpButton, ref bool isMoveDownButton, bool allowMoveUp = true, bool allowMoveDown = true)
        {
            // Get members.
            SerializedProperty name = property.FindPropertyRelative(IAPProduct_NameProperty);
            SerializedProperty type = property.FindPropertyRelative(IAPProduct_TypeProperty);
            SerializedProperty id = property.FindPropertyRelative(IAPProduct_IdProperty);
            SerializedProperty price = property.FindPropertyRelative(IAPProduct_PriceProperty);
            SerializedProperty description = property.FindPropertyRelative(IAPProduct_DescriptionProperty);
            SerializedProperty storeSpecificIds = property.FindPropertyRelative(IAPProduct_StoreSpecificIdsProperty);
            SerializedProperty isEditingAdvanced = property.FindPropertyRelative(IAPProduct_IsEditingAdvanced);

            EditorGUILayout.BeginHorizontal();

            // Main content section.
            EditorGUILayout.BeginVertical(EM_GUIStyleManager.GetCustomStyle("Item Box"));

            // Required settings
            EditorGUILayout.LabelField(string.IsNullOrEmpty(name.stringValue) ? "New Product" : name.stringValue, EditorStyles.boldLabel);
            name.stringValue = EditorGUILayout.TextField(IAPProduct_NameContent, name.stringValue);
            type.enumValueIndex = EditorGUILayout.Popup(IAPProduct_TypeContent.text, type.enumValueIndex, type.enumDisplayNames);
            id.stringValue = EditorGUILayout.TextField(IAPProduct_IdContent, id.stringValue);

            // Advanced settings
            EditorGUI.indentLevel++;
            isEditingAdvanced.boolValue = EditorGUILayout.Foldout(isEditingAdvanced.boolValue, "More (Optional)");

            if (isEditingAdvanced.boolValue)
            {
                price.stringValue = EditorGUILayout.TextField(IAPProduct_PriceContent, price.stringValue);
                description.stringValue = EditorGUILayout.TextField(IAPProduct_DescriptionContent, description.stringValue, EM_GUIStyleManager.WordwrapTextField, GUILayout.Height(EditorGUIUtility.singleLineHeight * 3));
                // Store-specific Ids section.
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(IAPProduct_StoreSpecificIdsContent);
                EditorGUI.BeginChangeCheck();
                storeSpecificIds.arraySize = EditorGUILayout.IntField(storeSpecificIds.arraySize, GUILayout.Width(50));
                if (EditorGUI.EndChangeCheck())
                {
                    // Won't allow values larger than the number of available stores.
                    int storeNum = System.Enum.GetNames(typeof(IAPStore)).Length;
                    if (storeSpecificIds.arraySize > storeNum)
                    {
                        storeSpecificIds.arraySize = storeNum;
                    }
                }
                EditorGUILayout.EndHorizontal();

                for (int i = 0; i < storeSpecificIds.arraySize; i++)
                {
                    SerializedProperty element = storeSpecificIds.GetArrayElementAtIndex(i);
                    SerializedProperty specificStore = element.FindPropertyRelative(StoreSpecificIds_StoreProperty);
                    SerializedProperty specificId = element.FindPropertyRelative(StoreSpecificIds_IdProperty);

                    EditorGUILayout.BeginHorizontal();
                    specificStore.enumValueIndex = EditorGUILayout.Popup(specificStore.enumValueIndex, specificStore.enumDisplayNames);
                    specificId.stringValue = EditorGUILayout.TextField(specificId.stringValue);

                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUI.indentLevel--;
                
            EditorGUILayout.EndVertical();

            // The toolbox section.
            EditorGUILayout.BeginVertical(EM_GUIStyleManager.GetCustomStyle("Tool Box"), GUILayout.Width(EM_GUIStyleManager.toolboxWidth), GUILayout.Height(EM_GUIStyleManager.toolboxHeight));

            // Move up button.
            EditorGUI.BeginDisabledGroup(!allowMoveUp);
            if (GUILayout.Button(EM_Constants.UpSymbol, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
            {
                isMoveUpButton = true;
            }
            EditorGUI.EndDisabledGroup();

            // Delete button.
            if (GUILayout.Button(EM_Constants.DeleteSymbol, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
            {
                // DeleteArrayElementAtIndex seems working fine even while iterating
                // through the array, but it's still a better idea to move it outside the loop.
                isDeleteButton = true;
            }

            // Move down button.
            EditorGUI.BeginDisabledGroup(!allowMoveDown);
            if (GUILayout.Button(EM_Constants.DownSymbol, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
            {
                isMoveDownButton = true;
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        void AddNewProduct(SerializedProperty productsArrayProp)
        {
            if (productsArrayProp.isArray)
            {
                productsArrayProp.InsertArrayElementAtIndex(productsArrayProp.arraySize);
        
                // Init values.
                SerializedProperty newProp = productsArrayProp.GetArrayElementAtIndex(productsArrayProp.arraySize - 1);
                SerializedProperty name = newProp.FindPropertyRelative(IAPProduct_NameProperty);
                SerializedProperty type = newProp.FindPropertyRelative(IAPProduct_TypeProperty);
                SerializedProperty id = newProp.FindPropertyRelative(IAPProduct_IdProperty);
                SerializedProperty storeSpecificIds = newProp.FindPropertyRelative(IAPProduct_StoreSpecificIdsProperty);

                name.stringValue = string.Empty;
                id.stringValue = string.Empty;
                type.enumValueIndex = (int)IAPProductType.Consumable;
                storeSpecificIds.ClearArray();
            }
        }
    }
}

