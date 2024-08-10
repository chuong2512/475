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

public class PostSwapTextureChannels : AssetPostprocessor
{
	public const string TargetTexturePostfix = "_";

	void OnPostprocessTexture (Texture2D texture)
	{
		string fileName = System.IO.Path.GetFileNameWithoutExtension (assetPath);
		if (fileName.Substring (fileName.Length - TargetTexturePostfix.Length, TargetTexturePostfix.Length) == TargetTexturePostfix) {
			Color32[] colors = texture.GetPixels32 ();
			for (int i = 0; i < colors.Length; i++) {
				Color32 c = colors [i];
				int grayRgb = (c.r + c.g + c.b) / 3;
				c.r = c.a;
				c.g = c.a;
				c.b = c.a;

				c.a = (byte)grayRgb;

				colors [i] = c;
			}

			texture.SetPixels32 (colors);
			texture.Apply (true);
			Debug.Log (string.Format ("Texture {0} RGB <-> A switched.", fileName));
		}
	}
}
