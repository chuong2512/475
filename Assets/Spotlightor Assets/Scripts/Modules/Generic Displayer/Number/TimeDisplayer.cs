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

public class TimeDisplayer : NumberTextDisplayer
{
	private const string TextFormatTokenMinutes = "[m]";
	private const string TextFormatTokenSeconds = "[s]";
	private const string TextFormatTokenMilliseconds = "[ms]";
	[Header("Avaialbe tokens: [m] [s] [ms]")]
	public string
		timeFormat = "[m]'[s]\"[ms]";
	
	protected override string FormatNumberValueToString (float numberValue)
	{
		string timeText = timeFormat;
		
		if (timeText.Contains (TextFormatTokenMinutes)) {
			int minutes = Mathf.FloorToInt (numberValue / 60);
			timeText = timeText.Replace (TextFormatTokenMinutes, string.Format ("{0:00}", minutes));
		}
		
		if (timeText.Contains (TextFormatTokenSeconds)) {
			int seconds = Mathf.FloorToInt (numberValue) % 60;
			timeText = timeText.Replace (TextFormatTokenSeconds, string.Format ("{0:00}", seconds));
		}
		
		if (timeText.Contains (TextFormatTokenMilliseconds)) {
			int milliseconds = Mathf.FloorToInt ((numberValue - Mathf.FloorToInt (numberValue)) * 1000);
			timeText = timeText.Replace (TextFormatTokenMilliseconds, string.Format ("{0:000}", milliseconds));
		}
		
		return timeText;
	}
}
