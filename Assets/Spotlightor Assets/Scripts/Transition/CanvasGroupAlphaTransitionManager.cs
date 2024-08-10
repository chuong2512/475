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

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupAlphaTransitionManager : ValueTransitionManager
{
	protected override void OnProgressValueUpdated (float progress)
	{
		GetComponent<CanvasGroup> ().alpha = progress;
	}

	protected override void DoTransitionIn (bool instant, StateTypes prevStateType)
	{
		// CanvasGroup.alpha will be set to 1 after Awake (bug), so we've to make sure alpha = 0 before transition in.
		if (prevStateType == StateTypes.Out)
			OnProgressValueUpdated (0);

		base.DoTransitionIn (instant, prevStateType);
	}
}
