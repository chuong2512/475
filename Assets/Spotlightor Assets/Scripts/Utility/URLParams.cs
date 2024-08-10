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

public static class URLParams {
	
	/// <summary>
	/// Get a param value from url with paramName
	/// </summary>
	/// <param name="paramName">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="url">
	/// A <see cref="System.String"/>
	/// </param>
	/// <returns>
	/// A <see cref="System.String"/>
	/// </returns>
	public static string GetURLParam(string paramName, string url)
	{
		if(url == null || url == "")return "";
		if(paramName == null || paramName == "")return "";
		
		int paramStartIndex = url.LastIndexOf(paramName+"=");
		if(paramStartIndex != -1)
		{
			int paramEndIndex = url.IndexOf ("&", paramStartIndex);
			if (paramEndIndex != -1)
				return url.Substring (paramStartIndex + paramName.Length+1, paramEndIndex - 1 - paramStartIndex - paramName.Length);
			else
				return url.Substring (paramStartIndex + paramName.Length+1);
		}
		else return "";
	}
}
