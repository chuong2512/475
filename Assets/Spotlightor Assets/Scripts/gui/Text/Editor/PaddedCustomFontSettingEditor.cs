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
using UnityEditor;

[CustomEditor(typeof(PaddedCustomFontSetting))]
public class PaddedCustomFontSettingEditor : Editor
{
	public const string PaddedFontPostfix = "_padded";
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		if (GUILayout.Button ("Upadte Custom Font")) {
			Update (target as PaddedCustomFontSetting);
		}
	}
	
	private static void Update (PaddedCustomFontSetting customFontSetting)
	{
		Font sourceFont = customFontSetting.sourceFont;
		Font customFont = customFontSetting.customFont;
		if (sourceFont != null && customFont != null) {
			int maxTextureSize = customFontSetting.maxTextureSize;
			int padding = customFontSetting.padding;
		
			string sourceFontAssetPath = AssetDatabase.GetAssetPath (sourceFont);
			TrueTypeFontImporter sourceFontImporter = TrueTypeFontImporter.GetAtPath (sourceFontAssetPath) as TrueTypeFontImporter;
			string sourceFontFolderPath = sourceFontAssetPath.Substring (0, sourceFontAssetPath.LastIndexOf ("/") + 1);
			string editableFontAssetPath = sourceFontFolderPath + sourceFont.name + "_editable.fontsettings";
		
			Font editableFontCopy = sourceFontImporter.GenerateEditableFont (editableFontAssetPath);
		
			string editableFontTextureAssetPath = AssetDatabase.GetAssetPath (editableFontCopy.material.mainTexture);
			TextureImporter editableFontTextureImporter = TextureImporter.GetAtPath (editableFontTextureAssetPath) as TextureImporter;
			editableFontTextureImporter.isReadable = true;
			AssetDatabase.ImportAsset (editableFontTextureAssetPath);
		
			Texture2D fontTexture = editableFontCopy.material.mainTexture as Texture2D;
			
			Texture2D[] charTextures = new Texture2D[editableFontCopy.characterInfo.Length];
			for (int k = 0; k < editableFontCopy.characterInfo.Length; k++) {
				CharacterInfo charInfo = editableFontCopy.characterInfo [k];
#if UNITY_4
				int charPixelStartX = Mathf.RoundToInt (charInfo.uv.x * (fontTexture.width));
				int charPixelWidth = Mathf.FloorToInt (charInfo.uv.width * (fontTexture.width));
				int charPixelStartY = Mathf.RoundToInt ((charInfo.uv.y) * (fontTexture.height));
				int charPixelHeight = Mathf.FloorToInt (charInfo.uv.height * (fontTexture.height));
#endif		
				#if UNITY_5_3_OR_NEWER
				int charPixelStartX = Mathf.RoundToInt (charInfo.uvTopLeft.x * (fontTexture.width));
				int charPixelWidth = Mathf.FloorToInt ((charInfo.uvTopRight.x - charInfo.uvTopLeft.x) * (fontTexture.width));
				int charPixelStartY = Mathf.RoundToInt ((charInfo.uvTopLeft.y) * (fontTexture.height));
				int charPixelHeight = Mathf.FloorToInt ((charInfo.uvBottomLeft.y - charInfo.uvTopLeft.y) * (fontTexture.height));
#endif
				int newTextureWidth = Mathf.Abs (charPixelWidth) + padding * 2;
				int newTextureHeight = Mathf.Abs (charPixelHeight) + padding * 2;
				bool isFilpped = false;
#if UNITY_4
				isFilpped = charInfo.flipped;
#endif
				#if UNITY_5_3_OR_NEWER
				isFilpped = charInfo.uvTopLeft.x > charInfo.uvTopRight.x;
#endif
				if (isFilpped) {
					int temp = newTextureWidth;
					newTextureWidth = newTextureHeight;
					newTextureHeight = temp;
				}
				
				if (charPixelHeight < 0)
					charPixelStartY --;
				if (charPixelWidth < 0)
					charPixelStartX--;
				
				Texture2D charTexture = new Texture2D (newTextureWidth, newTextureHeight, TextureFormat.ARGB32, false);
				Color32[] defaultPixels = new Color32[charTexture.width * charTexture.height];
				for (int i = 0; i < defaultPixels.Length; i++)
					defaultPixels [i] = new Color32 (0, 0, 0, 0);
				charTexture.SetPixels32 (defaultPixels);
				
				for (int i= 0; i < Mathf.Abs(charPixelWidth); i++) {
					for (int j = 0; j < Mathf.Abs(charPixelHeight); j++) {
						int x = charPixelStartX + (charPixelWidth > 0 ? i : -i);
						int y = charPixelStartY + (charPixelHeight > 0 ? j : -j);
						
						Color pixel = fontTexture.GetPixel (x, y);
						int newCharX = padding + i;
						int newCharY = padding + j;
						if (!isFilpped)
							charTexture.SetPixel (newCharX, newCharY, pixel);
						else
							charTexture.SetPixel (newCharY, newCharX, pixel);
					}
				}
				charTextures [k] = charTexture;
			}
			
			Texture2D newFontTexture = new Texture2D (maxTextureSize, maxTextureSize, TextureFormat.ARGB32, false);
#if UNITY_4
			Rect[] newCharUvs = newFontTexture.PackTextures (charTextures, 0, maxTextureSize, false);
#endif
#if UNITY_5
			newFontTexture.PackTextures (charTextures, 0, maxTextureSize, false);
#endif
			CharacterInfo[] newCharInfos = editableFontCopy.characterInfo;
			for (int i = 0; i < editableFontCopy.characterInfo.Length; i++) {
#if UNITY_4
				newCharInfos [i].uv = newCharUvs [i];
				newCharInfos [i].flipped = false;
#endif
			}
			customFont.characterInfo = newCharInfos;
			EditorUtility.SetDirty (customFont);
			
			string path = AssetDatabase.GetAssetPath (sourceFont);
			string currentFolderPath = path.Substring (0, path.LastIndexOf ("/") + 1);
			string textureAssetPath = currentFolderPath + sourceFont.name + PaddedFontPostfix + ".png";
			
			SaveAssetUtility.SaveTexture (newFontTexture, textureAssetPath);
			
			AssetDatabase.DeleteAsset (AssetDatabase.GetAssetPath (editableFontCopy.material.mainTexture));
			AssetDatabase.DeleteAsset (AssetDatabase.GetAssetPath (editableFontCopy.material));
			AssetDatabase.DeleteAsset (AssetDatabase.GetAssetPath (editableFontCopy));
			foreach (Texture2D charTexture in charTextures) {
				DestroyImmediate (charTexture);
			}
		} else
			Debug.LogError ("Some font is null");
	}
}
