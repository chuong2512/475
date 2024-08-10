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

public static class ColorHexConverter
{
	public static string ColorToHex (Color32 color)
	{
		string hex = "#" + color.r.ToString ("x2") + color.g.ToString ("x2") + color.b.ToString ("x2") + color.a.ToString ("x2");
		return hex;
	}
 
	public static Color HexToColor (string hex)
	{
		if (hex [0] == '#')
			hex = hex.Substring (1);
		
		byte r = byte.Parse (hex.Substring (0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse (hex.Substring (2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse (hex.Substring (4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color32 (r, g, b, 255);
	}
}
