/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CopyPlatformTextureSettings : ScriptableWizard
{
	[PopupString (new string[] {
		"Standalone",
		"iPhone",
		"Android",
		"WebGL",
		"Windows Store Apps",
		"PS4",
		"XboxOne",
		"tvOS"
	})]
	public string sourcePlatform = "iPhone";
	public bool sourceOnlyOverriden = true;

	[PopupString (new string[] {
		"Standalone",
		"iPhone",
		"Android",
		"WebGL",
		"Windows Store Apps",
		"PS4",
		"XboxOne",
		"tvOS"
	})]
	public string targetPlatform = "Android";
	public bool targetOnlyNotOverriden = false;
	public bool targetOnlyDifferent = true;

	[MenuItem ("Assets/Copy Platform Texture Settings")]
	static void DisplayWizard ()
	{
		ScriptableWizard.DisplayWizard<CopyPlatformTextureSettings> ("Copy Platform Texture Settings", "Copy", "List Valid Textures");
	}

	private void OnWizardOtherButton ()
	{
		List<TextureImporter> validImporters = FindValidTextureImporters ();

		Debug.LogFormat ("{0} valid importers found.", validImporters.Count);
		string pathLog = "";
		validImporters.ForEach (v => pathLog += v.assetPath + "\n");
		Debug.Log (pathLog);
	}

	private void OnWizardCreate ()
	{
		List<TextureImporter> validImporters = FindValidTextureImporters ();
		for (int i = 0; i < validImporters.Count; i++) {
			TextureImporter importer = validImporters [i];
			TextureImporterPlatformSettings sourcePlatformSettings = importer.GetPlatformTextureSettings (sourcePlatform);
			TextureImporterPlatformSettings targetPlatformSettings = importer.GetPlatformTextureSettings (targetPlatform);

			sourcePlatformSettings.CopyTo (targetPlatformSettings);
			targetPlatformSettings.name = targetPlatform;
			importer.SetPlatformTextureSettings (targetPlatformSettings);

			importer.SaveAndReimport ();
		}
	}

	private List<TextureImporter> FindValidTextureImporters ()
	{
		string[] textureGuids = AssetDatabase.FindAssets ("t:texture");
		List<TextureImporter> validTextureImporters = new List<TextureImporter> ();
		foreach (string guid in textureGuids) {
			TextureImporter importer = AssetImporter.GetAtPath (AssetDatabase.GUIDToAssetPath (guid)) as TextureImporter;
			if (importer != null) {
				TextureImporterPlatformSettings sourcePlatformSettings = importer.GetPlatformTextureSettings (sourcePlatform);
				TextureImporterPlatformSettings targetPlatformSettings = importer.GetPlatformTextureSettings (targetPlatform);
				if (sourcePlatformSettings != null && targetPlatformSettings != null) {
					if ((!sourceOnlyOverriden || sourcePlatformSettings.overridden)
					    && (!targetOnlyNotOverriden || !targetPlatformSettings.overridden)) {
						if (targetOnlyDifferent) {
							bool isDifferent = false;
							if (targetPlatformSettings.overridden != sourcePlatformSettings.overridden)
								isDifferent = true;
							if (targetPlatformSettings.maxTextureSize != sourcePlatformSettings.maxTextureSize)
								isDifferent = true;
							if (targetPlatformSettings.textureCompression != sourcePlatformSettings.textureCompression)
								isDifferent = true;
							if (targetPlatformSettings.compressionQuality != sourcePlatformSettings.compressionQuality)
								isDifferent = true;
							if (targetPlatformSettings.allowsAlphaSplitting != sourcePlatformSettings.allowsAlphaSplitting)
								isDifferent = true;
							if (targetPlatformSettings.crunchedCompression != sourcePlatformSettings.crunchedCompression)
								isDifferent = true;
							if (targetPlatformSettings.format != sourcePlatformSettings.format)
								isDifferent = true;
							
							if (isDifferent)
								validTextureImporters.Add (importer);
						} else
							validTextureImporters.Add (importer);
					}
				}
			}
		}
		return validTextureImporters;
	}
}
