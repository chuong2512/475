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

public class DigitalDisplayer : NumberTextDisplayer
{
	public enum FloatToIntMethods
	{
		Floor,
		Ceil,
		Round,
	}
	public int digitsCount = 3;
	public int maxValue = 0;
	public FloatToIntMethods floatToIntMethod = FloatToIntMethods.Round;
	private string stringFormat = "";

	protected override string FormatNumberValueToString (float numberValue)
	{
		if (stringFormat == "") {
			if (digitsCount > 0) {
				stringFormat = "{0:";
				for (int i = 0; i < digitsCount; i++)
					stringFormat += "0";
				stringFormat += "}";
			} else
				stringFormat = "{0}";
		}
		if (maxValue != 0)
			numberValue = Mathf.Min (numberValue, maxValue);
		return string.Format (stringFormat, ConverFloatToInt (numberValue));
	}

	private int ConverFloatToInt (float numberValue)
	{
		if (floatToIntMethod == FloatToIntMethods.Ceil)
			return Mathf.CeilToInt (numberValue);
		else if (floatToIntMethod == FloatToIntMethods.Floor)
			return Mathf.FloorToInt (numberValue);
		else
			return Mathf.RoundToInt (numberValue);
	}
}
