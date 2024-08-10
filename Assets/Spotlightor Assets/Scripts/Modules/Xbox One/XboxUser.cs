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

#if UNITY_XBOXONE
using Users;
#endif

public class XboxUser
{
	#if UNITY_XBOXONE
	private User user;
	#endif

	public string DisplayName {
		get {
			string displayName = "Unkown";
			#if UNITY_XBOXONE
			if (user != null)
				displayName = user.GameDisplayName;
			#endif
			return displayName;
		}
	}

	public int Index {
		get {
			#if UNITY_XBOXONE
			return user != null ? user.Index : -1; 
			#else
			return -1;
			#endif
		}
	}

	public int Id {
		get { 
			#if UNITY_XBOXONE
			return user != null ? user.Id : -1; 
			#else
			return -1;
			#endif
		}
	}

	public string UID {
		get { 
			#if UNITY_XBOXONE
			return user != null ? user.UID : ""; 
			#else
			return "";
			#endif
		}
	}

	#if UNITY_XBOXONE
	public XboxUser (User user)
	{
		this.user = user;
	}
	#endif

	public XboxUser ()
	{
	}
}
