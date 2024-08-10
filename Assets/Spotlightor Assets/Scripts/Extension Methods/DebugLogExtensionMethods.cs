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

public static class DebugLogExtensionMethods
{
	public static void Log (this Object obj, string format, params object[] args)
	{
		Debug.Log (FormatLogMessage (obj, format, args));
	}
	
	public static void LogWarning (this Object obj, string format, params object[] args)
	{
		Debug.LogWarning (FormatLogMessage (obj, format, args));
	}
	
	public static void LogError (this Object obj, string format, params object[] args)
	{
		Debug.LogError (FormatLogMessage (obj, format, args));
	}
	
	private static string FormatLogMessage (Object obj, string format, object[] args)
	{
		return string.Format ("{0}[{1}]: ", obj.name, obj.GetType ().Name) + string.Format (format, args);
	}
}
