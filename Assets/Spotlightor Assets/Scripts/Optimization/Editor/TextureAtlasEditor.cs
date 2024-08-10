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

[CustomEditor(typeof(TextureAtlas))]
public class TextureAtlasEditor : Editor
{
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		if (GUILayout.Button ("Pack Texture Atlas")) {
			Pack ((target as TextureAtlas));
		}
	}

	public static void Pack (TextureAtlas textureAtlas)
	{
		ValidateTextureImportSettings (textureAtlas);
		
		Texture2D atlas = textureAtlas.Pack ();
		EditorUtility.SetDirty (textureAtlas);
			
		string path = AssetDatabase.GetAssetPath (textureAtlas);
		string folderPath = path.Substring (0, path.LastIndexOf ("/") + 1);
		string atlasTexturePath = folderPath + textureAtlas.name.ToLower () + ".png";
			
		SaveAssetUtility.SaveTexture (atlas, atlasTexturePath);
	}
	
	private static void ValidateTextureImportSettings (TextureAtlas textureAtlas)
	{
		foreach (Texture2D texture in textureAtlas.textures) {
			string textureAssetPath = AssetDatabase.GetAssetPath (texture);
			TextureImporter textureImporter = AssetImporter.GetAtPath (textureAssetPath) as TextureImporter;
			if (textureImporter != null) {

				TextureImporterSettings validSettings = new TextureImporterSettings ();
				textureImporter.ReadTextureSettings (validSettings);

				if (validSettings.mipmapEnabled ||
					!validSettings.readable ||
					validSettings.filterMode != FilterMode.Point ||
					validSettings.wrapMode != TextureWrapMode.Clamp ||
					validSettings.npotScale != TextureImporterNPOTScale.None ||
					validSettings.alphaIsTransparency == false ||
					validSettings.sRGBTexture == false) {

					validSettings.mipmapEnabled = false;
					validSettings.readable = true;
					validSettings.filterMode = FilterMode.Point;
					validSettings.wrapMode = TextureWrapMode.Clamp;
					validSettings.npotScale = TextureImporterNPOTScale.None;
					validSettings.alphaIsTransparency = true;
					validSettings.sRGBTexture = true;

					textureImporter.SetTextureSettings (validSettings);
					AssetDatabase.ImportAsset (textureAssetPath, ImportAssetOptions.ForceUpdate);
					
					Debug.Log (string.Format ("{0}'s TextureImport settings validated for atlas.", texture.name));
				}
			}
		}
	}
}
