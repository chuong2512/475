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

public class TransitionManagerTester : EasyDebugGui
{
	public TransitionManager transition;

	protected override void DrawDebugGUI ()
	{
		if (transition != null) {
			Label (string.Format ("{0} [{1}]", transition.name, transition.State), 200);

			if (Button ("In", 50))
				transition.TransitionIn ();
			if (Button ("Out", 50))
				transition.TransitionOut ();
		}
	}

	protected override void Update ()
	{
		base.Update ();

		if (transition != null) {
			if (ControlFreak2.CF2Input.GetKeyDown (KeyCode.I))
				transition.TransitionIn ();
			if (ControlFreak2.CF2Input.GetKeyDown (KeyCode.O))
				transition.TransitionOut ();
		}
	}

	void Reset ()
	{
		if (transition == null)
			transition = GetComponent<TransitionManager> ();
	}
}
