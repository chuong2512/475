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

public class ObjectTransitionsNumberDisplayer : NumberDisplayer
{
	public TransitionManager[] transitions;

	protected override void Display (float value)
	{
		int intValue = Mathf.RoundToInt (value);

		for (int i = 0; i < transitions.Length; i++) {
			if (i < intValue)
				transitions [i].TransitionIn ();
			else
				transitions [i].TransitionOut ();
		}

		if (intValue < 0 || intValue > transitions.Length) 
			this.LogWarning ("Display value {0} out of display range: {1}~{2}", value, 0, transitions.Length);
	}

	void Reset ()
	{
		transitions = GetComponentsInChildren<TransitionManager> ();
	}
}
