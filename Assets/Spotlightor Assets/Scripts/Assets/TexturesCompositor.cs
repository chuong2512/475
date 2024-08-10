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

public class TexturesCompositor : ScriptableObject
{
	public Texture2D rgb;
	public Texture2D alpha;

	public Texture2D Composite ()
	{
		Color32[] rgbColors = rgb.GetPixels32 ();
		Color32[] alphaColors = alpha.GetPixels32 ();
		Color32[] resultColors = new Color32[rgbColors.Length];
		for (int i= 0; i < resultColors.Length; i++) {
			Color32 rgbColor = rgbColors [i];
			byte a = alphaColors [i].a;
			resultColors [i] = new Color32 (rgbColor.r, rgbColor.g, rgbColor.b, a);
		}

		Texture2D result = new Texture2D (rgb.width, rgb.height);
		result.SetPixels32 (resultColors);
		result.Apply ();
		return result;
	}
}
