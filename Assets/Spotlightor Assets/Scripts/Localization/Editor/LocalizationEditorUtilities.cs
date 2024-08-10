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
using System.Collections.Generic;

public class LocalizationEditorUtilities : ScriptableObject
{
	private const string LocalizationAssetsDefaultPath = "Assets";

	[MenuItem ("Spotlightor/Localization/Switch Language/Chinese")]
	public static void SwitchLanguageChinese ()
	{
		SwitchLanguage (LocalizationLanguageTypes.Chinese);
	}

	[MenuItem ("Spotlightor/Localization/Switch Language/English")]
	public static void SwitchLanguageEnglish ()
	{
		SwitchLanguage (LocalizationLanguageTypes.English);
	}

	[MenuItem ("Spotlightor/Localization/Switch Language/French")]
	public static void SwitchLanguageFrench ()
	{
		SwitchLanguage (LocalizationLanguageTypes.French);
	}

	[MenuItem ("Spotlightor/Localization/Switch Language/German")]
	public static void SwitchLanguageGerman ()
	{
		SwitchLanguage (LocalizationLanguageTypes.German);
	}

	[MenuItem ("Spotlightor/Localization/Switch Language/Italian")]
	public static void SwitchLanguageItalian ()
	{
		SwitchLanguage (LocalizationLanguageTypes.Italian);
	}

	[MenuItem ("Spotlightor/Localization/Switch Language/Spanish")]
	public static void SwitchLanguageSpanish ()
	{
		SwitchLanguage (LocalizationLanguageTypes.Spanish);
	}

	[MenuItem ("Spotlightor/Localization/Switch Language/Japanese")]
	public static void SwitchLanguageJapanese ()
	{
		SwitchLanguage (LocalizationLanguageTypes.Japanese);
	}

	[MenuItem ("Spotlightor/Localization/Switch Language/Russian")]
	public static void SwitchLanguageRussian ()
	{
		SwitchLanguage (LocalizationLanguageTypes.Russian);
	}

	[MenuItem ("Spotlightor/Localization/Switch Language/Chinese Traditional")]
	public static void SwitchLanguageChsTraditional ()
	{
		SwitchLanguage (LocalizationLanguageTypes.ChsTraditional);
	}

	public static void SwitchLanguage (LocalizationLanguageTypes language)
	{
		Localization.CurrentLanguage = language;
		Localization.SaveLanguagePreference ();
		if (SaveSlotsStorage.ActiveSaveSlot != null)
			SaveSlotsStorage.ActiveSaveSlot.Save ();
	}

	[MenuItem ("Spotlightor/Localization/Create Localization Asset")]
	public static void CreateLocalizationAsset ()
	{
		Object data = ScriptableObject.CreateInstance<LocalizationTextsAsset> ();
		string path = LocalizationAssetsDefaultPath + "/" + "localization.asset";
		AssetDatabase.CreateAsset (data, path);
		Selection.activeObject = AssetDatabase.LoadAssetAtPath (path, typeof(LocalizationTextsAsset));
	}

	[MenuItem ("Spotlightor/Localization/Create Localization Setting Asset")]
	public static void CreateLocalizationSettingsAsset ()
	{
		Object data = ScriptableObject.CreateInstance<LocalizationSettings> ();
		string path = LocalizationAssetsDefaultPath + "/" + "localization_settings.asset";
		AssetDatabase.CreateAsset (data, path);
		Selection.activeObject = AssetDatabase.LoadAssetAtPath (path, typeof(LocalizationSettings));
	}

	[MenuItem ("Spotlightor/Localization/Update fonts for ALL")]
	public static void UpdateFontCharactersForAll ()
	{
		List<TrueTypeFontImporter> modifiedFontImporters = new List<TrueTypeFontImporter> ();
		List<LocalizationTextsAsset> assets = LoadAllLocalizationTextsAssets ();
		foreach (LocalizationTextsAsset asset in assets) {
			if (asset.fonts != null && asset.fonts.Length > 0) {
				string assetAllText = asset.GetAllTexts ();
				foreach (Font assetFont in asset.fonts) {
					TrueTypeFontImporter fontImporter = TrueTypeFontImporter.GetAtPath (AssetDatabase.GetAssetPath (assetFont)) as TrueTypeFontImporter;
					if (modifiedFontImporters.Contains (fontImporter) == false) {
						fontImporter.customCharacters = "";
						modifiedFontImporters.Add (fontImporter);
					}

					for (int i = 0; i < assetAllText.Length; i++) {
						if (fontImporter.customCharacters.IndexOf (assetAllText [i]) == -1)
							fontImporter.customCharacters += assetAllText [i];
					}
				}
			}
			if (asset.fontOfLanguages != null && asset.fontOfLanguages.Length > 0) {
				foreach (LocalizationTextsAsset.FontOfLanguage fontOfLanguage in asset.fontOfLanguages) {
					string textsOfLanguage = asset.GetAllTextsByLanguage (fontOfLanguage.language);
					foreach (Font assetFont in fontOfLanguage.fonts) {
						TrueTypeFontImporter fontImporter = TrueTypeFontImporter.GetAtPath (AssetDatabase.GetAssetPath (assetFont)) as TrueTypeFontImporter;
						if (modifiedFontImporters.Contains (fontImporter) == false) {
							fontImporter.customCharacters = "";
							modifiedFontImporters.Add (fontImporter);
						}
						
						for (int i = 0; i < textsOfLanguage.Length; i++) {
							if (fontImporter.customCharacters.IndexOf (textsOfLanguage [i]) == -1)
								fontImporter.customCharacters += textsOfLanguage [i];
						}
					}
				}
			}
		}
		foreach (TrueTypeFontImporter fontImporter in modifiedFontImporters) {
			Debug.Log ("Reimport modified Font " + fontImporter.assetPath + " with characters count: " + fontImporter.customCharacters.Length.ToString ());
			AssetDatabase.ImportAsset (fontImporter.assetPath);
		}
		modifiedFontImporters = null;
	}

	[MenuItem ("Spotlightor/Localization/Log for excel")]
	public static void LogLocalizationTextsForExcel ()
	{
		List<LocalizationTextsAsset> assets = LoadAllLocalizationTextsAssets ();
		string logMsg = "";
		foreach (LocalizationTextsAsset asset in assets) {
			logMsg += asset.name + "\n";
			for (int i = 0; i < asset.TextsCount; i++) {
				LocalizationText localizationText = asset.GetLocalizationTextByIndex (i);
				List<LocalizationText.TextByLanguage> languageTexts = new List<LocalizationText.TextByLanguage> (localizationText.textsByLanguages);
				languageTexts.Sort ((x, y) => ((int)x.language).CompareTo ((int)y.language));
				for (int j = 0; j < languageTexts.Count; j++)
					logMsg += string.Format ("\t\"{0}\"", languageTexts [j].text);
				logMsg += "\n";
			}
		}
		Debug.Log (logMsg);
	}

	private static List<LocalizationTextsAsset> LoadAllLocalizationTextsAssets ()
	{
		List<LocalizationTextsAsset> result = new List<LocalizationTextsAsset> ();
		string[] allAssetPaths = AssetDatabase.GetAllAssetPaths ();
		foreach (string assetPath in allAssetPaths) {
			if (assetPath.Length > 5 && assetPath.Substring (assetPath.Length - 5) == "asset") {
				LocalizationTextsAsset asset = AssetDatabase.LoadAssetAtPath (assetPath, typeof(LocalizationTextsAsset)) as LocalizationTextsAsset;
				if (asset != null)
					result.Add (asset);
			}
		}
		Debug.Log (string.Format ("Load {0} LocalizationTextsAsset.", result.Count));
		return result;
	}
}
